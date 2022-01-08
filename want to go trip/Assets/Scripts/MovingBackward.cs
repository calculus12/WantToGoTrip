using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackward : MonoBehaviour
{
    public float speed;
    
    public void Update()
    {
        transform.position += Vector3.back * speed * Time.deltaTime;
    }
}
   