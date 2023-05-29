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

    private void Update()
    {
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

        _blueMusicLayer.volume = 0;
        _orangeMusicLayer.volume = 0;


    }

    public void SetVolumeLayer(ColorType type, float amount)
    {
        if (type == ColorType.Blue)
        {
            _blueMusicLayer.volume = amount;
        }
        else if (type == ColorType.Orange)
        {
            _orangeMusicLayer.volume = amount;
        }
    }

    public void PlayIntroMusic(bool gameStart)
    {
        if (!gameStart) return;

        StartCoroutine(IntroToLoop());

    }

    private IEnumerator IntroToLoop()
    {
        _baseMusic.clip = _musicTheme.Intro;
        _baseMusic.PlayScheduled(AudioSettings.dspTime);
        yield return new WaitForSeconds(_musicTheme.Intro.length);
        PlayMainMusicTheme();
    }

    public void FadeMusicLayer(ColorType type)
    {
        if (AudioManager.audioInstance.HasAudioMusic)
        {
            StartCoroutine(FadeInLayer(type));
        }
    }


    private IEnumerator FadeInLayer(ColorType type)
    {
        float percentage = 0;
        AudioSource currentLayer;

        

        if (type == ColorType.Blue)
        {
            currentLayer = _blueMusicLayer;
        }
        else if (type == ColorType.Orange)
        {
            currentLayer = _orangeMusicLayer;
        }
        else
        {
            currentLayer = _baseMusic;
        }

        while(currentLayer.volume < _baseMusic.volume)
        {
            currentLayer.volume = Mathf.Lerp(0, AudioManager.audioInstance.volumeMusics, percentage);
            percentage += Time.deltaTime / _transitionTime;
            yield return null;
        }
        
    }

    public void TriggerMusicLayer(ColorType type)
    {
        
        FadeMusicLayer(type);
    }

}
