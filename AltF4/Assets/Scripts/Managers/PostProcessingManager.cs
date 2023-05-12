using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostProcessingManager : MonoBehaviour
{
    //[SerializeField] private VolumeProfile postProcessing;

    public void ChangeProfile(bool nameEmotion, string newProfile)
    {
        Debug.Log(nameEmotion);
        Debug.Log(newProfile);
    }
}
