using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeAction : MonoBehaviour, IColor
{
    public void Action(PlayerCore player)
    {

        if (player.Input.ColorButton)
        {
            player.Movement.SetMaxSpeed(player.Data.MaxRunSpeed);
            return;
        }
        player.Movement.SetMaxSpeed(player.Data.MaxHorizontalSpeed);
    }   

    public void ResetAction(PlayerCore player)
    {
        player.Movement.SetMaxSpeed(player.Data.MaxHorizontalSpeed);
    }

}
