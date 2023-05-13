using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private Transform ImageToParallax;
    Vector2 StartPosition;
    float StartZ;

    Vector2 travel =>  (Vector2)cam.transform.position - StartPosition;
    void Start()
    {
        StartPosition = transform.position;
        StartZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
