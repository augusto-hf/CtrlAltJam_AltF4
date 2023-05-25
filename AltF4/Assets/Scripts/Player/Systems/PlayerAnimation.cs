using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        animator.SetInteger("walk_Direction", (int)player.Controller.Axis.x);
        animator.SetBool("tongue", player.Controller.TongueButton);

        setJumpAndFall();

        if (player.ColorManager.CurrentColor.ColorData.Type == ColorType.Orange && Mathf.Abs(player.Controller.Axis.x) > 0)
            animator.SetBool("isRunning", player.Controller.ColorButtonHold);
        else
            animator.SetBool("isRunning", false);     

    }

    //TODO: bug ao cair do vento 
    private void setJumpAndFall()
    {
        if (player.ColorManager.CurrentColor.ColorData.Type == ColorType.Blue)
        {
            animator.SetBool("isJumping", player.Abilities.IsJumping);
        }
        if(player.Check.IsFalling && !player.Abilities.IsJumping)
        {
            animator.SetBool("isJumping", false);
        }
        if(!player.Check.OnGround() && !player.Check.IsFalling && !player.Abilities.IsJumping)
        {
            animator.SetBool("isJumping", true);
        }

        animator.SetBool("isFalling", player.Check.IsFalling);
    }


}
