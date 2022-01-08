using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIStartMenu : MonoBehaviour
{
    [HideInInspector] public AudioSource[] audioList;

    [SerializeField] GameObject setting;
    [SerializeField] TextMeshProUGUI volumeRate;
    [SerializeField] Slider audioController;


    void Awake()
    {
        // Set audio volume
        audioList = FindObjectsOfType<AudioSource>();
        for (int i = 0; i < audioList.Length; i++)
        {
            audioList[i].volume = UIManager.instance.audioVolume;
        }
        audioController.value = UIManager.instance.audioVolume;
    }
    
    public void OnClickPlay()
    {
        UILoading.instance.LoadScene("Main");
    }

    public void OnClickSetting()
    {
        if (setting.activeSelf)
        {
            setting.SetActive(false);
        }
        else
        {
            setting.SetActive(true);
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
        for (int i = 0; i < audioList.Length; i++)
        {
            audioList[i].volume = value;
        }
        UIManager.instance.audioVolume = value;
    }
}
