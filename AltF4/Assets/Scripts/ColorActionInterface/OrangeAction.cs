using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeAction : MonoBehaviour, IColor
{
    public void Action(GameObject player, bool isPressed)
    {
        PlayerCore core = player.GetComponent<PlayerCore>();

        if (isPressed) {
            core.Movement.SetMaxSpeed(core.Data.MaxRunSpeed);
            return;
        }
        core.Movement.SetMaxSpeed(core.Data.MaxHorizontalSpeed);
    }   

    public void ResetAction(GameObject player)
    {
        PlayerCore core = player.GetComponent<PlayerCore>();
        core.Movement.SetMaxSpeed(core.Data.MaxHorizontalSpeed);
    }

}
