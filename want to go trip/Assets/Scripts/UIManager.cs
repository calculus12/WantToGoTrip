using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Vector2 defaultMouseSensitivity;
    [Range(0f, 1f)] public float audioVolume;
    [Range(0f, 1f)] public float mouseSensitivity;

    public Image playerHealthSlider;
    public Image playerOxygenSlider;
    public Image raftHealthSlider;
    public GameObject settingUI;
    public GameObject gameoverUI;
    public GameObject raftHpUI;

    // objects having to do with Gameover UI
    [SerializeField] Image gameoverBackground;
    [SerializeField] TextMeshProUGUI gameoverText;
    [SerializeField] GameObject gameoverButtons;
 
    public GameObject pauseUI;

    static UIManager m_instance;
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

    public void RotateRaftHP(Vector3 lookAt)
    {
        // Boat Hp bar rotates, so player can see it
        raftHpUI.transform.LookAt(raftHpUI.transform.position + lookAt);
    }

    public void UpdateHealth(float healthRatio)
    {
        // healthRatio : 0 ~ 1
        playerHealthSlider.fillAmount = healthRatio;
    }

    public void UpdateOxygen(float oxygenRatio)
    {
        playerOxygenSlider.fillAmount = oxygenRatio;
    }

    public void UpdateRaftHealth(float raftHealthRatio)
    {
        raftHealthSlider.fillAmount = raftHealthRatio;
    }

    public void SetActivePauseUI(bool active)
    {
        if (!active)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
        pauseUI.SetActive(active);
    }

    public void SetActiveGameoverUI()
    {
        Cursor.lockState = CursorLockMode.None;
        StartCoroutine(gameoverUIFadeIn());
        gameoverUI.SetActive(true);
    }

    IEnumerator gameoverUIFadeIn()
    {
        // Fade in background until alpha <= 0.4
        // Fade in text until alpha <= 1
        // Fading of background and text must be finihsed at the same time
        while (gameoverBackground.color.a < 0.4f)
        {
            Color backgroundColor = gameoverBackground.color;
            Color textColor = gameoverText.color;
            backgroundColor.a += 0.01f;
            textColor.a += 0.025f;
            gameoverBackground.color = backgroundColor;
            gameoverText.color = textColor;
            yield return new WaitForSeconds(0.02f);
        }
        // After fading, activate buttons
        gameoverButtons.SetActive(true);
        Time.timeScale = 0f;
    }

    public void SetActiveSetting(bool active)
    {
        settingUI.SetActive(active);
    }

    public void GameRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }

    public void GoStartMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }

}
