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
        openMenu?.Invoke();

        if(menuActive)
        {
            ActivePause();
            pauseAnimator.Play("start");
            return;
        }


        pauseAnimator.Play("exit");

        float time = pauseAnimator.GetCurrentAnimatorStateInfo(0).length;
        Invoke("ActivePause", time);
        
    }

    public void ActivePause()
    {
        pause.SetActive(menuActive);
    }

    public void GetIfGameIsRunning(bool value)
    {
        gameISRunning = value;
    }


    
}
