using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource soundsPlayer;

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

    public void PlaySteps()
    {
        int keyValue = Random.Range(1, 9);

        AudioClip audioCurrent = Resources.Load<AudioClip>("Audio/Sounds/Steps/step0"+ keyValue.ToString());

        playSound(audioCurrent);
    }

    void playSound(AudioClip audioCurrent)
    {
        soundsPlayer.volume = volumeSounds;
        soundsPlayer.clip = audioCurrent;
        soundsPlayer.Play();
    }

}
