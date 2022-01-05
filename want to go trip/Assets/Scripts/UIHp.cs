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

    void Awake()
    {
        // Initialize HP to max
        UIManager.instance.playerHp = UIManager.instance.playerMaxHp;
        UIManager.instance.boatHp = UIManager.instance.boatMaxHp;
        UIManager.instance.playerOxygen = UIManager.instance.playerMaxOxygen;
        underwaterTimer = UIManager.instance.underwaterEndurance;
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

        // If player is in underwater, decrease oxygen rate
        if (playerState.isUnderwater)
        {
            if (underwaterTimer < 0f)
            {
                UIManager.instance.playerOxygen -= UIManager.instance.underwaterDamage;
                underwaterTimer = UIManager.instance.underwaterEndurance;
            }
            else
            {
                underwaterTimer -= Time.deltaTime;
            }
        }
        else
        {
            underwaterTimer = UIManager.instance.underwaterEndurance;
        }
    }
    
    void LateUpdate()
    {
        // Boat Hp bar rotates, so player can see it
        boatHpCanvas.LookAt(boatHpCanvas.position + cam.forward);
    }
}
