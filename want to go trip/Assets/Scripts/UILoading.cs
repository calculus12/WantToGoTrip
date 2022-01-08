using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UILoading : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] Image progressBar;
    [SerializeField] TextMeshProUGUI progressRate;
    static UILoading l_instance;
    string nextScene;

    public static UILoading instance
    {
        get
        {
            if (l_instance == null)
            {
                var obj = FindObjectOfType<UILoading>();
                if (obj != null)
                {
                    l_instance = obj;
                }
                else
                {
                    l_instance = Instantiate(Resources.Load<UILoading>("UILoading"));
                }
            }
            return l_instance;
        }
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        gameObject.SetActive(true);
        SceneManager.sceneLoaded += OnSceneLoaded;
        nextScene = sceneName;
        StartCoroutine(LoadSceneProgress());
    }

    // Fade out when scene is 100% loaded
    void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        StartCoroutine(Fade(false));
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    IEnumerator LoadSceneProgress()
    {
        yield return StartCoroutine(Fade(true));
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        // Fill progress bar
        float timer = 0f;
        while (!op.isDone)
        {
            yield return null;
            if (op.progress < 0.9f)
            {
                progressBar.fillAmount = op.progress;
                progressRate.text = $"{progressBar.fillAmount * 100:F1}%";
            }
            else
            {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                progressRate.text = $"{progressBar.fillAmount * 100:F1}%";
                if (progressBar.fillAmount >= 1f)
                {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }
        }
    }

    IEnumerator Fade(bool isFadeIn)
    {
        float timer = 0f;
        while (timer < 1f)
        {
            yield return null;
            timer += Time.unscaledDeltaTime * 3f;
            if (isFadeIn)
            {
                canvasGroup.alpha = Mathf.Lerp(0f, 1f, timer);
            }
            else
            {
                canvasGroup.alpha = Mathf.Lerp(1f, 0f, timer);
            }
        }

        if (!isFadeIn)
        {
            gameObject.SetActive(false);
        }
    }
}
