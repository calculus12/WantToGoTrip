using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftHealth : HealthEntity
{
    public AudioClip collisionSound;
    private CrashManager crashManager;
    private AudioSource raftAudioPlayer;

    private void Start()
    {
        raftAudioPlayer = GetComponent<AudioSource>();
    }

    private void Update()
    {
        UIManager.instance.UpdateRaftHealth(health / startingHealth);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        crashManager = GetComponent<CrashManager>();
        UIManager.instance.UpdateRaftHealth(health /startingHealth);
    }

    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        if (!dead)
        {
            raftAudioPlayer.PlayOneShot(collisionSound, 0.5f);
        }
        base.OnDamage(damage);
        crashManager.Generate();
    }
}
