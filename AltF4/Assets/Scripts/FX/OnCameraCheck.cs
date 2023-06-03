
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCameraCheck : MonoBehaviour
{
    public bool pointIsOnCamera { get => onCameraView();}

    [SerializeField] private Transform pointToBeSeen;
    [SerializeField] private Camera cam;
    [Header("Use if it activate animation:")]
    [SerializeField] private bool changeAnimationBool;
    [SerializeField] private Animator animator;
    [SerializeField] private string animatorBoolName;
    void Update()
    {
        if(changeAnimationBool)
            {
                animator.SetBool(animatorBoolName, onCameraView());                 
            }

    }
    private bool onCameraView()
    {
        Vector3 viewPos = cam.WorldToViewportPoint(pointToBeSeen.position);
        if (viewPos.x >= 0 && viewPos.x <= 1 && viewPos.y >= 0 && viewPos.y <= 1)
            return true;
        else
            return false;
    }

}
