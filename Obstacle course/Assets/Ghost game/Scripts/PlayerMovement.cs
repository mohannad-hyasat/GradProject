using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.EditorTools;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement parameters")]
    private float moveSpeed;
    public float walkspeed;
    public float runSpeed;
    public float crouchSpeed;
    [HideInInspector]
    public bool isGrounded;
    public LayerMask groundMask;
   [HideInInspector] 
    public float gravity;
    private Vector3 moveDir;
    private Vector3 velocity;
    [Header("Crouch parameters")]
    public float crouchHeight = 0.5f;
    public float standHeight = 2f;
    private const float timeToCrouch = 0.25f;
    public Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    public Vector3 standingCenter = new Vector3(0, 1f, 0);
    private bool isCrouching = false;
    private bool duringCrouchAnimation;
    private bool shouldCrouch => Input.GetKeyDown(KeyCode.LeftControl) && !duringCrouchAnimation && isGrounded;
    [HideInInspector]
    public Animator animator;
    private CharacterController characterController;
    [HideInInspector]
    public UniversalHealth PlayerHealth;
    public RoomsManager Favroom;
    [HideInInspector]
    public GameObject[] items = new GameObject[2];


    // Start is called before the first frame update
    void Start()
    {
        
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        PlayerHealth = GetComponentInParent<UniversalHealth>();
        Favroom = GameObject.FindGameObjectWithTag("World").GetComponent<RoomsManager>();
        for(int i = 0; i < items.Length; i++)
        {
            items[i].SetActive(false);
        }


    }
    /// <summary>
    /// function that handles all the switching between items mechanic
    /// </summary>
    private void SwitchingItems()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            items[1].SetActive(false);
            items[2].SetActive(false);
            items[0].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            items[1].SetActive(false);
            items[2].SetActive(false);
            items[0].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            items[1].SetActive(false);
            items[2].SetActive(false);
            items[0].SetActive(true);
        }

    }

    // Update is called once per frame
    
    /// <summary>
    /// function that handles all the movement of the player controller
    /// </summary>
    private void Move()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }
        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");
        moveDir = transform.right * moveX + transform.forward * moveZ;
        
        
            if (moveDir != Vector3.zero && isCrouching)
            {
                moveSpeed = crouchSpeed * Time.deltaTime;
                animator.SetBool("CrouchWalk", true);
                animator.SetBool("Crouch", false);
                animator.SetBool("walk", false);
                animator.SetFloat("CFB", moveZ);
                animator.SetBool("run", false);
                if (moveZ < 0)
                    animator.SetFloat("CRL", -moveX);
                else
                    animator.SetFloat("CRL", moveX);
            }
            if (moveDir != Vector3.zero && !Input.GetKey(KeyCode.LeftShift) && !isCrouching)
            {
                moveSpeed = walkspeed * Time.deltaTime;
                animator.SetBool("walk", true);
                animator.SetBool("run", false);
                animator.SetBool("Crouch", false);
                animator.SetBool("CrouchWalk", false);
                animator.SetFloat("FB", moveZ);
                if (moveZ < 0)
                    animator.SetFloat("RL", -moveX);
                else
                    animator.SetFloat("RL", moveX);
                
            }
            else if (moveDir != Vector3.zero && Input.GetKey(KeyCode.LeftShift) && !isCrouching)
            {
                moveSpeed = runSpeed * Time.deltaTime;
                animator.SetBool("run", true);
                animator.SetFloat("SFB", moveZ);
                animator.SetBool("walk", false);
                animator.SetBool("Crouch", false);
                animator.SetBool("CrouchWalk", false);
                if (moveZ < 0)
                    animator.SetFloat("SRL", -moveX);
                else
                    animator.SetFloat("SRL", moveX);

            }
            else if (moveDir == Vector3.zero && isCrouching)
            {
                animator.SetBool("run", false);
                animator.SetBool("walk", false);
                animator.SetBool("Crouch", true);
                animator.SetBool("CrouchWalk", false);

            }
            else if (moveDir == Vector3.zero && !isCrouching)
            {
                animator.SetBool("run", false);
                animator.SetBool("walk", false);
                animator.SetBool("Crouch", false);
                animator.SetBool("CrouchWalk", false);
                animator.SetFloat("FB", 0f);
                animator.SetFloat("RL", 0f);

            }
            characterController.Move(moveDir * moveSpeed);
        
        if(!isGrounded)
        { 
            velocity.y += gravity * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);
        }
        
    }
    /// <summary>
    /// handels the crouch function
    /// </summary>
    private void CrouchHandler()
    {
        if (shouldCrouch)
        {
            CrouchStand();
        }
    }
    /// <summary>
    /// Switches between crouching and standing
    /// </summary>
    private async void CrouchStand()
    {
        duringCrouchAnimation = true;
        float timelapsed = 0f;
        float targetHeight = isCrouching ? standHeight : crouchHeight;
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = characterController.center;
        float currentHeight = characterController.height;
        while (timelapsed < timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timelapsed / timeToCrouch);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timelapsed / timeToCrouch);
            timelapsed += Time.deltaTime;
            await Task.Yield();
        }
        characterController.height = targetHeight;
        characterController.center = targetCenter;
        isCrouching = !isCrouching;
        duringCrouchAnimation = false;
    }
    void Update()
    {
        Favroom.DistanceBetweenPlayerAndRoom = Vector3.Distance(Favroom.Favorite_Room.position, gameObject.transform.position); //check distance betweeen player and ghost room

        if (!PlayerHealth.IsDead)
        {
            CrouchHandler();
            Move();
            SwitchingItems();
        }
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.up), out hit, 0.08f, groundMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.up) * 0.08f, Color.red);
            isGrounded = true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(-Vector3.up) * 0.08f, Color.red);
            isGrounded = false;
        }
    }
}