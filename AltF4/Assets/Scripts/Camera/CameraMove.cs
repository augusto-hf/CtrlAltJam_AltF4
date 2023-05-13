using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraMove : MonoBehaviour
{
    
    [SerializeField] private bool menuActive = false;
    [SerializeField] private bool gameISRunning = false;

    [SerializeField] private GameObject pause;
    [SerializeField] private Animator pauseAnimator;

    public event Action openMenu;

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameISRunning)
        {
            ChangeTarget();
        }
    }

    public void ChangeTarget()
    {
        menuActive = !menuActive;
        pause.SetActive(menuActive);
        openMenu?.Invoke();

        if(menuActive)
        {
            pauseAnimator.Play("start");
            return;
        }
        
        pauseAnimator.Play("exit");
        
    }

    public void GetIfGameIsRunning(bool value)
    {
        gameISRunning = value;
    }


    
}
