using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System.Globalization;
using Newtonsoft.Json;
using System.Linq;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager localizationInstance; //instancia do script

    public string currentLanguage = "pt"; //idioma atual do jogo
    private LocalizationData localizationData; //objeto de dados de localização

    void Awake()
    {
        if (localizationInstance == null)
        {
            localizationInstance = this;
        }

        //carrega o idioma atual do sistema
        currentLanguage = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
        Debug.Log(currentLanguage);
        //carrega o arquivo de texto
        LoadLocalizedText(currentLanguage);
    }

    //carrega o arquivo de texto
    public void LoadLocalizedText(string language)
    {
        string filePath = "Localization/" + language;
    
        TextAsset jsonTextAsset = Resources.Load<TextAsset>(filePath) ?? null;

        if (jsonTextAsset != null)
        {
            string json = jsonTextAsset.text;
            localizationData = JsonConvert.DeserializeObject<LocalizationData>(json);
        }
        else
        { 
            jsonTextAsset = Resources.Load<TextAsset>("Localization/en");
            string json = jsonTextAsset.text;
            localizationData = JsonConvert.DeserializeObject<LocalizationData>(json);
        }
    }


    //retorna a tradução da chave especificada
    public string GetLocalizedValue(string key)
    {
        string value = key;

        //verifica se o objeto de dados de localização existe e contém a chave especificada
        if (localizationData != null)
        {
            if(localizationData.localizedTexts.ContainsKey(key))
            {
                value = localizationData.localizedTexts[key];
            }
            else
            {
                value = localizationData.localizedTexts["null"];
            }
        }

        return value;
    }
}
