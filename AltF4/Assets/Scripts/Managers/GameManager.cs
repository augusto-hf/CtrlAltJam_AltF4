using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public event Action onSaved;

    public event Action onLoad;

    public event Action onNewGame;

    public event Action<bool> onGameStarted;

    public event Action<Transform> onSetPlayerPosition;
    
    private Transform playerCore;

    [SerializeField] private GameObject mainMenu;

    public bool gameISRunning = false;

    private void Awake() 
    {
        playerCore = GameObject.FindWithTag("Player").GetComponent<Transform>();    
    }

    public void NewGame()
    {
        onNewGame?.Invoke();
        onSetPlayerPosition?.Invoke(playerCore);
        StartedGame();
    }
    
    public void SaveGame()
    {
        onSaved?.Invoke();
    }

    public void LoadGame()
    {
        onLoad?.Invoke();
        onSetPlayerPosition?.Invoke(playerCore);
        StartedGame();
    }

    void StartedGame()
    {
        gameISRunning = true;
        mainMenu.SetActive(!gameISRunning);
        onGameStarted?.Invoke(gameISRunning);
    }

    public void StopedGame()
    {
        gameISRunning = false;
        mainMenu.SetActive(!gameISRunning);
        onGameStarted?.Invoke(gameISRunning);
    }

    public void ResetScene()
    {
        SceneManager.LoadScene("Debug");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
