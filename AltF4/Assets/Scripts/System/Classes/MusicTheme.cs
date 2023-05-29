using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MusicTheme
{
    [SerializeField] private AudioClip _intro;
    [SerializeField] private AudioClip _base;
    [SerializeField] private AudioClip _orange;
    [SerializeField] private AudioClip _blue;

    public AudioClip Intro { get => _intro; }
    public AudioClip MusicBase { get => _base; }
    public AudioClip OrangeLayer { get => _orange; }
    public AudioClip BlueLayer { get => _blue; }

}
