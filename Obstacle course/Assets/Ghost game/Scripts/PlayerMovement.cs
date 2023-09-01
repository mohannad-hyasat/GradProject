using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveSpeed;
    public float walkspeed;
    public float runSpeed;
    public bool isGrounded;
    public LayerMask groundMask;
    public float gravity;


    private Vector3 moveDir;
    private Vector3 velocity;
    public Animator animator;

    [SerializeField] private CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {        
        Move();
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
                animator.SetBool("run", false);
            }

            else if(moveDir != Vector3.zero && Input.GetKeyDown(KeyCode.LeftShift))
            {
                moveSpeed = runSpeed * Time.deltaTime;
                animator.SetBool("run", true);
                animator.SetBool("walk", false);
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
