using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeAction : MonoBehaviour, IColor
{
    public void Action(GameObject player, bool isPressed)
    {
        PlayerMovement moviment = player.GetComponent<PlayerMovement>();

        if (isPressed) {
            moviment.SetMaxSpeed(moviment.Data.MaxRunSpeed);
            return;
        }
        moviment.SetMaxSpeed(moviment.Data.MaxHorizontalSpeed);
    }   

    public void ResetAction(GameObject player)
    {
        PlayerMovement moviment = player.GetComponent<PlayerMovement>();
        moviment.SetMaxSpeed(moviment.Data.MaxHorizontalSpeed);
    }

}
