using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{

    private GameManager gameManager;

    private void Awake() 
    {
        gameManager = GetComponent<GameManager>();
    }

    private void Start() 
    {
   
        
    }

}