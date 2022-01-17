using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class UIInGameMenu : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] GameObject menu;
    [SerializeField] GameObject setting;
    [SerializeField] TextMeshProUGUI volumeRate;
    [SerializeField] TextMeshProUGUI mouseSensitivityRate;

    void Update()
    {
        // Open and close in-game menu
        if (playerInput.esc)
        {
            if (menu.activeSelf)
            {
                UIManager.instance.SetActiveInGameMenu(false);
            }
            else
            {
                UIManager.instance.SetActiveInGameMenu(true);
            }
        }
    }

    public void OnClickContinue()
    {
        UIManager.instance.SetActiveInGameMenu(false);
    }

    public void OnClickRestart()
    {
        UIManager.instance.GameRestart();
    }

    public void OnClickSetting()
    {
        if (setting.activeSelf)
        {
            UIManager.instance.SetActiveInGameSetting(false);
        }
        else
        {
            UIManager.instance.SetActiveInGameSetting(true);
        }
    }

    public void OnClickExit() {
        UIManager.instance.GoStartMenu();
    }

    // Change audio volume
    public void OnSliderAudioVolume(float value)
    {
        volumeRate.text = $"{value * 100:F1}%";
        UIManager.instance.UpdateAudioVolumeData(value);
    }

    // Change mouse sensitivity
    public void OnSliderMouseSensitivity(float value)
    {
        mouseSensitivityRate.text = $"{value * 100:F1}%";
        UIManager.instance.UpdateMouseSensitivityData(value);
    }
}
