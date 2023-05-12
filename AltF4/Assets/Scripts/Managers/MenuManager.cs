using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPrincipal;
    [SerializeField] private GameObject rooms;
    [SerializeField] private TMP_Dropdown drop;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSounds;

    private void Awake() 
    {
        SetDropdownOptions();
    }

    private void Start() 
    {
        UpdatedSliderVolumeMusic();    
        UpdatedSliderVolumeSound();
        UpdatedDropDownOptions();    
    }
    
    public void ChangeMenu(bool active)
    {
        menuPrincipal.SetActive(!active);
        rooms.SetActive(active);
    }

    private void SetDropdownOptions()
    {
        drop.options.Clear();

        TextAsset[] options = Resources.LoadAll<TextAsset>("Localization/");

        List<TMP_Dropdown.OptionData> dropdownOptions = new List<TMP_Dropdown.OptionData>();

        foreach (TextAsset option in options)
        {
            TMP_Dropdown.OptionData dropdownOption = new TMP_Dropdown.OptionData(option.name.ToUpper());
            dropdownOptions.Add(dropdownOption);
        }

        drop.AddOptions(dropdownOptions);

    }

    public void UpdatedSliderVolumeSound()
    {
        sliderSounds.value = AudioManager.audioInstance.volumeSounds;
    }

    public void UpdatedSliderVolumeMusic()
    {
        sliderMusic.value = AudioManager.audioInstance.volumeMusics;
    }

    public void SetNewVolumeSound()
    {
        AudioManager.audioInstance.volumeSounds = sliderSounds.value;
    }
    public void SetNewVolumeMusic()
    {
        AudioManager.audioInstance.volumeMusics = sliderMusic.value;
        AudioManager.audioInstance.SetNewVolumeMusic();
    }

    public void NewSelectedOption()
    {
        LocalizationManager.localizationInstance.currentLanguage = drop.options[drop.value].text.ToLower();
    }

    public void UpdatedDropDownOptions()
    {
        string text = LocalizationManager.localizationInstance.currentLanguage.ToUpper();

        for (int i = 0; i < drop.options.Count; i++)
        {
            if (drop.options[i].text == text)
            {
                drop.value = i;
                break;
            }
        }
        
        drop.RefreshShownValue();
    }
}
