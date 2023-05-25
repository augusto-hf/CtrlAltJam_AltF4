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
        //NewTextLocalized();
        StartCoroutine(NewTextLocalized());
    }

    public IEnumerator NewTextLocalized()
    {
        yield return new WaitForSeconds(0.01f);
        selfText.text = LocalizationManager.localizationInstance.GetLocalizedValue(key);
    }
    
}
