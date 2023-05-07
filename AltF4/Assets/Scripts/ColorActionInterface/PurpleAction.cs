using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurpleAction : MonoBehaviour, IColor
{
    [SerializeField] private Transform teleportPoint;

    private bool isPlacedTeleportBeacon;
    private Vector2 originPosition;

    private void Awake()
    {
        originPosition = this.transform.position;
    }

    private void Start() 
    {
        ResetTeleportPosition();
    }



    public void Action(GameObject player, bool isPressed)
    {
        if (teleportPoint == null) return;

        if (Input.GetButtonDown("ColorActionButton")) 
        {
            if (!isPlacedTeleportBeacon)
            {
                teleportPoint.gameObject.SetActive(true);
                isPlacedTeleportBeacon = true;
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
        if (isPlacedTeleportBeacon)
        {
            ResetTeleportPosition();
        }
    }

    private void Teleport(GameObject player)
    {
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.zero;
        rb.position = teleportPoint.position;
        ResetTeleportPosition();
    }

    private void ResetTeleportPosition()
    {
        teleportPoint.position = originPosition;
        isPlacedTeleportBeacon = false;
        teleportPoint.gameObject.SetActive(false);
    }


}
