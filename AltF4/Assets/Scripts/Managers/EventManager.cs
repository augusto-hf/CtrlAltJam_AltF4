using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    private GameManager gameManager;
    private SaveManager saveManager;
    
    private GameObject playerObj;

    private void Awake() 
    {
        gameManager = GetComponent<GameManager>();
        saveManager = GetComponent<SaveManager>();
        playerObj = GameObject.FindWithTag("Player");
    }

    private void Start() 
    {
        gameManager.onNewGame += saveManager.NewGame;
        gameManager.onSaved += saveManager.Save;

        gameManager.onLoad += saveManager.Load;
    }

}