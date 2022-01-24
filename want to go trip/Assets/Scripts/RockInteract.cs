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
    
    [SerializeField] Item item;

    private void Start()
    {
        rockAudioPlayer = GetComponent<AudioSource>();
        rockRigidbody = GetComponent<Rigidbody>();
        movingBackward = GetComponent<MovingBackward>();

        // Disable parent game object
        onDeath += () => { gameObject.SetActive(false); };
        onDeath += () => { UIManager.instance.AcquireItem(item); };
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
                OnDamage(health, collision.GetContact(0).point, collision.GetContact(0).normal);
            }
        }
    }

    public override void OnDamage(float damage, Vector3 hitPosition, Vector3 hitNormal)
    {
        if (!dead)
        {
            EffectManager.instance.ActivateEffect(EffectManager.EffectType.mining, hitPosition, Quaternion.Euler(hitNormal), Vector3.one);
            rockAudioPlayer.PlayOneShot(hitSound);
        }
        base.OnDamage(damage, hitPosition, hitNormal);
    }

    public override void Die(Vector3 hitPosition)
    {
        base.Die();
        EffectManager.instance.ActivateEffect(EffectManager.EffectType.rockDest, hitPosition, Quaternion.identity, Vector3.one);
    }
}
