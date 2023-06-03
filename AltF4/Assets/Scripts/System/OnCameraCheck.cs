
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCameraCheck : MonoBehaviour
{
    public bool pointIsOnCamera { get => onCameraView();}

    [SerializeField] private Transform pointToBeSeen;
    [SerializeField] private Camera cam;

    private bool onCameraView()
    {
        Vector3 viewPos = cam.WorldToViewportPoint(pointToBeSeen.position);
        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1)
            return true;
        else
            return false;
    }

}
