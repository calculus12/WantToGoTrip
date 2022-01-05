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

    static UIManager m_instance;
    int currentPlayerHp = 100;
    int currentBoatHp = 100;

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

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
