using System.IO;
using System;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public GameData gameData;

    private const string saveFileName = "/save.json"; 
    private string filePath;

    private void Awake() 
    {
        filePath = Application.persistentDataPath + saveFileName; 

        Debug.Log("Local de salvamento: " + filePath); 
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
        File.WriteAllText(filePath, json); 
    }

    public void Load() 
    {

        if (File.Exists(filePath)) 
        {
            string json = File.ReadAllText(filePath);
            gameData = JsonConvert.DeserializeObject<GameData>(json); 
        }
        else
        { 
            LoadDefaultSave();
        }
    }

    public void NewGame() 
    {
        if (File.Exists(filePath)) 
        {
            File.Delete(filePath); 
        }

        LoadDefaultSave();
    }
    
    private void LoadDefaultSave()
    {
        TextAsset jsonTextAsset = Resources.Load<TextAsset>("Data/SaveGame/DefaultSave"); 
        string json = jsonTextAsset.text; 
        gameData = JsonConvert.DeserializeObject<GameData>(json); 
    }
}
