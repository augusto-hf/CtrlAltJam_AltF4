using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;


public class flareAnimationManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private OnCameraCheck OnCameraCheck;
    [SerializeField] private Light2D flareLight, spotLight;
    [SerializeField] private float lightMinIntesity, lightMaxIntesity, time;
    private float elapsedTime;

    private void Start()
    {
        flareLight.intensity = spotLight.intensity = lightMinIntesity;
    }

    private void Update()
    {
        //testasdasd
        if (OnCameraCheck.pointIsOnCamera)
        {
            animator.Play("lightSmoothlyMovesIntoPlace");
            StartCoroutine(turnLightsOnSmoothly());
        }
    }

    IEnumerator turnLightsOnSmoothly()
    {
        float cnangeSpeed = 0;
        while (elapsedTime < time)
        {

            flareLight.intensity = spotLight.intensity = Mathf.Lerp(lightMinIntesity, lightMaxIntesity, cnangeSpeed);

            cnangeSpeed += Time.deltaTime / time;
            yield return null;
        }

        enabled = false;
    }
}
