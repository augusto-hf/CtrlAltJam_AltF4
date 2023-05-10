using System.IO;
using System;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    const string FILE_DEFAULT_SAVE_GAME = "Data/SaveGame/DefaultSave";
    const string FILE_DEFAULT_SAVE_CONFIG = "Data/SaveGame/DefaultConfig";
    const string FILE_SAVE = "/save.json"; 
    const string FILE_CONFIG = "/config.json"; 

    public GameData gameData;
    public ConfigData configData;

    private string filePathSave;
    private string filePathConfig;

    private void Awake() 
    {
        filePathSave = Application.persistentDataPath + FILE_SAVE; 
        filePathConfig = Application.persistentDataPath + FILE_CONFIG; 

        Debug.Log("Local de salvamento: " + filePathSave); 

        LoadConfig();
    }

    public void ResetConfig()
    {
        if (File.Exists(filePathConfig)) 
        {
            File.Delete(filePathConfig); 
        }

        configData = LoadDefaultSave(FILE_DEFAULT_SAVE_CONFIG, configData);
    }

    public void LoadConfig()
    {
        if (File.Exists(filePathConfig)) 
        {
            string json = File.ReadAllText(filePathConfig);
            configData = JsonConvert.DeserializeObject<ConfigData>(json); 
        }
        else
        { 
            configData = LoadDefaultSave(FILE_DEFAULT_SAVE_CONFIG, configData);
        }

        configData.currentLanguage = LocalizationManager.localizationInstance.SetNewLanguage(configData.currentLanguage);
        SaveConfig();

    }

    public void SaveConfig() 
    {
        Debug.Log("Save Config");
        string json = JsonUtility.ToJson(configData, true); 
        File.WriteAllText(filePathConfig, json); 
    }




    public void ApplyPositionInPlayer(Transform positionPlayer)
    {
        Debug.Log("Apply Position");
        positionPlayer.position = gameData.positionPlayer;
    }

    public void SaveNewEmotion(string nameEmotion)
    {
        string findEmotion = gameData.emotions.Find(x => x == nameEmotion);

        if (findEmotion == null)
        {
            gameData.emotions.Add(nameEmotion);
            Debug.Log("Narração");
        }

        Save();
    }

    public void SavePositionPlayer(Transform positionPlayer)
    {
        Debug.Log("Save Position");
        gameData.positionPlayer = positionPlayer.position;

        Save();
    }

    public void Save() 
    {
        Debug.Log("Save Game");
        string json = JsonUtility.ToJson(gameData, true); 
        File.WriteAllText(filePathSave, json); 
    }

    public void Load() 
    {
        if (File.Exists(filePathSave)) 
        {
            string json = File.ReadAllText(filePathSave);
            gameData = JsonConvert.DeserializeObject<GameData>(json); 
        }
        else
        { 
            gameData = LoadDefaultSave(FILE_DEFAULT_SAVE_GAME, gameData);
        }
    }

    public void NewGame() 
    {
        if (File.Exists(filePathSave)) 
        {
            File.Delete(filePathSave); 
        }

        gameData = LoadDefaultSave(FILE_DEFAULT_SAVE_GAME, gameData);
    }
    
    private T LoadDefaultSave<T>(string path, T data)
    {
        TextAsset jsonTextAsset = Resources.Load<TextAsset>(path); 
        string json = jsonTextAsset.text; 
        data = JsonConvert.DeserializeObject<T>(json); 

        return data;
    }
}
