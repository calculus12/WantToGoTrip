using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    
    // transform(position) for check whether player is grounded or in water
    public Transform groundCheck1;
    public Transform groundCheck2;
    public Transform waterCheck;

    public float groundDistance = 0.4f;

    // mask that distinguish ground, water, water surface
    public LayerMask groundMask;
    public LayerMask waterMask;
    public LayerMask surfaceMask;
    
    //property that represent player's state
    public bool isGrounded { get; private set; }

    public bool isUnderwater { get; private set; }

    public bool isSurface { get; private set; }

    public bool isFalling { get; set; }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck1.position, groundDistance, groundMask) |
                     Physics.CheckSphere(groundCheck2.position, groundDistance, groundMask);
        isUnderwater = Physics.CheckSphere(waterCheck.position, 0.02f, waterMask);
        isSurface = Physics.CheckSphere(waterCheck.position, 0.1f, surfaceMask);
    }
}
