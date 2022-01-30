using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeState : MonoBehaviour
{
    public Transform RopeRaftAnchor;

    public float ropeMaxLength = 10;
    public Vector3 ropeVector;
    public float ropeLength;

    public bool inRangeOfRope;

    // Update is called once per frame
    void Update()
    {  
        ropeVector = RopeRaftAnchor.position - transform.position;
        ropeLength = ropeVector.magnitude;
        if(ropeLength > ropeMaxLength)
        {
            inRangeOfRope = false;
        }
        else
        {
            inRangeOfRope = true;
        }  
    }
}
