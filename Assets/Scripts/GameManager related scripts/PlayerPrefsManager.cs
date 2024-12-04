using System;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    public static PlayerPrefsManager PPManager;

    private void Awake()
    {
        PPManager = this;
    }


    public void SaveFloatValue(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }

    public void SaveStringValue(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    public float LoadFloatValue(string key, float defaultValue = 0f)
    {
        if (PlayerPrefs.HasKey(key))
        {
            float loadedValue = PlayerPrefs.GetFloat(key);
            return loadedValue;
        }
        return defaultValue;

    }

    public string LoadStringValue(string key, string defaultValue = "")
    {
        if (PlayerPrefs.HasKey(key))
        {
            string loadedValue = PlayerPrefs.GetString(key);
            return loadedValue;
        }
        return defaultValue;

    }


    public void DeleteValue(string key)
    {
        if (PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.DeleteKey(key);
        }
    }
}