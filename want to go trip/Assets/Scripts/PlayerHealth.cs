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
    public float oxygenRecoverySpeed = 6f;
    public float oxygenDecreaseSpeed = 2f;
    public float healthDecreasingByNoOxygen = 3f;
    public SailingControl sailing;

    private PlayerState state;
    private AudioSource playerAudioPlayer;
    private Animator playerAnimator;
    private PlayerMovement playerMovement;
    private PlayerLumbering playerLumbering;


    private void Awake()
    {
        state = GetComponent<PlayerState>();
        playerAudioPlayer = GetComponent<AudioSource>();
        playerAnimator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerLumbering = GetComponent<PlayerLumbering>();
        oxygen = startingOxygen;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        UIManager.instance.UpdateHealth(health / startingHealth);
        UIManager.instance.UpdateOxygen(oxygen / startingOxygen);

        playerMovement.enabled = true;
        playerLumbering.enabled = true;
        sailing.enabled = true;
    }

    private void Update()
    {
        if (dead)
            return;

        if (oxygen <= 0f)
        {
            OnDamage(Time.deltaTime * healthDecreasingByNoOxygen);
        }

        if (state.isSubmerging || state.isSurface || state.isUnderwater)
        {
            oxygen = Mathf.Clamp(oxygen - Time.deltaTime * oxygenDecreaseSpeed, 0, startingOxygen);
        }
        else
        {
            oxygen = Mathf.Clamp(oxygen + Time.deltaTime * oxygenRecoverySpeed, 0, startingOxygen);
        }
        UIManager.instance.UpdateOxygen(oxygen / startingOxygen);
        UIManager.instance.UpdateHealth(health / startingHealth);
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
        sailing.enabled = false;
    }
}
