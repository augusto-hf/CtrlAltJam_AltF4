using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Obsolete]
public class PlayerAnimation : MonoBehaviour
{
    private PlayerCore player;
    private Animator animator;

    void Awake()
    {
        player = GetComponent<PlayerCore>();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        setWalkAndRun();
        setJumpAndFall();
        setGround();
    }
    #region Horizontal Animation Setting
    private void setWalkAndRun()
    {
        animator.SetInteger("walk_Direction", (int)player.Controller.Axis.x);

        animator.SetBool("isFacingWall", player.Check.IsFacingWall);

        animator.SetBool("tongue", player.Tongue.isTongueOut);

        if (player.ColorManager.CurrentColor.ColorData.Type == ColorType.Orange && Mathf.Abs(player.Controller.Axis.x) > 0)
            animator.SetBool("isRunning", player.Controller.ColorButtonHold);
        else
            animator.SetBool("isRunning", false);
    }

    #endregion
    #region Vertical Animation Setting
    //TODO: bug ao cair do vento 
    private void setJumpAndFall()
    {
        bool isJumpAnim = animator.GetBool("isJumping");
        
        if (player.Abilities.IsJumping)
        {
            animator.SetBool("isJumping", true);
        }
        if (player.Check.IsFalling && !player.Abilities.IsJumping)
        {
            animator.SetBool("isJumping", false);
        }
        if (!player.Check.OnGround() && player.rb.velocity.y > 0.15f && !player.Abilities.IsJumping)
        {
            animator.SetBool("isJumping", true);
        }
        if (isJumpAnim && player.Check.OnGround())
        {
            animator.SetBool("isJumping", false);
        }

        animator.SetBool("isFalling", player.Check.IsFalling);
    }

    private void setGround()
    {
        animator.SetBool("isGrounded", player.Check.IsGrounded);
    }
    #endregion


}
