using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public event Action onSaved;

    public event Action onLoad;

    public event Action onNewGame;


    public event Action<Transform> onSetPlayerPosition;
    
    private Transform playerCore;

    private void Awake() 
    {
        playerCore = GameObject.FindWithTag("Player").GetComponent<Transform>();    
    }

    public void NewGame()
    {
        onNewGame?.Invoke();
    }
    
    public void SaveGame()
    {
        onSaved?.Invoke();
    }

    public void LoadGame()
    {
        onLoad?.Invoke();
        onSetPlayerPosition?.Invoke(playerCore);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
