using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public void OnClickRestart()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnClickExit() {
        SceneManager.LoadScene("StartMenu");
    }
}