using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBackward : MonoBehaviour
{
    public float rockspeed;
    
    public void Update()
    {
        transform.Translate(Vector3.back * rockspeed * Time.deltaTime);
    }
}
   