using System.Collections;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // input of player
    private PlayerInput input;

    public CharacterController controller;

    public Transform camTransform;
    public RopeState  ropeState;

    public float turnSmoothTime = 0.1f; // player's rotation speed
    public float turnSmoothVelocity;

    public float speed = 12f; // player's speed
    public float jumpHeight = 3f;
    public float speedWhileJump = 8f; // player's speed while jump
    public float underwaterSpeed = 6f; // player's speed under the water
    public float underwaterUpDownSpeed = 2f; // player's speed when player moves up or down

    public float gravityAcceleration = -9.81f;
    public float buoyancy = 15f;
    public float underwaterYDamping = 0.5f;

    private Vector3 velocity; // only for y-axis movement (gravity or buoyancy)
    private float underwaterY = 0f;
    private bool jumpAnimationPlayed = false;
    private bool gettingForce = false;

    // jump height when player jumps on water surface
    public float underwaterJumpHeight = 1f;

    // player's state
    private PlayerState state;

    // player animator
    private Animator animator;

    // player's final moving vector
    private Vector3 moveVector;

    void Start()
    {
        animator = GetComponent<Animator>();
        input = GetComponent<PlayerInput>();
        state = GetComponent<PlayerState>();
    }

    void LateUpdate()
    {
        // Raft Hp bar rotates, so player can see it
        UIManager.instance.RotateRaftHP(camTransform.forward);
    }

    void Update()
    {
        moveVector = Vector3.zero;

        if (state.isSailing)
        {
            animator.SetBool("isWalking", false);

            /*
             * need to add sailing animation
             */

            return; // player cannnot move while sailing

        }


        Vector3 direction = new Vector3(input.horizontal, 0f, input.vertical).normalized;

        if (state.isOnRaft || state.isGrounded)
        {
            state.isFalling = false;
            underwaterY = 0f;
            //Debug.Log(RaftController.velocity);
            if (state.isOnRaft)
            {
                moveVector += RaftController.velocity * Time.deltaTime; // if player is on raft, then plaeyr should be moved with raft's same velocity
            }
            // prevent increasing velocity by gravity while player is grounded
            if (velocity.y < 0)
            {
                velocity.y = -1f;
            }

            // jump

            if (state.canJump && input.space)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravityAcceleration);
                if (!jumpAnimationPlayed)
                {
                    animator.SetTrigger("jump");
                    jumpAnimationPlayed = true;
                }
            }

            // if player is moving then, play walk animation and make move
            if (direction.magnitude >= 0.1f) // if player is moving
            {
                /// for smooth rotation
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg
                                    + camTransform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                    turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                /// for smooth rotation

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                moveVector += moveDir.normalized * speed * Time.deltaTime;
                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }

            moveVector += velocity * Time.deltaTime; // y-axis movement (move caused by gravity)
        }
        else if (!state.isUnderwater && !state.isOnRaft && !state.isGrounded) // if player is falling
        {
            state.isFalling = true;
            jumpAnimationPlayed = false;
            underwaterY = 0f;
            // gravity force
            if (velocity.y >= -60f) // terminal speed
                velocity.y += gravityAcceleration * Time.deltaTime;

            if (direction.magnitude >= 0.1f) // if player is moving
            {
                /// for smooth rotation
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg
                                    + camTransform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                    turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                /// for smooth rotation

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                moveVector += moveDir.normalized * speed * Time.deltaTime;
            }

            moveVector += velocity * Time.deltaTime; // y-axis movement (move caused by gravity)
        }
        else if (state.isUnderwater)
        {
            if (state.isFalling)
            {
                velocity.y += buoyancy * Time.deltaTime;
                moveVector += velocity * Time.deltaTime;
                if (velocity.y >= 0f)
                {
                    velocity.y = 0f;
                    state.isFalling = false;
                }

            }

            if (direction.magnitude >= 0.1f) // if player is moving
            {
                /// for smooth rotation
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg
                                    + camTransform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                    turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
                /// for smooth rotation

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                moveVector += moveDir.normalized * speed * Time.deltaTime;
                // should play swimming animation
            }

            if (!state.isFalling && input.space)
            {
                //controller.Move(Vector3.up * underwaterUpDownSpeed * Time.deltaTime);
                velocity.y += underwaterUpDownSpeed;
            }

            if (!state.isFalling && input.ctrl)
            {
                velocity.y -= underwaterUpDownSpeed;
            }

            if (Mathf.Abs(underwaterY) >= underwaterUpDownSpeed / 2)
                velocity.y = 0f;

            underwaterY = Mathf.Lerp(underwaterY, velocity.y, Time.deltaTime * underwaterYDamping);
            moveVector += Vector3.up * underwaterY * Time.deltaTime;

            if (state.isSurface && !state.isFalling)
            {
                if (input.space)
                {
                    velocity.y = Mathf.Sqrt(underwaterJumpHeight * -2f * gravityAcceleration);
                    if (!jumpAnimationPlayed)
                    {
                        animator.SetTrigger("jump");
                        jumpAnimationPlayed = true;
                    }
                    moveVector += velocity * Time.deltaTime;
                }
            }
        }
        // Movement control according to rope distance and movement vector
        if (Vector3.Dot(ropeState.ropeVector.normalized, moveVector) + ropeState.ropeLength <= ropeState.ropeMaxLength + 1)
        {
            controller.Move(moveVector);
        }
        else
        {
           if(!gettingForce)
           StartCoroutine(GetForce(ropeState.ropeVector.normalized * 2f));
        }
    }


    public IEnumerator GetForce(Vector3 velocity)
    {
        gettingForce = true;
        float tempSpeed = speed;
        Debug.Log(tempSpeed);
        speed = 0.5f;

        while (velocity.magnitude >= 1f)
        {
            velocity = Vector3.Lerp(velocity, Vector3.zero, Time.deltaTime);
            Debug.Log(velocity);
            yield return new WaitForFixedUpdate();
            controller.Move(velocity * Time.deltaTime);
        }
        Debug.Log(tempSpeed);
        speed = tempSpeed;
        gettingForce = false;
    }

}
