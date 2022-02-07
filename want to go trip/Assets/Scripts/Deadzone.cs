using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadzone : RaftHealth
{

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Dead" && !dead)
        {
            OnDamage(100f);
        }
    }
}
