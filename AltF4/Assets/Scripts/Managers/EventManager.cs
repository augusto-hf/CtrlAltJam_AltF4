using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    private GameManager gameManager;
    private SaveManager saveManager;
    private CameraMove menuControll;
    private GameObject savePoint;

    private void Awake() 
    {
        gameManager = GetComponent<GameManager>();
        saveManager = GetComponent<SaveManager>();
        menuControll = Camera.main.GetComponent<CameraMove>();
        //savePoint = ;
    }

    private void Start() 
    {
        gameManager.onNewGame += saveManager.NewGame;
        gameManager.onNewGame +=  menuControll.ChangeTarget;

        gameManager.onSaved += saveManager.Save;

        gameManager.onLoad += saveManager.Load;
        gameManager.onLoad +=  menuControll.ChangeTarget;
    }

}