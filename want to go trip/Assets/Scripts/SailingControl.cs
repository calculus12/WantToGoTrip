using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SailingControl : MonoBehaviour
{
    public PlayerInput input;
    public Transform playerTransform;
    public Transform sailingPosition;
    public BoxCollider sailingUIZone;
    public GameObject sailingUI;
    public PlayerState playerState;

    private void Update()
    {
        if (playerState.isSailing)
        {
            playerTransform.SetPositionAndRotation(sailingPosition.position, sailingPosition.rotation);
            if (input.f)
            {
                playerState.isSailing = false;
            }
        }
        else if (sailingUIZone.bounds.Contains(playerTransform.position) && !playerState.isSailing)
        {
            sailingUI.SetActive(true);
            if (input.f)
            {
                playerState.isSailing = true;
                playerTransform.SetPositionAndRotation(sailingPosition.position, sailingPosition.rotation);
                sailingUI.SetActive(false);
            }
        }
        else
        {
            sailingUI.SetActive(false);
        }
    }
}
