using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraMove : MonoBehaviour
{
    
    [SerializeField] private bool menuActive = false;
    
    private float smoothTimeFinal = 0.6f;
    private float offSetXFinal; 
    private float offSetYFinal;

    private Vector3 velocity = Vector3.zero;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private GameObject canvasCamera;

    public event Action openMenu;


    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeTarget();
        }
    }

    public void ChangeTarget()
    {
        menuActive = !menuActive;

        if(!menuActive)
        {
            playerCamera.SetActive(true);
            canvasCamera.SetActive(false);
            return;
        }

        playerCamera.SetActive(false);
        canvasCamera.SetActive(true);

        openMenu?.Invoke();
    }


    
}
