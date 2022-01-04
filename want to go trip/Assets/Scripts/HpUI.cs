using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpUI : MonoBehaviour
{
    [SerializeField] Image playerHp;
    [SerializeField] Image boatHp;
    [SerializeField] GameObject gameOverUI;

    void Awake()
    {
        // Initialize HP to max
        UIManager.instance.playerHp = UIManager.instance.playerMaxHp;
        UIManager.instance.boatHp = UIManager.instance.boatMaxHp;
    }

    void Update()
    {
        // Represent HP amount to HP bar
        playerHp.fillAmount = UIManager.instance.playerHp / 100f;
        boatHp.fillAmount = UIManager.instance.boatHp / 100f;
        
        // if HP <= 0, game over
        if (playerHp.fillAmount <= 0 || boatHp.fillAmount <= 0)
        {
            gameOverUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
