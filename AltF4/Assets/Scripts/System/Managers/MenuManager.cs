using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private MusicManager menuMusic;
    [SerializeField] private GameObject menuPrincipal;
    [SerializeField] private GameObject rooms;
    [SerializeField] private TMP_Dropdown drop;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSounds;

    [SerializeField] private FadeScript fadeScript;

    private void Awake() 
    {
        //SetDropdownOptions();
    }

    private void Start() 
    {
        SetDropdownOptions();
        //UpdatedSliderVolumeMusic();    
        //UpdatedSliderVolumeSound();
        //UpdatedDropDownOptions();
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

    //ATUALIZA COM NOVOS VALORES
    public void SetNewVolumeSound()
    {
        AudioManager.audioInstance.volumeSounds = sliderSounds.value;
    }
    public void SetNewVolumeMusic()
    {
        AudioManager.audioInstance.volumeMusics = sliderMusic.value;
        menuMusic.SetVolume(sliderMusic.value);
    }

    public void UpdatedVomlumeMusicSaved()
    {
        menuMusic.SetVolume(AudioManager.audioInstance.volumeMusics);
    }

    public void NewSelectedOption()
    {
        LocalizationManager.localizationInstance.currentLanguage = drop.options[drop.value].text.ToLower();
    }

    //ATUALIZA O VISUAL
    public void UpdatedSliderVolumeSound()
    {
        sliderSounds.value = AudioManager.audioInstance.volumeSounds;
    }

    public void UpdatedSliderVolumeMusic()
    {
        sliderMusic.value = AudioManager.audioInstance.volumeMusics;
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

    public void CallFadeIn()
    {
        fadeScript.CallFade(true);
    }
}
