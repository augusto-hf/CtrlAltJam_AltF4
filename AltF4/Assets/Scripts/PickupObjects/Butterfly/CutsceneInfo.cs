using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Cutscene", menuName = "Game Data/Cutscene")]
public class CutsceneInfo : ScriptableObject
{
    public int ID;
    public string Name;
    public int NumberOfPanels;
    public Sprite[] VisualPanel;
    public string[] PanelDescription;
}
