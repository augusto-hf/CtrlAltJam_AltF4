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

    [SerializeField] private string currentSpeak;

    public void LoadNarration(string color)
    {
        int keyValue = 0;
        currentSpeak = LocalizationManager.localizationInstance.GetLocalizedValueForNarration(color, keyValue.ToString());
        audioNarration.clip = Resources.Load<AudioClip>("Audio/Narrations/"+ color + "/"+ keyValue.ToString());
    }
    public void ReproduceNarration()
    {
        StartCoroutine(ShowText(currentSpeak));
    }

    public int Check(string color)
    {
        int size = LocalizationManager.localizationInstance.GetSizeDictionary(color);

        int keyValue = Random.Range(0, size);

        if(colorPicked[color].Length == size)
        {
            Debug.Log("zerou");
            colorPicked[color] = new int[0];
        }
        else
        {
            List<int> list = new List<int>(colorPicked[color]);

            while(list.Contains(keyValue))
            {
                keyValue = Random.Range(0, size);
                Debug.Log("já tem");
            }
        }

        if (!colorPicked.ContainsKey(color))
        {
            Debug.Log("add");
            colorPicked[color] = new int[keyValue];
        }

        return keyValue;
    }

    IEnumerator ShowText(string text)
    {
        audioNarration.Play();
        
        textBoxObj.SetActive(true);

        for (int i = 0; i < text.Length; i++)
        {
            legendText.text += text[i];
            yield return new WaitForSeconds(typingSpeed);
        }

        yield return new WaitForSeconds(audioNarration.clip.length);
        FinishedNarration();
    }

    public void FinishedNarration()
    {
        legendText.text = "";
        textBoxObj.SetActive(false);
    }
}
