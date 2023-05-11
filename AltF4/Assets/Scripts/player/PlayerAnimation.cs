using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerCore player;
    [SerializeField] private Animator animator;

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

        if (player.Color.CurrentColor.Type == ColorType.Orange)
            animator.SetBool("isRunning", player.Controller.ColorButton);
        else
            animator.SetBool("isRunning", false);
    }

    private void setJumpAndFall()
    {
        if (player.Color.CurrentColor.Type == ColorType.Blue)
            animator.SetBool("isJumping", player.Controller.ColorButton);

        else
            animator.SetBool("isJumping", false);

        animator.SetBool("isFalling", player.Check.IsFalling);
    }
}
