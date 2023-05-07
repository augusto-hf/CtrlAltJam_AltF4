using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private  float smoothTime = 0.3f; 

    [SerializeField] private bool menuActive = false;

    private Vector3 velocity = Vector3.zero;

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
            target = GameObject.FindWithTag("Player").GetComponent<Transform>();
            return;
        }

        target = GameObject.FindWithTag("Menu").GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        if(target != null )
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z); 

            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}
