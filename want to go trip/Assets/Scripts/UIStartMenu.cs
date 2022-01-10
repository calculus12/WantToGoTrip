using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIStartMenu : MonoBehaviour
{
    [SerializeField] GameObject setting;
    [SerializeField] TextMeshProUGUI volumeRate;

    public void OnClickPlay()
    {
        UIManager.instance.GameStart();
    }

    public void OnClickSetting()
    {
        if (setting.activeSelf)
        {
            UIManager.instance.SetActiveStartSetting(false);
        }
        else
        {
            UIManager.instance.SetActiveStartSetting(true);
        }
    }

    public void OnClickExit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    // Change audio volume
    public void OnSliderAudioVolume(float value)
    {
        volumeRate.text = $"{value * 100:F1}%";
        for (int i = 0; i < UIManager.instance.audioList.Length; i++)
        {
            UIManager.instance.audioList[i].volume = value;
        }
        GameManager.instance.audioVolume = value;
    }
}
