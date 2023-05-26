using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Cutscene", menuName = "Game Data/New Cutscene Information")]
public class CutsceneInfo : ScriptableObject
{
    public int ID;
    public int numberOfPanels;
    public GameObject[] visualPanel;
    public string[] PanelDescription;
}
