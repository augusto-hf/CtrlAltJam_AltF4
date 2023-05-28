using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private GameObject panelPlayerObject;
    [SerializeField] private PlayerCore player;
    private VideoPlayer cutsceneVideoPlayer;
    private CutsceneInfo loadedCutscene;
    private bool alreadyPlayed = false;
    private int lastPanel, currentPanel = 0, maxPanels;
    void Start()
    {
        cutsceneVideoPlayer = panelPlayerObject.GetComponent<VideoPlayer>();
        if (loadedCutscene == null)
            panelPlayerObject.SetActive(false);
    }
    private void Update()
    {
        if (loadedCutscene == null)
            return;

        playCutscene();
        cutsceneInput();
        autoSkipPanel();
    }
    #region Input
    public void cutsceneInput()
    {
        if (player.Controller.ColorButtonDown)
        {
            currentPanel++;
        }
        if (player.Controller.TongueButton)
        {
            currentPanel = maxPanels;
        }
    }
    #endregion

    #region VideoPlayer
    private void playCutscene()
    {       
            if (currentPanel > lastPanel && currentPanel <= maxPanels - 1)
            {
                playNextPanel(loadedCutscene.cutscenePanels[currentPanel].video, loadedCutscene.cutscenePanels[currentPanel].description);
            }


        else if (currentPanel > maxPanels - 1)
                endCutscene();

            else
                return;
    }
    private void autoSkipPanel()
    {
       if (cutsceneVideoPlayer.isPlaying)
        {
            if (cutsceneVideoPlayer.time == cutsceneVideoPlayer.length)
            {
                currentPanel++;
            }
        }
    }
    private void playNextPanel(VideoClip panel, string description)
    {
        cutsceneVideoPlayer.Stop();
        cutsceneVideoPlayer.Prepare();
        cutsceneVideoPlayer.clip = panel;
        
        lastPanel = currentPanel;
    }
    private void endCutscene()
    {
        unloadCutscene();
    }
    #endregion

    #region Load & Unload
    public void loadCutscene(CutsceneInfo cutsceneToPlay)
    {
        loadedCutscene = cutsceneToPlay;
        maxPanels = cutsceneToPlay.cutscenePanels.Length;

        lastPanel = -1;

        panelPlayerObject.SetActive(true);        
    }

    private void unloadCutscene()
    {
        loadedCutscene = null;
        panelPlayerObject.SetActive(false);
        currentPanel = maxPanels = 0;
    }
    #endregion
}
