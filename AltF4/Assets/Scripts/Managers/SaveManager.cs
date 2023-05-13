using System.IO;
using System;
using Newtonsoft.Json;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    const string FILE_DEFAULT_SAVE_GAME = "Data/SaveGame/DefaultSave";
    const string FILE_DEFAULT_SAVE_CONFIG = "Data/SaveGame/DefaultConfig";
    const string FILE_SAVE = "/save.json"; 
    const string FILE_CONFIG = "/config.json"; 
    
    [SerializeField] private GameObject IcoSaved;
    [SerializeField] private Button buttonContinue;

    public GameData gameData;
    public ConfigData configData;

    private string filePathSave;
    private string filePathConfig;

    public event Action<bool, string> newColorPicks;

    private void Awake() 
    {
        filePathSave = Application.persistentDataPath + FILE_SAVE; 
        filePathConfig = Application.persistentDataPath + FILE_CONFIG; 

        Debug.Log("Local de salvamento: " + filePathSave); 

    }

    private void Start() 
    {
        LoadConfig();
        UpdatedButtonContinue();  
    }

    public void LoadEmotionPlayer(string nameColor)
    {
        GameObject[] allBlobs = GameObject.FindGameObjectsWithTag("ColorPower");

        foreach (GameObject blob in allBlobs) 
        {
            BlobManager blobManager = blob.GetComponent<BlobManager>();
            
            if(blobManager != null )
            {
                if(blobManager.nameColor == nameColor)
                {
                    PlayerColorAction playerColor = GameObject.FindWithTag("Player").GetComponent<PlayerColorAction>();
                    playerColor.TakeColor(blob);
                    return;
                }
            }
        }
    }
    
    public void UpdatedButtonContinue()
    {
        buttonContinue.interactable = CheckIfExistSave();
    }

    public bool CheckIfExistSave()
    {
        if (File.Exists(filePathSave)) 
        {
            return true;
        }
        return false;
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

        UpdatedConfig();
    }

    public void updatedNewConfig()
    {
        configData.currentLanguage = LocalizationManager.localizationInstance.SetNewLanguage(LocalizationManager.localizationInstance.currentLanguage);

        configData.volumeMusics = AudioManager.audioInstance.volumeMusics;
        configData.volumeSounds = AudioManager.audioInstance.volumeSounds;

        SaveConfig();

    }
    
    public void UpdatedConfig()
    {
        configData.currentLanguage = LocalizationManager.localizationInstance.SetNewLanguage(configData.currentLanguage);
        AudioManager.audioInstance.GetVolumesSaved(configData.volumeMusics, configData.volumeSounds);
    }

    public void SaveConfig() 
    {
        string json = JsonUtility.ToJson(configData, true); 
        File.WriteAllText(filePathConfig, json); 
    }

    public void ApplyPositionInPlayer(Transform positionPlayer)
    {
        positionPlayer.position = gameData.positionPlayer;
    }

    public void SaveNewEmotion(string nameEmotion)
    {
        bool IsFind = CheckIfEmotionExist(nameEmotion);

        if (!IsFind)
        {
            gameData.emotions.Add(nameEmotion);
        }

        gameData.currentEmotions = nameEmotion;
        
        newColorPicks?.Invoke(IsFind, nameEmotion);

        Save();
    }

    public bool CheckIfEmotionExist(string nameEmotion)
    {
        string findEmotion = gameData.emotions.Find(x => x == nameEmotion);

        if (findEmotion != null)
        {
            return true;
        }

        return false;
    }

    public void SavePositionPlayer(Transform positionPlayer)
    {
        gameData.positionPlayer = positionPlayer.position;

        Save();
    }

    public void Save() 
    {
        StartCoroutine(ShowIco());

        string json = JsonUtility.ToJson(gameData, true); 
        File.WriteAllText(filePathSave, json); 
    }

    IEnumerator ShowIco()
    {
        if(!IcoSaved.activeSelf)
        {
            IcoSaved.SetActive(true);
            yield return new WaitForSeconds(3f);
            IcoSaved.SetActive(false);
        }
    }

    public void Load() 
    {
        if (CheckIfExistSave()) 
        {
            string json = File.ReadAllText(filePathSave);
            gameData = JsonConvert.DeserializeObject<GameData>(json); 
        }
        else
        { 
            gameData = LoadDefaultSave(FILE_DEFAULT_SAVE_GAME, gameData);
        }

        LoadEmotionPlayer(gameData.currentEmotions);
    }

    public void NewGame() 
    {
        if (CheckIfExistSave()) 
        {
            File.Delete(filePathSave); 
            buttonContinue.interactable = false;
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
