using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetTextLocalized : MonoBehaviour
{
    [SerializeField] private string key;
    private TextMeshProUGUI selfText;

    private void Awake() 
    {
        selfText = GetComponent<TextMeshProUGUI>();
    }

    void Start()
    {
        NewTextLocalized();
    }


    public void NewTextLocalized()
    {
        Debug.Log(transform.parent.name);
        selfText.text = LocalizationManager.localizationInstance.GetLocalizedValue(key);
    }
    
}
