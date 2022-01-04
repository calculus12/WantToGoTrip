using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    static UIManager m_instance;
    int currentPlayerMaxHp = 100;
    int currentBoatMaxHp = 100;
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
    
    public int playerMaxHp
    {
        get
        {
            return currentPlayerMaxHp;
        }
        set
        {
            if (value < 0)
            {
                currentPlayerMaxHp = 0;
            }
            else
            {
                currentPlayerMaxHp = value;
            }
        }
    }
    
    public int boatMaxHp
    {
        get
        {
            return currentBoatMaxHp;
        }
        set
        {
            if (value < 0)
            {
                currentBoatMaxHp = 0;
            }
            else
            {
                currentBoatMaxHp = value;
            }
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
            else if (value > currentPlayerMaxHp)
            {
                currentPlayerHp = currentPlayerMaxHp;
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
            else if (value > currentBoatMaxHp)
            {
                currentBoatHp = currentBoatMaxHp;
            }
            else
            {
                currentBoatHp = value;
            }
        }
    }

    public float audioVolume { get; set; } = 1f;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
