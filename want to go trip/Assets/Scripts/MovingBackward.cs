using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackward : MonoBehaviour
{
    public float speed;
    RandObjGenerator randObjGenerator;
    
    public void Awake()
    {
        randObjGenerator = FindObjectOfType<RandObjGenerator>();
    }

    public void Update()
    {
        transform.position += Vector3.back * speed * Time.deltaTime;
        if (transform.position.z < -400f)
        {
            randObjGenerator.DeactivateObj(gameObject);
        }
    }
}
   