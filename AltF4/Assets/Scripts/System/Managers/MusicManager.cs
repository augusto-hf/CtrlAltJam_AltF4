using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;
    [SerializeField] private AudioClip _menuMusic;
    
    [SerializeField] private MusicTheme _musicTheme;

    [SerializeField] private AudioSource _baseMusic;
    [SerializeField] private AudioSource _blueMusicLayer;
    [SerializeField] private AudioSource _orangeMusicLayer;

    [SerializeField] private float _transitionTime;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _baseMusic.clip = _menuMusic;
        _baseMusic.PlayScheduled(AudioSettings.dspTime);
        GameManager.Instance.onGameStarted += PlayIntroMusic;
    }

    public void SetVolume(float amount)
    {
        _baseMusic.volume = amount;
        _blueMusicLayer.volume = amount;
        _orangeMusicLayer.volume = amount;
    }

    public void PlayMainMusicTheme()
    {

        _baseMusic.clip = _musicTheme.MusicBase;
        _blueMusicLayer.clip = _musicTheme.BlueLayer;
        _orangeMusicLayer.clip = _musicTheme.OrangeLayer;

        _baseMusic.PlayScheduled(AudioSettings.dspTime);
        _blueMusicLayer.PlayScheduled(AudioSettings.dspTime);
        _orangeMusicLayer.PlayScheduled(AudioSettings.dspTime);

        _blueMusicLayer.mute = true;
        _orangeMusicLayer.mute = true;


    }
    public void PlayIntroMusic(bool gameStart)
    {
        if (!gameStart) return;

        StartCoroutine(IntroToLoop());

    }

    private void PlayOrangeLayer()
    {
        _orangeMusicLayer.mute = false;
    }

    private void PlayBlueLayer()
    {
        _blueMusicLayer.mute = false;
    }

    private IEnumerator IntroToLoop()
    {
        _baseMusic.clip = _musicTheme.Intro;
        _baseMusic.PlayScheduled(AudioSettings.dspTime);
        yield return new WaitForSeconds(_musicTheme.Intro.length);
        _baseMusic.clip = _musicTheme.IntroLoop;
        _baseMusic.PlayScheduled(AudioSettings.dspTime);
    }

    public void FadeInVolume()
    {
        if (AudioManager.audioInstance.HasAudioMusic)
        {
            StartCoroutine(FadeInMusic());
        }
    }

    public void FadeOutVolume()
    {
        if (AudioManager.audioInstance.HasAudioMusic)
        {
            StartCoroutine(FadeOutMusic());            
        }
    }

    private IEnumerator FadeInMusic()
    {
        float percentage = 0;

        while(_baseMusic.volume < 0)
        {
            SetVolume(Mathf.Lerp(0, AudioManager.audioInstance.volumeMusics, percentage));
            percentage += Time.deltaTime / _transitionTime;
            yield return null;
        } 
    }
    private IEnumerator FadeOutMusic()
    {
        float percentage = 0;

        while(_baseMusic.volume > 0)
        {
            SetVolume(Mathf.Lerp(AudioManager.audioInstance.volumeMusics, 0, percentage));
            percentage += Time.deltaTime / _transitionTime;
            yield return null;
        } 
    }

    public void TriggerMusicLayer(ColorType type)
    {
        switch(type)
        {
            case ColorType.Blue:
                PlayBlueLayer();
                break;
            case ColorType.Orange:
                PlayOrangeLayer();
                break;
            default:
                return;
        }
    }

}
