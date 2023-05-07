using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangePassive : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var movement = other.gameObject.GetComponent<PlayerMovement>();
            RunPassive(movement);
        }   
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var movement = other.gameObject.GetComponent<PlayerMovement>();
            DisablePassive(movement);
        }
        
    }


    public void RunPassive(PlayerMovement movement)
    {
        movement.SetMaxSpeed(movement.Data.MaxRunSpeed);
    }

    public void DisablePassive(PlayerMovement movement)
    {
        movement.SetMaxSpeed(movement.Data.MaxHorizontalSpeed);
    }

    
}
