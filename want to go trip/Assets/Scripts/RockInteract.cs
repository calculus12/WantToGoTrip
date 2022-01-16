using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockInteract : HealthEntity
{
    [SerializeField] ParticleSystem hitEffect;
    // [SerializeField] AudioClip destroySound;
    [SerializeField] AudioClip hitSound;
    public float damageOnRaft = 15f;

    private AudioSource rockAudioPlayer;
    private Rigidbody rockRigidbody;
    private MovingBackward movingBackward;

    private void Start()
    {
        rockAudioPlayer = GetComponent<AudioSource>();
        rockRigidbody = GetComponent<Rigidbody>();
        movingBackward = GetComponent<MovingBackward>();

        // Disable parent game object
        onDeath += () => { gameObject.SetActive(false); };
    }

    public void Setup(float newHealth, float newDamage)
    {
        startingHealth = newHealth;
        damageOnRaft = newDamage;
    }


    // Collision with raft
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ContactPoint cp = collision.GetContact(0);
            PlayerMovement playerMovement = collision.collider.GetComponent<PlayerMovement>();
            Vector3 normal = -cp.normal;
            normal.y = 0f;
            playerMovement.GetForce(normal);
        }
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
