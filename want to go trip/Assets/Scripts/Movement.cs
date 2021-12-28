using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public PlayerInput input;

    public Camera firstPersonCamera;

    public CharacterController controller;

    public float speed = 12f;
    public float underwaterSpeed = 6f;

    public float gravityAcceleration = -9.81f;
    public float buoyancy = 2f;

    private Vector3 velocity; // for gravity

    public float jumpHeight = 3f;

    public float underwaterJumpForce = 1f;

    public Transform groundCheck;
    public Transform waterCheck;

    public float groundDistance = 0.4f;

    public LayerMask groundMask;
    public LayerMask waterMask;
    public LayerMask surfaceMask;

    public Animator animator;

    public bool isGrounded { get; private set; }

    public bool isUnderwater { get; private set; }

    public bool isSurface { get; private set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        isUnderwater = Physics.CheckSphere(waterCheck.position, 0.02f, waterMask);
        isSurface = Physics.CheckSphere(waterCheck.position, 0.05f, surfaceMask);

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
            // gravity force
            if (velocity.y >= -60f) // terminal speed
                velocity.y += gravityAcceleration * Time.deltaTime;

            controller.Move(direction * speed * Time.deltaTime);

            controller.Move(velocity * Time.deltaTime);
        }
        else if (isUnderwater)
        {
            Vector3 underwaterDirection = firstPersonCamera.transform.right * input.x +
                                          firstPersonCamera.transform.forward * input.z;

            if (isSurface)
            {
                if (input.space)
                {
                    velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityAcceleration);
                    controller.Move(velocity * Time.deltaTime);
                }
            }

            controller.Move(underwaterDirection * speed * Time.deltaTime);
        }
    }
}
