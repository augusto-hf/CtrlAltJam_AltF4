using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioInstance; 

    [SerializeField] private AudioSource audioMusic;
    [SerializeField] private Transform target;

    public float volumeMusics = 1;
    public float volumeSounds = 1;

    void Awake()
    {
        if (audioInstance == null)
        {
            audioInstance = this;
        }
    }

    public void SetNewVolumeMusic()
    {
        audioMusic.volume = volumeMusics;
    }

    public void GetVolumesSaved(float musics, float sounds)
    {
        volumeMusics = musics;
        volumeSounds = sounds;
        SetNewVolumeMusic();
    }

    public float GetSoundsCurrent()
    {
        return volumeSounds;
    }
    
    public void PlayAudioClip(string id)
    {
        AudioClip audioCurrent = Resources.Load<AudioClip>("Audio/Sounds/"+ id);
        AudioSource.PlayClipAtPoint(audioCurrent, target.position, volumeSounds);
    }
}
