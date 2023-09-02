using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed;
    public float walkspeed;
    public float runSpeed;
    public float crouchSpeed;
    public bool isGrounded;
    public LayerMask groundMask;
    public float gravity;
    public UniversalHealth PlayerHealth;
    private Vector3 moveDir;
    private Vector3 velocity;
    public Animator animator;

    [SerializeField] private CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        PlayerHealth = GetComponentInParent<UniversalHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerHealth.Health > 0) { Move(); }

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 0.1f, groundMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 0.1f, Color.red);
            isGrounded = true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 0.1f, Color.red);
            isGrounded = false;
        }
    }

    private void Move()
    {

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = 0f;
        }

        float moveZ = Input.GetAxis("Vertical");
        float moveX = Input.GetAxis("Horizontal");
        moveDir = transform.right * moveX + transform.forward * moveZ;

        if (isGrounded )
        {
            if(moveDir != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = walkspeed * Time.deltaTime;
                animator.SetBool("walk", true);
                animator.SetFloat("FB", moveZ);
                if(moveZ<0)
                    animator.SetFloat("RL", -moveX);
                else
                    animator.SetFloat("RL", moveX);
                animator.SetBool("run", false);
            }
            else if(moveDir != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = runSpeed * Time.deltaTime;
                animator.SetBool("run", true);
                animator.SetFloat("SFB", moveZ);
                animator.SetBool("walk", false);
                if(moveZ<0)
                    animator.SetFloat("SRL", -moveX);
                else
                    animator.SetFloat("SRL", moveX);
                
            }
            else if(moveDir == Vector3.zero)
            {
                animator.SetBool("walk", false);
                animator.SetBool("run", false);


            }
            characterController.Move(moveDir * moveSpeed);
        }
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

}
