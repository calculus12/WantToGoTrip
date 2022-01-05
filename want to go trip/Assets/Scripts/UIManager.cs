using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Vector2 defaultMouseSensitivity;
    [Range(0f, 1f)] public float audioVolume;
    [Range(0f, 1f)] public float mouseSensitivity;
    public int playerMaxHp;
    public int boatMaxHp;
    public int maxOxygen;
    public int oxygenDamage;
    public int oxygenDamageInterval;
    public int oxygenRecovery;
    public int oxygenRecoveryInterval;

    static UIManager m_instance;
    int currentPlayerHp;
    int currentBoatHp;
    int currentPlayerOxygen;

    public static UIManager instance
    {
        get
        {
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<UIManager>();
            }
            return m_instance;
        }
    }

    public int playerHp
    {
        get
        {
            return currentPlayerHp;
        }
        set
        {
            if (value < 0)
            {
                currentPlayerHp = 0;
            }
            else if (value > playerMaxHp)
            {
                currentPlayerHp = playerMaxHp;
            }
            else
            {
                currentPlayerHp = value;
            }
        }
    }
    
    public int boatHp
    {
        get
        {
            return currentBoatHp;
        }
        set
        {
            if (value < 0)
            {
                currentBoatHp = 0;
            }
            else if (value > boatMaxHp)
            {
                currentBoatHp = boatMaxHp;
            }
            else
            {
                currentBoatHp = value;
            }
        }
    }

    public int playerOxygen
    {
        get
        {
            return currentPlayerOxygen;
        }
        set
        {
            if (value < 0)
            {
                currentPlayerOxygen = 0;
            }
            else if (value > maxOxygen)
            {
                currentPlayerOxygen = maxOxygen;
            }
            else
            {
                currentPlayerOxygen = value;
            }
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
