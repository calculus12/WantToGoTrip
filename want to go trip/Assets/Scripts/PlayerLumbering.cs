using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLumbering : MonoBehaviour
{
    Animator animator;
    PlayerInput input;
    PlayerState state;
    
    public Transform axeRaycastStart;

    public float damage = 30;
    public float lumberingTime = 1f;
    public float axeDistance = 0.3f;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        input = GetComponent<PlayerInput>();
        state = GetComponent<PlayerState>();
    }


    void Update()
    {
        if (input.leftClick)
        {
            if (!state.isLumbering)
                StartCoroutine(LumberRoutine());
        }
    }

    private IEnumerator LumberRoutine()
    {
        state.isLumbering = true;
        animator.SetTrigger("lumber");

        yield return new WaitForSeconds(0.4f);
        Ray ray = new Ray(axeRaycastStart.position, axeRaycastStart.forward);

        RaycastHit hit;
        Debug.DrawRay(axeRaycastStart.position, axeRaycastStart.forward * axeDistance, Color.red, 0.5f);
        if (Physics.Raycast(ray, out hit, axeDistance))
        {
            IDamageable target = hit.collider.GetComponent<IDamageable>();

            if (target != null)
            {
                Debug.Log(hit.collider.name);
                target.OnDamage(damage, hit.point, hit.normal);
            }
        }
        yield return new WaitForSeconds(lumberingTime);
        state.isLumbering = false;
    }
}
