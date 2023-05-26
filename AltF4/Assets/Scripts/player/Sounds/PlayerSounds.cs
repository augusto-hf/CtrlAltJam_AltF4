using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource soundsPlayer;
    [SerializeField] private PlayerAnimation anima;

    public float volumeSounds = 1;

    private void Awake() 
    {
        anima = GetComponent<PlayerAnimation>();
        //anima.playAnimation += PlaySounds;
    }

    private void Start() 
    {
        volumeSounds = AudioManager.audioInstance.GetSoundsCurrent();
    }

    public void PlaySounds(string name)
    {
        AudioClip audioCurrent = Resources.Load<AudioClip>("Audio/Sounds/"+ name);

        soundsPlayer.volume = volumeSounds;
        soundsPlayer.clip = audioCurrent;
        soundsPlayer.Play();
    }

}
