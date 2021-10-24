using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Rigidbody playerRigidbody;
    Animator playerAnimator;
    PlayerInput playerInput;
    public float speed = 8f; // 이동속도
    public bool isWalking // 움직이고 있으면 true
    {
        get;
        private set;
    }

    public bool isLumbering { get; private set; }
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        playerAnimator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void FixedUpdate() // 물리주기 마다 입력을 확인해서 이동, 도끼질, 애니메이션 재생
    {

        Move(playerInput.horizontalMoving, playerInput.verticalMoving);
        Lumber(playerInput.space);
        playerAnimator.SetBool("isWalking", isWalking);
    }

    void Move(float horizontal, float vertical)
    {
        if (horizontal == 0f && vertical == 0f)
        {
            isWalking = false;
            return;
        }
        isWalking = true;
        Vector3 newPosition = transform.position + new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime;
        playerRigidbody.MovePosition(newPosition);
        transform.LookAt(newPosition);
    }

    void Lumber(bool lumber)
    {
        if (!lumber)
        {
            isLumbering = false;
            return;
        }
        playerAnimator.SetTrigger("lumber");
    }
}
