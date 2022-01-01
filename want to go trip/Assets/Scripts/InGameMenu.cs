using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    [HideInInspector] public AudioSource[] audioList;

    [SerializeField] GameObject menu;
    [SerializeField] GameObject setting;
    [SerializeField] GameObject gameOverUI;

    void Awake()
    {
        // Set audio volume
        audioList = FindObjectsOfType<AudioSource>();
        for (int i = 0; i < audioList.Length; i++)
        {
            audioList[i].volume = UIManager.instance.audioVolume;
        }
    }

    void Update()
    {
        // Open and close in-game menu
        if (Input.GetButtonDown("Cancel") && !gameOverUI.activeSelf)
        {
            if (menu.activeSelf)
            {
                menu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                menu.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void OnClickContinue()
    {
        menu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnClickRestart()
    {
        SceneManager.LoadScene("Main");
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

    public void OnClickExit() {
        SceneManager.LoadScene("StartMenu");
    }
}
