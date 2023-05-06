using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeAction : MonoBehaviour, IColor
{
    public void Action(GameObject player)
    {
        PlayerMoviment moviment = player.GetComponent<PlayerMoviment>();

        moviment.currentMaxSpeed *= 2;
    }
}
