using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using Utils;

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
    
    public void UpdatedButtonContinue()
    {
        buttonContinue.interactable = Utils.Data.CheckIfExistSave(filePathSave);
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

    public void SaveNewEmotion(string nameEmotion)
    {
        bool IsFind = Utils.Data.FindKeyInArrayData(nameEmotion, gameData);

        if (!IsFind)
        {
            gameData.emotions.Add(nameEmotion);
        }

        gameData.currentEmotions = nameEmotion;
        
        newColorPicks?.Invoke(IsFind, nameEmotion);

        Save();
    }


    public void ApplyPositionInPlayer(Transform positionPlayer)
    {
        positionPlayer.position = gameData.positionPlayer;
    }

    public void SavePositionPlayer(Transform positionPlayer)
    {
        gameData.positionPlayer = positionPlayer.position;

        Save();
    }

    public void SaveConfig() 
    {
        Utils.Data.Save<ConfigData>(filePathConfig, ref configData);
    }

    public void Save() 
    {
        StartCoroutine(ShowIco());

        Utils.Data.Save<GameData>(filePathSave, ref gameData);
    }

    public void LoadConfig()
    {
        Utils.Data.LoadSave<ConfigData>(filePathConfig, ref configData, FILE_DEFAULT_SAVE_CONFIG);
        UpdatedConfig();
        configUpdated?.Invoke();
    }

    public void Load() 
    {
        Utils.Data.LoadSave<GameData>(filePathSave, ref gameData, FILE_DEFAULT_SAVE_GAME);
        UpdatedButterfly();
    }

    public void ResetConfig()
    {
        Utils.Data.ResetFileSaved<ConfigData>(FILE_DEFAULT_SAVE_CONFIG, ref configData, filePathConfig);
    }

    public void NewGame() 
    {
        Utils.Data.ResetFileSaved<GameData>(FILE_DEFAULT_SAVE_GAME, ref gameData, filePathSave);
        buttonContinue.interactable = false;
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
                    MusicManager.Instance.TriggerMusicLayer(managerButterfly.colorIUnlock);
                }
            }
        }
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

}
