using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    private GameManager gameManager;
    private SaveManager saveManager;
    private CameraMove menuControll;
    private PlayerCore playerCore;
    private MenuManager menuManager;
    private NarrationManager narrationManager;

    private GameObject[] savePoint;

    private void Awake() 
    {
        gameManager = GetComponent<GameManager>();
        saveManager = GetComponent<SaveManager>();
        menuManager = GetComponent<MenuManager>();
        narrationManager = GetComponent<NarrationManager>();
        menuControll = Camera.main.GetComponent<CameraMove>();

        playerCore = GameObject.FindWithTag("Player").GetComponent<PlayerCore>();
        savePoint = GameObject.FindGameObjectsWithTag("SavePoint");

    }

    private void Start() 
    {
        //new game
        gameManager.onNewGame += saveManager.NewGame;
        gameManager.onNewGame +=  menuControll.ChangeTarget;
        gameManager.onNewGame += playerCore.StopAndRunPlayer;

        //save game
        gameManager.onSaved += saveManager.Save;
        playerCore.onPickColor += saveManager.SaveNewEmotion;
        
        //narration
        playerCore.onPickColor += narrationManager.ReproduceNarration;

        foreach (GameObject point in savePoint)
        {
            SavePoint savePoint = point.GetComponent<SavePoint>();
            savePoint.onSavePoint += saveManager.SavePositionPlayer;
        }

        //loadGame
        gameManager.onLoad += saveManager.Load;
        gameManager.onLoad +=  menuControll.ChangeTarget;
        gameManager.onLoad += playerCore.StopAndRunPlayer;
        menuControll.openMenu += playerCore.StopAndRunPlayer;
        gameManager.onSetPlayerPosition += saveManager.ApplyPositionInPlayer;

        gameManager.onGameStarted += menuManager.ChangeMenu;
        
    }

}