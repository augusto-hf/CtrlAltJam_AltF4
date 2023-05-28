using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "New Panel", menuName = "Game Data/Cutscene/Panel")]
public class PanelInfo : ScriptableObject
{
    public bool isASprite;
    public VideoClip video;
    public Sprite sprite; 
    public string description;
}