using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class UIManager : MonoBehaviour
{
    [HideInInspector] public bool isPlaying; // From game start to game over 
    [HideInInspector] public AudioSource[] audioList;
    [SerializeField] Slider startAudioController;
    [SerializeField] Slider inGameAudioController;
    [SerializeField] Slider mouseSensitivityController;

    [SerializeField] GameObject startMenu;
    [SerializeField] GameObject inGameMenu;
    [SerializeField] GameObject startSettingUI;
    [SerializeField] GameObject inGameSettingUI;
    [SerializeField] GameObject gameoverUI;
    [SerializeField] GameObject playerHpUI;
    [SerializeField] GameObject raftHpUI;
    [SerializeField] GameObject nearDeadzoneUI;
    [SerializeField] Inventory inventory;
    [SerializeField] Image playerHealthSlider;
    [SerializeField] Image playerOxygenSlider;
    [SerializeField] Image raftHealthSlider;

    // objects having to do with Gameover UI
    [SerializeField] Image gameoverBackground;
    [SerializeField] TextMeshProUGUI gameoverText;
    [SerializeField] GameObject gameoverButtons;
    
    [SerializeField] CinemachineBrain cinemachineBrain;
    [SerializeField] CinemachineFreeLook cinemachineFreeLook;
    [SerializeField] Vector3 zoomInPos;
    [SerializeField] Vector3 zoomInRot;

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

    void Awake()
    {
        audioList = FindObjectsOfType<AudioSource>();

        // Initialize audio volume
        for (int i = 0; i < audioList.Length; i++)
        {
            audioList[i].volume = SettingData.instance.audioVolume;
        }

        // Initialize mouse sensitivity
        cinemachineFreeLook.m_XAxis.m_MaxSpeed = SettingData.instance.defaultMouseSensitivity.x * SettingData.instance.mouseSensitivity;
        cinemachineFreeLook.m_YAxis.m_MaxSpeed = SettingData.instance.defaultMouseSensitivity.y * SettingData.instance.mouseSensitivity;
    }

    public void RotateRaftHP(Vector3 lookAt)
    {
        // Boat Hp bar rotates, so player can see it
        raftHpUI.transform.LookAt(raftHpUI.transform.position + lookAt);
    }

    public void UpdateHealth(float healthRatio)
    {
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

    public void GameStart()
    {
        startMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(ZoomIn());
    }

    IEnumerator ZoomIn()
    {
        while (Vector3.Distance(cinemachineBrain.transform.localPosition, zoomInPos) > 0.1f || Vector3.Distance(cinemachineBrain.transform.localEulerAngles, zoomInRot) > 0.1f)
        {
            yield return null;
            cinemachineBrain.transform.localPosition = Vector3.Lerp(cinemachineBrain.transform.localPosition, zoomInPos, Time.deltaTime * 5f);
            cinemachineBrain.transform.localEulerAngles = Vector3.Lerp(cinemachineBrain.transform.localEulerAngles, zoomInRot, Time.deltaTime * 5f);
        }
        cinemachineBrain.transform.localPosition = zoomInPos;
        cinemachineBrain.transform.localEulerAngles = zoomInRot;
        cinemachineBrain.enabled = true;

        playerHpUI.SetActive(true);
        raftHpUI.SetActive(true);
        inventory.gameObject.SetActive(true);
        isPlaying = true;
    }

    public void SetActiveInGameMenu(bool active)
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
        inGameMenu.SetActive(active);
    }

    public void SetActiveStartSetting(bool active)
    {
        startAudioController.value = SettingData.instance.audioVolume;
        startSettingUI.SetActive(active);
    }

    public void SetActiveInGameSetting(bool active)
    {
        inGameAudioController.value = SettingData.instance.audioVolume;
        mouseSensitivityController.value = SettingData.instance.mouseSensitivity;
        inGameSettingUI.SetActive(active);
    }

    public void UpdateAudioVolumeData(float value)
    {
        for (int i = 0; i < audioList.Length; i++)
        {
            audioList[i].volume = value;
        }
        SettingData.instance.audioVolume = value;
    }

    public void UpdateMouseSensitivityData(float value)
    {
        cinemachineFreeLook.m_XAxis.m_MaxSpeed = SettingData.instance.defaultMouseSensitivity.x * value;
        cinemachineFreeLook.m_YAxis.m_MaxSpeed = SettingData.instance.defaultMouseSensitivity.y * value;
        SettingData.instance.mouseSensitivity = value;
    }

    public void GameRestart()
    {
        Time.timeScale = 1f;
        UILoading.instance.LoadScene("Main", true);
    }

    public void SetActiveGameoverUI()
    {
        isPlaying = false;
        Cursor.lockState = CursorLockMode.None;
        gameoverUI.SetActive(true);
        StartCoroutine(gameoverUIFadeIn());
    }
    public void SetDeadzoneAlarmUI()
    {

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

    public void GoStartMenu()
    {
        Time.timeScale = 1f;
        UILoading.instance.LoadScene("Main", false);
    }

    public void AcquireItem(Item _item)
    {
        inventory.AcquireItem(_item);
    }
}
