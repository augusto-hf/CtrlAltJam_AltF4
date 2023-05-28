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

    public event Action configUpdated;

    private GameObject[] butterlfyObj;

    private void Awake() 
    {
        filePathSave = Application.persistentDataPath + FILE_SAVE; 
        filePathConfig = Application.persistentDataPath + FILE_CONFIG; 

        Debug.Log("Local de salvamento: " + filePathSave); 

        butterlfyObj = GameObject.FindGameObjectsWithTag("butterfly");
    }

    private void Start() 
    {
        LoadConfig();
        UpdatedButtonContinue();  
    }

    public void LoadEmotionPlayer(string nameColor)
    {
        GameObject[] allBlobs = GameObject.FindGameObjectsWithTag("ColorPower");
        PlayerColorManager playerColor = GameObject.FindWithTag("Player").GetComponent<PlayerColorManager>();

        foreach (GameObject blob in allBlobs) 
        {
            BlobManager blobManager = blob.GetComponent<BlobManager>();
            
            if(blobManager != null )
            {
                if(blobManager.nameColor == nameColor)
                {
                    playerColor.TakeBlobColor(blob);
                    return;
                }
            }

        }

        playerColor.GiveNoColor();
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
        configUpdated?.Invoke();
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
        LocalizationManager.localizationInstance.SetNewLanguage(configData.currentLanguage);
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

        UpdatedButterfly();
        //LoadEmotionPlayer(gameData.currentEmotions);
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

    private void UpdatedButterfly()
    {
        foreach (GameObject obj in butterlfyObj)
        {
            ButterflyManager managerButterfly = obj.GetComponent<ButterflyManager>();
            
            foreach(string emotion in gameData.emotions)
            {
                
                if(emotion == managerButterfly.colorIUnlock.ToString().ToLower())
                {
                    managerButterfly.used = true;
                }
            }
        }
    }

}
