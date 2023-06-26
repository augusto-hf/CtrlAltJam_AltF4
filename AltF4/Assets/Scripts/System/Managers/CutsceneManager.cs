using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System;

public class CutsceneManager : MonoBehaviour
{
    [SerializeField] private FadeScript fadeScript;
    [SerializeField] private GameObject panelPlayerObject, skipButton;
    //[SerializeField] private RawImage rawImage;
    [SerializeField] private PlayerCore player;
    private VideoPlayer cutsceneVideoPlayer;
    private CutsceneInfo loadedCutscene;
    private bool alreadyPlayed = false;
    private int lastPanel, currentPanel = 0, maxPanels;

    public event Action onLoadCutscene;
    public event Action onFinishedCutscene;

    private bool endFade = false;

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
        //autoSkipPanel();
    }
    #region Input
    public void cutsceneInput()
    {
        if (player.Controller.ColorButtonDown)
        {
            currentPanel++;
        }
        if (player.Controller.TongueButtonDown)
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
            if(endFade)
            {
                playNextPanel(loadedCutscene.cutscenePanels[currentPanel].video, loadedCutscene.cutscenePanels[currentPanel].description);
            }
        }
        else if (currentPanel > maxPanels - 1)
            endCutscene();

        else
            return;
    }
    private void autoSkipPanel()
    {
        if (cutsceneVideoPlayer.isPlaying && endFade)
            return;

        else
            currentPanel++;
    }
    private void playNextPanel(VideoClip panel, string description)
    {
        cutsceneVideoPlayer.clip = null;
        cutsceneVideoPlayer.clip = panel;
        cutsceneVideoPlayer.Stop();
        cutsceneVideoPlayer.Prepare();
        cutsceneVideoPlayer.Play();

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
        StartCoroutine(EndFade());
        loadedCutscene = cutsceneToPlay;
        maxPanels = cutsceneToPlay.cutscenePanels.Length;

        lastPanel = -1;

        player.Movement.cutsceneActive = true;

        skipButton.SetActive(true);
        //panelPlayerObject.SetActive(true);
    }

    private void unloadCutscene()
    {
        loadedCutscene = null;
        currentPanel = maxPanels = 0;

        lastPanel = -1;
        panelPlayerObject.SetActive(false);
        skipButton.SetActive(false);

        endFade = false;
        player.Movement.cutsceneActive = false;
        onFinishedCutscene?.Invoke();
    }
    #endregion

    IEnumerator EndFade()
    {
        //fade está ruim
        fadeScript.CallFade(true);
        yield return new WaitForSeconds(0.8f);
        onLoadCutscene?.Invoke();
        panelPlayerObject.SetActive(true);
        fadeScript.CallFade(false);
        endFade = true;
    }
}
