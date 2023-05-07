using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private  float smoothTime = 0.3f; 
    private Vector3 velocity = Vector3.zero;

    private void Awake() 
    {
        if (player == null)
            player = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, transform.position.z); 

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
