using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deadzone: MonoBehaviour
{
    [SerializeField] RaftHealth raftHealth;
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Dead")
        {
            raftHealth.OnDamage(raftHealth.startingHealth);
        }
    }
}
