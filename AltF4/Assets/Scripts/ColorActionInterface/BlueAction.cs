using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueAction : MonoBehaviour, IColor
{
    private bool iJumped = false;
    public void Action(GameObject player, bool isPressed)
    {
        PlayerMoviment moveScript = player.GetComponent<PlayerMoviment>();
        PlayerChecks checkScript = player.GetComponent<PlayerChecks>();
        PlayerControl inputScript = moveScript.Input;
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        

        if (isPressed)
        {
            if (checkScript.IsGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, moveScript.Data.JumpForce);
                iJumped = true;
                return;
            }
        }
        else if (!isPressed && iJumped && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            iJumped = false;
            return;
        }
    }
}
