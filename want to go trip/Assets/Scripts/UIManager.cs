using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager m_instance; // singletone variable
    public static UIManager instance // singletone property
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

    public float audioVolume = 1f;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

}
