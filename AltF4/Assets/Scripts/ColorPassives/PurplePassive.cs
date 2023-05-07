using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurplePassive : MonoBehaviour
{
    [SerializeField] private Transform outputPoint;

    private void OnTriggerEnter2D(Collider2D other) 
    {

        if (other.CompareTag("Player"))
        {
            Debug.Log("Purple Passive");

            other.GetComponent<PlayerMovement>().Teleport(outputPoint.position);
        }
        
    }

}
