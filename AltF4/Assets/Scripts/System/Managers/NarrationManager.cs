using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NarrationManager : MonoBehaviour
{
    [SerializeField] private GameObject textBoxObj;
    [SerializeField] private AudioSource audioNarration;
    [SerializeField] private float typingSpeed = 0.1f;
    [SerializeField] private TextMeshProUGUI legendText;

    private Dictionary<string, int[]> colorPicked = new Dictionary<string, int[]>();

    public void ReproduceNarration(string color)
    {
        int size = LocalizationManager.localizationInstance.GetSizeDictionary(color);

        int keyValue = Random.Range(0, size);

        if (!colorPicked.ContainsKey(color))
        {
            colorPicked.Add(color, new int[keyValue]);
        }
        else if(colorPicked[color].Length == size)
        {
            colorPicked[color] = new int[0];
        }
        else
        {
            List<int> list = new List<int>(colorPicked[color]);

            while(list.Contains(keyValue))
            {
                keyValue = Random.Range(0, size);
            }
        }

        string text = LocalizationManager.localizationInstance.GetLocalizedValueForNarration(color, keyValue.ToString());
        
        audioNarration.clip = Resources.Load<AudioClip>("Audio/Narrations/"+color+ "/Edit/"+ keyValue.ToString());
        StartCoroutine(ShowText(text));
    }

    IEnumerator ShowText(string text)
    {
        audioNarration.Play();
        legendText.text = "";
        
        textBoxObj.SetActive(true);

        for (int i = 0; i < text.Length; i++)
        {
            legendText.text += text[i];
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(audioNarration.clip.length);
        textBoxObj.SetActive(false);
    }
}
