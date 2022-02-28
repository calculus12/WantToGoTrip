using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerState : MonoBehaviour
{

    // transform(position) for check whether player is grounded or in water
    public Transform groundCheck1;
    public Transform groundCheck2;
    public Transform waterCheck;
    public Transform submergeCheck;
    public BoxCollider waterBox;

    public float groundDistance = 0.4f;
    public float jumpCheckDistance = 0.1f;

    // mask that distinguish ground, water, water surface
    public LayerMask raftMask;
    public LayerMask groundMask;
    public LayerMask waterMask;
    public LayerMask surfaceMask;
    
    //property that represent player's state
    public bool isOnRaft { get; private set; }

    public bool isGrounded { get; private set; }
    
    public bool canJump { get; private set; }

    public bool isUnderwater { get; private set; }

    public bool isSurface { get; private set; }

    public bool isFalling { get; set; }
    
    public bool isSailing { get; set; }

    public bool canNotMove{ get; set; } // fixing, fishing etc..

    public bool canNotChangeEquipment { get; set; } // sailing, lumbering etc..

    public bool isSubmerging { get; private set; }

    //public int currentEquipment { get; set; } // Indicate which equipment player is equipping

    void Update()
    {
        isOnRaft = Physics.CheckSphere(groundCheck1.position, groundDistance, raftMask) |
                     Physics.CheckSphere(groundCheck2.position, groundDistance, raftMask);
        isGrounded = Physics.CheckSphere(groundCheck1.position, groundDistance, groundMask) |
            Physics.CheckSphere(groundCheck2.position, groundDistance, groundMask);
        canJump = Physics.CheckSphere(groundCheck1.position, jumpCheckDistance, raftMask) |
                  Physics.CheckSphere(groundCheck2.position, jumpCheckDistance, raftMask) |
                  Physics.CheckSphere(groundCheck1.position, jumpCheckDistance, groundMask) |
            Physics.CheckSphere(groundCheck2.position, jumpCheckDistance, groundMask);
        isUnderwater = Physics.CheckSphere(waterCheck.position, 0.02f, waterMask);
        isSurface = Physics.CheckSphere(waterCheck.position, 0.1f, surfaceMask);
        isSubmerging = waterBox.bounds.Contains(submergeCheck.position);
    }
}
