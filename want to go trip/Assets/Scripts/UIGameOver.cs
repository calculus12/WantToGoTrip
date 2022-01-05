using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIGameOver : MonoBehaviour
{
    [SerializeField] Image background;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] GameObject button;

    void Start() {
        StartCoroutine(FadeIn());
    }

    public void OnClickRestart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }

    public void OnClickExit() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("StartMenu");
    }

    IEnumerator FadeIn()
    {
        // Fade in background until alpha <= 0.4
        // Fade in text until alpha <= 1
        // Fading of background and text must be finihsed at the same time
        while (background.color.a < 0.4f)
        {
            Color backgroundColor = background.color;
            Color textColor = text.color;
            backgroundColor.a += 0.01f;
            textColor.a += 0.025f;
            background.color = backgroundColor;
            text.color = textColor;
            yield return new WaitForSeconds(0.02f);
        }
        // After fading, activate buttons
        button.SetActive(true);
        Time.timeScale = 0f;
    }
}
