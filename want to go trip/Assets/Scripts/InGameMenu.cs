using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour
{
    [SerializeField]
    GameObject menu;

    [SerializeField]
    GameObject setting;

    public AudioSource[] audioList;

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
        if (Input.GetButtonDown("Cancel"))
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
