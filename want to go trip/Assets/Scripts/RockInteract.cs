using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockInteract : HealthEntity
{
    // [SerializeField] ParticleSystem hitEffect;
    // [SerializeField] AudioClip destroySound;
    [SerializeField] AudioClip hitSound;

    private AudioSource rockAudioPlayer;

    private void Start()
    {
        rockAudioPlayer = GetComponent<AudioSource>();
        onDeath += () => { gameObject.SetActive(false); };
    }

    public void Setup(float newHealth)
    {
        startingHealth = newHealth;
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

    public override void Die()
    {
        base.Die();
        // rockAudioPlayer.PlayOneShot(destroySound);
    }
}
