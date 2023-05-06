using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueAction : MonoBehaviour, IColor
{
    [SerializeField] private PlayerControl inputScript;
    public void Action(GameObject player, bool isPressed)
    {
        PlayerMoviment moveScript = player.GetComponent<PlayerMoviment>();
        PlayerChecks checkScript = player.GetComponent<PlayerChecks>();
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();

        if (checkScript.IsGrounded && isPressed)
        {
            rb.velocity = new Vector2(rb.velocity.x, moveScript.Data.JumpForce);
            return;
        }

    }
}
