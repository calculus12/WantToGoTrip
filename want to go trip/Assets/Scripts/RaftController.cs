using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftController : MonoBehaviour
{
    public PlayerInput input;
    public Transform raftTransform;
    public PlayerState playerState;
    public Transform PlayerTransform;

    public float maxVelocity = 5f;
    public float damping = 1f;
    public float forwardVelocity = 3f;
    public float sailingTime = 0.5f;
    public float rotationUnit = 5f;
    public float rotationSpeed = 1f;
    public float speed;
    public float backwardSpeed=10f;

    public static Vector3 velocity; //raft's velocity
    private float timeAfterOneSailing = 0f;
    private Vector3 targetAngle = Vector3.zero;
    private Vector3 targetVelocity;
    private Vector3 Velocity; //raft's forward velocity

    private void Start()
    {
        raftTransform = GetComponent<Transform>();
        velocity = Vector3.zero;
    }

    private void Update()
    {
        // 자동으로 뒤로 가는 기능
        // rotate raft
        raftTransform.rotation = Quaternion.Slerp(raftTransform.rotation, Quaternion.Euler(targetAngle)
            , Time.deltaTime * rotationSpeed);

        // move raft forward
        if (Mathf.Abs(Velocity.magnitude) >= forwardVelocity / 2)
            targetVelocity = Vector3.zero;
        Velocity = Vector3.Lerp(Velocity, targetVelocity, Time.deltaTime * damping);
        velocity = Vector3.back * backwardSpeed + Velocity;
        raftTransform.position += velocity * Time.deltaTime; // move forward;

        if (!playerState.isSailing)
        {
            return; // if player's state is not sailing then player can't control raft
        }

        // while player is sailing

        if (timeAfterOneSailing <= sailingTime)
            timeAfterOneSailing += Time.deltaTime;

        // rotation
        if (input.a)
        {
            targetAngle.y -= rotationUnit;
        }
        else if (input.d)
        {
            targetAngle.y += rotationUnit;
        }

        // move forward
        if (input.w && timeAfterOneSailing >= sailingTime)
        {
            if (targetVelocity.magnitude <= maxVelocity)
            {
                targetVelocity += raftTransform.forward * forwardVelocity;
            }
            timeAfterOneSailing = 0f;
        }


        //if (Mathf.Abs(velocity.magnitude) >= forwardVelocity / 2)
        //    targetVelocity = Vector3.zero;

        //velocity = Vector3.Lerp(velocity, targetVelocity, Time.deltaTime * damping);
        //raftTransform.position += velocity * Time.deltaTime;

        PlayerTransform.SetPositionAndRotation(raftTransform.position, raftTransform.rotation);


    }

}
    