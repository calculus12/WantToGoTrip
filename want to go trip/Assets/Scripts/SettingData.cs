using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingData : MonoBehaviour
{
    public Vector2 defaultMouseSensitivity;
    [Range(0f, 1f)] public float audioVolume;
    [Range(0f, 1f)] public float mouseSensitivity;

    static SettingData _instance;

    public static SettingData instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SettingData>();
            }
            return _instance;
        }
    }

    void Awake() {
        if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
