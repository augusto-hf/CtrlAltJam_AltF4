using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangePassive : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<PlayerCore>();
            RunPassive(player);
        }   
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<PlayerCore>();
            DisablePassive(player);
        }
        
    }


    public void RunPassive(PlayerCore player)
    {
        player.Movement.SetMaxSpeed(player.Data.MaxRunSpeed);
    }

    public void DisablePassive(PlayerCore player)
    {
        player.Movement.SetMaxSpeed(player.Data.MaxHorizontalSpeed);
    }

    
}
