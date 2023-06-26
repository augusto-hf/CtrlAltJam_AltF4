using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform ImageToParallax;
    [SerializeField] private float YOffset, XOffset, Xmin, Xmax, Ymin, Ymax;
    [SerializeField] private Vector2 parallaxFactor;
    [SerializeField] private bool followCam;
    Renderer renderer;
    Vector2 startPosition;
    float startZ;

    Vector2 travel =>  (Vector2)cam.transform.position - startPosition;

    float distanceFromSubject => transform.position.z - ImageToParallax.position.z;

    float clippingPlane => cam.transform.position.z + (distanceFromSubject > 0? 50 : -10);


    //float parallaxFactor => Mathf.Abs(distanceFromSubject/clippingPlane);
    void Start()
    {
        renderer = GetComponent<Renderer>();

        startPosition = transform.position;
        startZ = transform.position.z;
    }



    void FixedUpdate()
    {
        if (!followCam) 
        {

            Vector2 newPos = startPosition + travel * parallaxFactor;
            Vector2 newPosLimitted;

            newPosLimitted.x = Mathf.Clamp(newPos.x, Xmin, Xmax);
            newPosLimitted.y = Mathf.Clamp(newPos.y, Ymin, Ymax);


            transform.position = new Vector3(newPosLimitted.x + XOffset, newPosLimitted.y + YOffset, startZ);
        }
        else
        {
            transform.position = new Vector3(cam.gameObject.transform.position.x , cam.gameObject.transform.position.y);
        }
       
    }
}
