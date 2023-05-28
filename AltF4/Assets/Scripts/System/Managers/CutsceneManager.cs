using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{

    
    public void startCutscene(CutsceneInfo cutsceneToPlay)
    {
        if (cutsceneToPlay.PanelDescription[0] != null)
        {
            playNextCurrentPanel(cutsceneToPlay.VisualPanel[0], cutsceneToPlay.PanelDescription[0]);
        }
        else
        {
            playNextCurrentPanel(cutsceneToPlay.VisualPanel[0], " ");
        }            
    }
    private void playNextCurrentPanel(Sprite panel, string description)
    {

    }

}
