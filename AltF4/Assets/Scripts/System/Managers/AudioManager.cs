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

    public bool HasAudioMusic;

    void Awake()
    {
        if (audioInstance == null)
        {
            audioInstance = this;
        }
    }

    public void GetVolumesSaved(float musics, float sounds)
    {
        volumeMusics = musics;
        volumeSounds = sounds;

    }

    public float GetSoundsCurrent()
    {
        return volumeSounds;
    }

    public float GetMusicCurrent()
    {
        return volumeMusics;
    }
    
    public void PlayAudioClip(string id)
    {
        AudioClip audioCurrent = Resources.Load<AudioClip>("Audio/Sounds/"+ id);
        AudioSource.PlayClipAtPoint(audioCurrent, target.position, volumeSounds);
    }
}
