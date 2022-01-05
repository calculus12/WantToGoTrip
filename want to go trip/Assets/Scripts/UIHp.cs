using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHp : MonoBehaviour
{
    [SerializeField] Image playerHp;
    [SerializeField] Image boatHp;
    [SerializeField] Image playerOxygen;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] Transform cam;
    [SerializeField] Transform boatHpCanvas;
    [SerializeField] PlayerState playerState;
    float underwaterTimer;
    float abovewaterTimer;

    void Awake()
    {
        // Initialize HP to max
        UIManager.instance.playerHp = UIManager.instance.playerMaxHp;
        UIManager.instance.boatHp = UIManager.instance.boatMaxHp;
        UIManager.instance.playerOxygen = UIManager.instance.maxOxygen;
        underwaterTimer = UIManager.instance.oxygenDamageInterval;
        abovewaterTimer = UIManager.instance.oxygenRecoveryInterval;
    }

    void Update()
    {
        // Represent HP amount to HP bar
        playerHp.fillAmount = UIManager.instance.playerHp / 100f;
        boatHp.fillAmount = UIManager.instance.boatHp / 100f;
        playerOxygen.fillAmount = UIManager.instance.playerOxygen / 100f;
        
        // If HP <= 0, game over
        if (playerHp.fillAmount <= 0 || boatHp.fillAmount <= 0 || playerOxygen.fillAmount <= 0)
        {
            gameOverUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }

        // If player is under water, decrease oxygen rate
        // If player is above water, increase oxygen rate
        if (playerState.isUnderwater)
        {
            abovewaterTimer = 0f;
            if (underwaterTimer <= 0f)
            {
                UIManager.instance.playerOxygen -= UIManager.instance.oxygenDamage;
                underwaterTimer = UIManager.instance.oxygenDamageInterval;
            }
            else
            {
                underwaterTimer -= Time.deltaTime;
            }
        }
        else
        {
            underwaterTimer = UIManager.instance.oxygenDamageInterval;
            if (abovewaterTimer <= 0f)
            {
                UIManager.instance.playerOxygen += UIManager.instance.oxygenRecovery;
                abovewaterTimer = UIManager.instance.oxygenRecoveryInterval;
            }
            else
            {
                abovewaterTimer -= Time.deltaTime;
            }
            
        }
    }
    
    void LateUpdate()
    {
        // Boat Hp bar rotates, so player can see it
        boatHpCanvas.LookAt(boatHpCanvas.position + cam.forward);
    }
}
