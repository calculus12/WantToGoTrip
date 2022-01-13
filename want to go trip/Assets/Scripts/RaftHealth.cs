using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaftHealth : HealthEntity
{
    public AudioClip collisionSound;

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

        UIManager.instance.UpdateRaftHealth(health /startingHealth);
    }

    public override void OnDamage(float damage)
    {
        if (!dead)
        {
            raftAudioPlayer.PlayOneShot(collisionSound);
        }
        base.OnDamage(damage);
    }
}
