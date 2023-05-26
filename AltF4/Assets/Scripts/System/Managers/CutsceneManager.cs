using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{


    public void playCutscene(CutsceneInfo cutsceneToPlay)
    {
        for (int i = 0;i > (cutsceneToPlay.NumberOfPanels - 1);i++)
        {
            if(cutsceneToPlay.PanelDescription[i] != null)
                playCurrentPanel(cutsceneToPlay.VisualPanel[i], cutsceneToPlay.PanelDescription[i]);
            else
                playCurrentPanel(cutsceneToPlay.VisualPanel[i], " ");
        }
    }
    private void playCurrentPanel(Sprite panel, string description)
    {

    }

}
