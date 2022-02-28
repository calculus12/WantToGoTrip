using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RepairFunc : MonoBehaviour
{
    [SerializeField] Transform checkCrashTransform;
    [SerializeField] LayerMask crashMask;
    [SerializeField] PlayerInput playerInput;
    [SerializeField] GameObject fixBar;
    [SerializeField] Image fixBarFill;
    [SerializeField] CrashManager cm;
    [SerializeField] PlayerState state;
    public float fixTime = 3f;
    private float checkSphereRadius = 0.5f;
    private bool isRepairing = false;

    private void OnDisable()
    {
        UIManager.instance.SetActiveCrashUI(false);
    }
    private void Update()
    {
        GameObject crash;
        Collider[] nearCrashes = Physics.OverlapSphere(checkCrashTransform.position, checkSphereRadius, crashMask);
        Debug.Log(nearCrashes.Length);
        if (nearCrashes.Length > 0) // detect crash
        {
            if (!isRepairing)  UIManager.instance.SetActiveCrashUI(true);
            crash = nearCrashes[0].gameObject;
        }
        else
        {
            crash = null;
            UIManager.instance.SetActiveCrashUI(false);
        }

        if (crash != null)
        {
            if (playerInput.f) // if input F then, fix crash
                StartCoroutine(Repair(crash));
        }
    }
    private IEnumerator Repair(GameObject crash)
    {
        UIManager.instance.SetActiveCrashUI(false);
        state.canNotChangeEquipment = true;
        state.canNotMove = true;
        float curTime = 0f;
        isRepairing = true;
        fixBar.SetActive(true);
        while (playerInput.presingF)
        {
            curTime += Time.deltaTime;
            FixFillUpdate(curTime / fixTime);
            yield return new WaitForEndOfFrame();
            if(curTime > fixTime)
            {
                cm.Remove(crash);
                break;
            }
        }
        fixBar.SetActive(false);
        isRepairing = false;
        state.canNotChangeEquipment = false;
        state.canNotMove = false;
    }

    private void FixFillUpdate(float val)
    {
        fixBarFill.fillAmount = val;
    }
}
