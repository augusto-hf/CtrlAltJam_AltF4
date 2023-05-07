using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{

    public event Action onSaved;

    public event Action onLoad;

    public event Action onNewGame;


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
        StartGame();
    }

    private void StartGame()
    {
        
    }
}
