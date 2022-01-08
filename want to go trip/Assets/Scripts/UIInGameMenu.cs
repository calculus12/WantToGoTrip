using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class UIInGameMenu : MonoBehaviour
{
    [HideInInspector] public AudioSource[] audioList;

    [SerializeField] GameObject menu;
    [SerializeField] GameObject setting;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] TextMeshProUGUI volumeRate;
    [SerializeField] Slider audioController;
    [SerializeField] TextMeshProUGUI mouseSensitivityRate;
    [SerializeField] Slider mouseSensitivityController;
    [SerializeField] CinemachineFreeLook cinemachineFreeLook;

    void Awake()
    {
        // Set audio volume
        audioList = FindObjectsOfType<AudioSource>();
        for (int i = 0; i < audioList.Length; i++)
        {
            audioList[i].volume = UIManager.instance.audioVolume;
        }
        audioController.value = UIManager.instance.audioVolume;

        // Set mouse sensitivity
        cinemachineFreeLook.m_XAxis.m_MaxSpeed = UIManager.instance.defaultMouseSensitivity.x * UIManager.instance.mouseSensitivity;
        cinemachineFreeLook.m_YAxis.m_MaxSpeed = UIManager.instance.defaultMouseSensitivity.y * UIManager.instance.mouseSensitivity;
        mouseSensitivityController.value = UIManager.instance.mouseSensitivity;
    }

    void Update()
    {
        // Open and close in-game menu
        if (Input.GetButtonDown("Cancel") && !gameOverUI.activeSelf)
        {
            if (menu.activeSelf)
            {
                UIManager.instance.SetActivePauseUI(false);
            }
            else
            {
                UIManager.instance.SetActivePauseUI(true);
            }
        }
    }

    public void OnClickContinue()
    {
        UIManager.instance.SetActivePauseUI(false);
    }

    public void OnClickRestart()
    {
        UIManager.instance.GameRestart();
    }

    public void OnClickSetting()
    {
        if (setting.activeSelf)
        {
            UIManager.instance.SetActiveSetting(false);
        }
        else
        {
            UIManager.instance.SetActiveSetting(true);
        }
    }

    public void OnClickExit() {
        UIManager.instance.GoStartMenu();
    }

    // Change audio volume
    public void OnSliderAudioVolume(float value)
    {
        volumeRate.text = $"{value * 100:F1}%";
        for (int i = 0; i < audioList.Length; i++)
        {
            audioList[i].volume = value;
        }
        UIManager.instance.audioVolume = value;
    }

    // Change mouse sensitivity
    public void OnSliderMouseSensitivity(float value)
    {
        mouseSensitivityRate.text = $"{value * 100:F1}%";
        cinemachineFreeLook.m_XAxis.m_MaxSpeed = UIManager.instance.defaultMouseSensitivity.x * value;
        cinemachineFreeLook.m_YAxis.m_MaxSpeed = UIManager.instance.defaultMouseSensitivity.y * value;
        UIManager.instance.mouseSensitivity = value;
    }
}
