using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[CreateAssetMenu(fileName = "New Cutscene", menuName = "Game Data/Cutscene/Cutscene")]
public class CutsceneInfo : ScriptableObject
{
    public int ID;
    public string Name;
    public PanelInfo[] cutscenePanels;
}
