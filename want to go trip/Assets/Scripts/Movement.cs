using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public PlayerInput input;
    
    public CharacterController controller;

    public float speed = 12f;

    public float gravityAcceleration = -9.81f;
    
    private Vector3 velocity;

    public float jumpHeight = 3f;

    public Transform groundCheck;

    public float groundDistance = 0.4f;

    public LayerMask groundMask;
    public LayerMask waterMask;

    public Animator animator;

    private bool isGrounded;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask) |
                     Physics.CheckSphere(groundCheck.position, groundDistance, waterMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (input.space && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityAcceleration);
        }

        if (input.x != 0 || input.z != 0)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        Vector3 direction = transform.right * input.x + transform.forward * input.z;

        controller.Move(direction * speed * Time.deltaTime);

        velocity.y += gravityAcceleration * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
