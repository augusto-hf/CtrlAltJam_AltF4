using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : MonoBehaviour
{
    [SerializeField] private Volume postProcessing;

    public void ChangeProfile(bool ifExist, string nameEmotion)
    {
        if(!ifExist)
        {
            VolumeProfile newVolumeProfile = Resources.Load<VolumeProfile>("volumePostProcessing/"+nameEmotion); 

            postProcessing.profile = newVolumeProfile;
        }        
    }

    public void LoadProfile(string nameEmotion) 
    {
        string value = nameEmotion;

        if(nameEmotion == "")
        {
            value = "NoColor";
        }

        VolumeProfile newVolumeProfile = Resources.Load<VolumeProfile>("volumePostProcessing/"+value); 
        postProcessing.profile = newVolumeProfile;
    }
}
