using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLumbering : MonoBehaviour
{
    public Animator animator;
    public PlayerInput input;
    public PlayerState state;
    
    public Transform axeRaycastStart;

    public float damage = 30;
    public float lumberingTime = 1f;
    public float axeDistance = 0.3f;

    private bool isLumbering = false;

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
        isLumbering = true;
        state.canNotChangeEquipment = true;
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
        state.canNotChangeEquipment = false;
        isLumbering = false;
    }
}
