using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeAction : MonoBehaviour, IColor
{
    public void Action(GameObject player, bool isPressed)
    {
        PlayerMoviment moviment = player.GetComponent<PlayerMoviment>();

        if (isPressed)
            moviment.SetMaxSpeed(moviment.Data.MaxRunSpeed);
        else
            moviment.SetMaxSpeed(moviment.Data.MaxHorizontalSpeed);
    }   
}
