using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLumbering : MonoBehaviour
{
    Animator animator;
    PlayerInput input;

    public float lumberingTime = 1f;
    private bool isLumbering;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        input = GetComponent<PlayerInput>();
    }


    void Update()
    {
        if (input.leftClick)
        {
            if (!isLumbering)
                StartCoroutine(LumberRoutine());
        }
    }

    private IEnumerator LumberRoutine()
    {
        Debug.Log("진입성공");
        isLumbering = true;
        animator.SetTrigger("lumber");
        yield return new WaitForSeconds(lumberingTime);
        isLumbering = false;
    }
}
