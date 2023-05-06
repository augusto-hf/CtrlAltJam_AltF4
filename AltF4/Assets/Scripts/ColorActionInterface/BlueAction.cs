using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueAction : MonoBehaviour, IColor
{
    [SerializeField] private PlayerData dataScript;
    [SerializeField] private PlayerControl inputScript;
    [SerializeField] private PlayerMoviment moveScript;
    [SerializeField] private PlayerChecks checkScript;
    public void Action(GameObject player)
    {
        if (checkScript.IsGrounded)
            player.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, dataScript.JumpForce));
    }
}
