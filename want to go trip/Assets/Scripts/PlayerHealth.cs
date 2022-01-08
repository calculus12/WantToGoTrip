using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HealthEntity
{
    public AudioClip hitSound;
    public AudioClip deathSound;
   
    public float oxygen { get; set; }
    public float oxygenTime = 20f;
    public float startingOxygen = 100f;
    public float oxygenRecoverySpeed = 2f;

    private PlayerState state;
    private AudioSource playerAudioPlayer;
    private Animator playerAnimator;
    private PlayerMovement playerMovement;
    private PlayerLumbering playerLumbering;


    private void Start()
    {
        state = GetComponent<PlayerState>();
        playerAudioPlayer = GetComponent<AudioSource>();
        playerAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerLumbering = GetComponent<PlayerLumbering>();
        oxygen = startingOxygen;
    }

    private void Update()
    {
        // update player's oxygen

        //oxygen = Mathf.Lerp(oxygen, startingOxygen, Time.deltaTime * oxygenRecoverySpeed);
        //if (state.isSubmerging)
        //{
        //    oxygen = Mathf.Lerp(oxygen, 0, Time.deltaTime);
        //}
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
