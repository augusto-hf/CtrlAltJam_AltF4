using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleAction : MonoBehaviour, IColor
{
    [SerializeField] private Transform teleportPoint;

    private bool isPlacedTeleportBeacon;


    private void Start() 
    {
        ResetTeleportPosition();
    }



    public void Action(GameObject player, bool isPressed)
    {
        if (teleportPoint == null) return;

        if (isPressed) 
        {
            if (!isPlacedTeleportBeacon)
            {
                teleportPoint.position = player.transform.position;
                return;
            }
            else 
            {
                Teleport(player);
            }  
        }
    }

    public void ResetAction(GameObject player)
    {
        ResetTeleportPosition();
    }

    private void Teleport(GameObject player)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.MovePosition(teleportPoint.position);
        ResetTeleportPosition();
    }

    private void ResetTeleportPosition()
    {
        teleportPoint.position = this.transform.position;
        isPlacedTeleportBeacon = false;
    }


}
