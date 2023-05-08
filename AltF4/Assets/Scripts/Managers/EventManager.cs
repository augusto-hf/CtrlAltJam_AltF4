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

    private GameObject[] savePoint;

    private void Awake() 
    {
        gameManager = GetComponent<GameManager>();
        saveManager = GetComponent<SaveManager>();
        menuControll = Camera.main.GetComponent<CameraMove>();
        playerCore = GameObject.FindWithTag("Player").GetComponent<PlayerCore>();
        savePoint = GameObject.FindGameObjectsWithTag("SavePoint");

    }

    private void Start() 
    {
        gameManager.onNewGame += saveManager.NewGame;
        gameManager.onNewGame +=  menuControll.ChangeTarget;
        gameManager.onNewGame += playerCore.StopAndRunPlayer;

        gameManager.onSaved += saveManager.Save;

        gameManager.onLoad += saveManager.Load;
        gameManager.onLoad +=  menuControll.ChangeTarget;
        gameManager.onLoad += playerCore.StopAndRunPlayer;

        menuControll.openMenu += playerCore.StopAndRunPlayer;

        gameManager.onSetPlayerPosition += saveManager.ApplyPositionInPlayer;

        playerCore.onPickColor += saveManager.SaveNewEmotion;
        
        foreach (GameObject point in savePoint)
        {
            SavePoint savePoint = point.GetComponent<SavePoint>();
            savePoint.onSavePoint += saveManager.SavePositionPlayer;
        }
    }

}