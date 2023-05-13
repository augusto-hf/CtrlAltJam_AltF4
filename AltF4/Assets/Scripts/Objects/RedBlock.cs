using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBlock : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            var playerCore = other.gameObject.GetComponent<PlayerCore>();

            playerCore.Health.Death();

        }
        
    }
}
