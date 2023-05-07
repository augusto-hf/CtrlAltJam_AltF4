using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueAction : MonoBehaviour, IColor
{
    private bool isJumping = false;
    private float coyoteCurrentTimer;
    private PlayerCore player;
    public void Action(PlayerCore player)
    {    
        this.player = player;
    }
    private void FixedUpdate()
    {
        Jump(player);
    }

    public void ResetAction(PlayerCore player)
    {
        this.player = null;
    }

    private void Jump(PlayerCore player)
    {
        if (player == null) return;

        if (player.Input.ColorButton && CanJump(player))
        {
            player.Movement.JumpForceApply();
            isJumping = true;
            return;
        }
        else if (!player.Input.ColorButton && isJumping && player.Movement.Velocity.y > 0)
        {
            player.Movement.JumpCutForceApply();

        }

        if (player.Check.IsGrounded)
        {
            isJumping = false;
        }
    }

    private bool CanJump(PlayerCore player)
    {
        return player.Check.LastTimeGrounded > 0 && !isJumping;
    }

}
