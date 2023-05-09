using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetTextLocalized : MonoBehaviour
{
    [SerializeField] private string key;
    private TextMeshProUGUI selfText;

    void Start()
    {
        selfText = GetComponent<TextMeshProUGUI>();
        selfText.text = LocalizationManager.localizationInstance.GetLocalizedValue(key);
    }

    
}
