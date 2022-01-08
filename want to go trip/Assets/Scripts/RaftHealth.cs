using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftHealth : HealthEntity
{
    protected override void OnEnable()
    {
        base.OnEnable();

        UIManager.instance.UpdateRaftHealth(health /startingHealth);
    }
}
