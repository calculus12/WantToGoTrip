using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private PlayerInput input;

    public Camera firstPersonCamera;

    public CharacterController controller;

    public float speed = 12f;
    public float speedWhileJump = 8f;
    public float underwaterSpeed = 6f;
    public float underwaterUpSpeed = 2f;

    public float gravityAcceleration = -9.81f;
    public float buoyancy = 8f;

    private Vector3 velocity; // for gravity

    public float jumpHeight = 3f;

    public float underwaterJumpHeight = 1f;

    public Transform groundCheck1;
    public Transform groundCheck2;
    public Transform waterCheck;

    public float groundDistance = 0.4f;

    public LayerMask groundMask;
    public LayerMask waterMask;
    public LayerMask surfaceMask;

    private Animator animator;

    public bool isGrounded { get; private set; }

    public bool isUnderwater { get; private set; }

    public bool isSurface { get; private set; }

    public bool isFalling { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck1.position, groundDistance, groundMask) |
            Physics.CheckSphere(groundCheck2.position, groundDistance, groundMask);
        isUnderwater = Physics.CheckSphere(waterCheck.position, 0.02f, waterMask);
        isSurface = Physics.CheckSphere(waterCheck.position, 0.1f, surfaceMask);

        Vector3 direction = transform.right * input.x + transform.forward * input.z;

        if (isGrounded)
        {
            // prevent increasing velocity by gravity
            if (velocity.y < 0)
            {
                velocity.y = -2f;
            }

            // jump
            if (input.space)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityAcceleration);
            }

            // play animation
            if (input.x != 0 || input.z != 0)
            {
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }

            controller.Move(direction * speed * Time.deltaTime);

            controller.Move(velocity * Time.deltaTime);
        }
        else if (!isUnderwater && !isGrounded)
        {
            isFalling = true;
            // gravity force
            if (velocity.y >= -60f) // terminal speed
                velocity.y += gravityAcceleration * Time.deltaTime;

            controller.Move(direction * speedWhileJump * Time.deltaTime);

            controller.Move(velocity * Time.deltaTime);
        }
        else if (isUnderwater)
        {
            Vector3 underwaterDirection = firstPersonCamera.transform.right * input.x +
                                          firstPersonCamera.transform.forward * input.z;
            if (isFalling)
            {
                velocity.y += buoyancy * Time.deltaTime;
                controller.Move(velocity * Time.deltaTime);
                if (velocity.y >= 0f)
                {
                    velocity.y = 0f;
                    isFalling = false;
                }

            }

            if (!isFalling && input.space)
            {
                controller.Move(Vector3.up * underwaterUpSpeed * Time.deltaTime);
            }

            if (isSurface && !isFalling)
            {
                if (input.space)
                {
                    velocity.y = Mathf.Sqrt(underwaterJumpHeight * -2f * gravityAcceleration);
                    controller.Move(velocity * Time.deltaTime);
                }
            }

            controller.Move(underwaterDirection * speed * Time.deltaTime);
        }
    }
}
