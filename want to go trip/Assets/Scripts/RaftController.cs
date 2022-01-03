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

    public float maxVelocity = 10f;
    public float forwardVelocity = 3f;
    public float friction = 1f;
    public float sailingTime = 0.5f;
    public float rotationSpeed = 5f;

    private Vector3 _velocity;
    private float timeAfterOneSailing = 0f;
    private Vector3 targetAngle;

    private void Start()
    {
        raftTransform = GetComponent<Transform>();
        _velocity = Vector3.zero;
    }

    private void Update() // need to make movement of raft more smoothly
    {
        if (!playerState.isSailing)
        {
            return;
        }

        timeAfterOneSailing += Time.deltaTime;

        if (input.w && timeAfterOneSailing >= sailingTime)
        {
            if (_velocity.magnitude <= maxVelocity)
            {
                _velocity += raftTransform.forward * forwardVelocity; // it is not smooth
            }
            timeAfterOneSailing = 0f;
        }

        if (input.a)
        {
            targetAngle.y -= rotationSpeed;
        }
        else if (input.d)
        {
            targetAngle.y += rotationSpeed;
        }


        if (_velocity.magnitude >= 0.1f)
        {
            _velocity -= raftTransform.forward * friction;
        }


        raftTransform.position += _velocity * Time.deltaTime;
        raftTransform.rotation = Quaternion.Slerp(raftTransform.rotation, Quaternion.Euler(targetAngle) 
            , 0.1f);
        PlayerTransform.position += _velocity * Time.deltaTime;
        PlayerTransform.rotation = Quaternion.Slerp(PlayerTransform.rotation, Quaternion.Euler(targetAngle)
            , 0.1f);
    }

}
    