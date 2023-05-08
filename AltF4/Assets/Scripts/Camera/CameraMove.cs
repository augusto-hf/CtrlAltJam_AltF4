using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private  float smoothTimeMenu = 0.6f; 

    [SerializeField] private  float smoothTimeGame = 0.1f; 
   
    [SerializeField] private  float offSetXPlayer; 
    [SerializeField] private  float offSetYPlayer; 

    [SerializeField] private bool menuActive = false;
    

    private float smoothTimeFinal = 0.6f;
    private float offSetXFinal; 
    private float offSetYFinal;

    private Vector3 velocity = Vector3.zero;

    public event Action openMenu;

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ChangeTarget();
        }
    }

    void LateUpdate()
    {
        if(target != null )
        {
            Vector3 targetPosition = new Vector3(target.position.x  + offSetXFinal, target.position.y + offSetYFinal, transform.position.z); 

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTimeFinal);
        }
    }

    public void ChangeTarget()
    {
        menuActive = !menuActive;

        if(!menuActive)
        {
            target = GameObject.FindWithTag("Player").GetComponent<Transform>();
            StartCoroutine(SetOffSetFinal(offSetXPlayer, offSetYPlayer, smoothTimeGame));
            return;
        }

        target = GameObject.FindWithTag("Menu").GetComponent<Transform>();
        StartCoroutine(SetOffSetFinal(0, 0, smoothTimeMenu));
        openMenu?.Invoke();
    }


    IEnumerator SetOffSetFinal(float x, float y, float smooth)
    {
        offSetXFinal = x;
        offSetYFinal = y;
        yield return new WaitForSeconds(0.2f);
        smoothTimeFinal = smooth;
    }
}
