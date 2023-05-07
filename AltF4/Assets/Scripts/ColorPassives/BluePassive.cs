using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePassive : MonoBehaviour
{

    private void Update()
    {
           
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var movement = other.gameObject.GetComponent<PlayerMovement>();
            var rb = other.gameObject.GetComponent<Rigidbody2D>();
            movement.JumpPad();
        }   
    }

    

    

    

}
