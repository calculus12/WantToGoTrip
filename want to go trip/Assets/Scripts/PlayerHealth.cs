using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthEntity
{
    public AudioClip hitSound;
    public AudioClip deathSound;

    private AudioSource playerAudioPlayer;
    private Animator playerAnimator;
    private PlayerMovement playerMovement;
    private PlayerLumbering playerLumbering;

    public void Start()
    {
        playerAudioPlayer = GetComponent<AudioSource>();
        playerAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerLumbering = GetComponent<PlayerLumbering>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        playerMovement.enabled = true;
        playerLumbering.enabled = true;
    }

    public override void RestoreHealth(float restoreHealth)
    {
        base.RestoreHealth(restoreHealth);
    }

    public override void Die()
    {
        base.Die();
        
        /*
         * play death animation
         */

        playerMovement.enabled = false;
        playerLumbering.enabled = false;
    }
}
