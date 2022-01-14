using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockInteract : HealthEntity
{
    // [SerializeField] ParticleSystem hitEffect;
    // [SerializeField] AudioClip destroySound;
    [SerializeField] AudioClip hitSound;
    public float damageOnRaft = 15f;

    private AudioSource rockAudioPlayer;
    private Rigidbody rockRigidbody;

    private void Start()
    {
        rockAudioPlayer = GetComponent<AudioSource>();
        rockRigidbody = GetComponent<Rigidbody>();

        // Disable parent game object
        onDeath += () => { gameObject.SetActive(false); };
    }

    public void Setup(float newHealth)
    {
        startingHealth = newHealth;
    }

    // Collision with raft


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.collider.name);
        if (collision.gameObject.tag == "Raft")
        {
            IDamageable raft = collision.collider.transform.parent.GetComponent<IDamageable>();

            if (raft != null)
            {
                raft.OnDamage(damageOnRaft, collision.GetContact(0).point, collision.GetContact(0).normal);
                Die();
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
