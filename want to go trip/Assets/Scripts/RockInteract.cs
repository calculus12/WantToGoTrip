using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockInteract : HealthEntity
{
    // [SerializeField] ParticleSystem hitEffect;
    // [SerializeField] AudioClip destroySound;
    [SerializeField] AudioClip hitSound;
    public LayerMask raftLayermask;
    public float damageOnRaft = 15f;

    private AudioSource rockAudioPlayer;

    private void Start()
    {
        rockAudioPlayer = GetComponent<AudioSource>();

        // Disable parent game object
        onDeath += () => { gameObject.transform.parent.gameObject.SetActive(false); };
    }

    public void Setup(float newHealth)
    {
        startingHealth = newHealth;
    }

    // Collision with raft
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.layer + ' ' + raftLayermask.value);
        if (collision.gameObject.layer == raftLayermask.value)
        {
            IDamageable raft = collision.collider.GetComponent<IDamageable>();

            if (raft != null)
            {
                raft.OnDamage(damageOnRaft);
            }
        }
    }

    public override void OnDamage(float damage, Vector3 hitPosition, Vector3 hitNormal)
    {
        if (!dead)
        {
            // hitEffect.Play();
            rockAudioPlayer.PlayOneShot(hitSound);
        }
        base.OnDamage(damage, hitPosition, hitNormal);
    }

    public override void Die()
    {
        base.Die();
        // rockAudioPlayer.PlayOneShot(destroySound);
    }
}
