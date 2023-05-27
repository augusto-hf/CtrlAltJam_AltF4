using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsObjects : MonoBehaviour
{
    [SerializeField] private AudioSource sound;

    public float volumeSounds = 1;

    private void Start() 
    {
        volumeSounds = AudioManager.audioInstance.GetSoundsCurrent();
    }

    public void PlaySoundWithText(string name)
    {
        AudioClip audioCurrent = Resources.Load<AudioClip>("Audio/Sounds/"+name);

        playSound(audioCurrent);
    }

    void playSound(AudioClip audioCurrent)
    {
        sound.volume = volumeSounds;

        if (!sound.isPlaying)
        {
            sound.clip = audioCurrent;
            sound.Play();
        }
    }

    public void StopSound()
    {
        sound.Stop();
    }
}
