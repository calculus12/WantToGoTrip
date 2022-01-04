using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGameSetting : MonoBehaviour
{
    [SerializeField] InGameMenu menu;
    [SerializeField] TextMeshProUGUI volumeRate;
    [SerializeField] Slider audioController;

    void Awake()
    {
        audioController.value = UIManager.instance.audioVolume;
    }

    // Change audio volume
    public void OnSliderEvent(float value)
    {
        volumeRate.text = $"{value * 100:F1}%";
        for (int i = 0; i < menu.audioList.Length; i++)
        {
            menu.audioList[i].volume = value;
        }
        UIManager.instance.audioVolume = value;
    }
}
