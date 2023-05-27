using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetTextLocalized : MonoBehaviour
{
    [SerializeField] private string key;
    private TextMeshProUGUI selfText;
    private TMP_Text selfTextAnother;

    private void Awake() 
    {
        selfText = GetComponent<TextMeshProUGUI>();

        if(selfText == null )
        {
            selfTextAnother = GetComponent<TMP_Text>();
        }
        
    }

    void Start()
    {
        //NewTextLocalized();
        StartCoroutine(NewTextLocalized());
    }

    public IEnumerator NewTextLocalized()
    {
        yield return new WaitForSeconds(0.01f);
        string newText = LocalizationManager.localizationInstance.GetLocalizedValue(key);

        if(selfText == null )
        {
            selfTextAnother.text = newText;
        }
        else
        {
            selfText.text = newText;
        }
    }
    
}
