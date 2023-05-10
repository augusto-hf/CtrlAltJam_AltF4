using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransitionRoom : MonoBehaviour
{
    [SerializeField] private GameObject roomCam;
    private void OnTriggerEnter2D(Collider2D other) 
    {

        if (other.CompareTag("Player"))
        {
            roomCam.SetActive(true);
        }
        
    }

    private void OnTriggerExit2D(Collider2D other) 
    {

        if (other.CompareTag("Player"))
        {
            roomCam.SetActive(false);
        }
        
    }

}
