using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Transform raftRopeTransform;
    public Transform playerRopeTransform;

    private float curTime;
    private float timeL = 1f;

    private void Update()
    {
        float distance;
        distance = Vector3.Distance(raftRopeTransform.position, playerRopeTransform.position);
        curTime += Time.deltaTime;
        if (curTime >= timeL)
        {
            curTime = 0f;
            Debug.Log(distance);    
        }
    }
}
