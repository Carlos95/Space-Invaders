using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveManager
{
    public static void SaveInt(string key, int value) => PlayerPrefs.SetInt(key, value);
    public static int LoadInt(string key) => PlayerPrefs.GetInt(key);
    public static void SaveString(string key, string value) => PlayerPrefs.SetString(key, value);
    public static string LoadString(string key) => PlayerPrefs.GetString(key);
}
