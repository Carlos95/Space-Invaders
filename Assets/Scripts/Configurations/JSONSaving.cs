using System.IO;
using UnityEngine;

public class JSONSaving : MonoBehaviour
{
    private string path = "";
    private string persistentPath = "";
    private PlayerData playerData;
    private const string PlayerPrefsGuidKey = "LEADERBOARD_CREATOR___LOCAL_GUID";

    private void Awake()
    {
        SetPaths();
        if (LoadData() == null)
        {
            CreatePlayerData();
        }
    }

    private void SetPaths()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "SaveData.json";
    }

    private void CreatePlayerData()
    {
        playerData = new PlayerData("", 0,"");
        SaveData(playerData);
    }

    public void SaveData(PlayerData playerData)
    {
        string savePath = persistentPath;

        if (playerData.guid == "" && !string.IsNullOrEmpty(PlayerPrefs.GetString(PlayerPrefsGuidKey, "")))
        {
            playerData.guid = PlayerPrefs.GetString(PlayerPrefsGuidKey);
        }
        string json = JsonUtility.ToJson(playerData);

        using StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
    }

    public PlayerData LoadData()
    {
        try
        {
            using StreamReader reader = new StreamReader(persistentPath);
            string json = reader.ReadToEnd();
            PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);
            if (playerData.guid != "")
            {
                PlayerPrefs.SetString(PlayerPrefsGuidKey, playerData.guid);
            }
            return playerData;
           

            
        } catch
        {
            return null;
        }
    }
}
