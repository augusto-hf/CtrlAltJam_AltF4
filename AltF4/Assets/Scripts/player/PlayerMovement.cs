using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerCore player;
    private Rigidbody2D rb;
    private bool canMove;
    private float currentMaxSpeed;
    private int jumpCount;
    private bool isJumping = false;
    
    private void Awake()
    {
        player = GetComponent<PlayerCore>();
        rb = GetComponent<Rigidbody2D>();

        jumpCount = 1;
        canMove = true;
        
        rb.gravityScale = player.Data.GravityScale;
        currentMaxSpeed = player.Data.MaxHorizontalSpeed;
    }

    private void FixedUpdate()
    {
        Walk();
        VerticalMovement();
    }

    #region Horizontal Movement
    private void Walk()
    {
        if (!canMove) return;

        float targetVeloticy = player.Input.Axis.x * currentMaxSpeed;
        float speedDif = targetVeloticy - rb.velocity.x;

        float accelRate = Mathf.Abs(targetVeloticy) > 0.01f ? player.Data.HorizontalAcceleration : player.Data.HorizontalDeceleration;

        float movement = speedDif * accelRate;


        rb.AddForce(movement * Vector2.right);

    }
    public void SetMaxSpeed(float maxSpeed)
    {
        currentMaxSpeed = maxSpeed;
    }

    #endregion 
    
    #region Vertical Movement
    
    private void VerticalMovement()
    {
        Jump();
        Fall();
    }

    private void Fall()
    {
        if (rb.velocity.y < 0 && !player.Check.IsGrounded)
        {
            rb.gravityScale = player.Data.GravityScale * player.Data.FallMultiplier;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -player.Data.MaxFallSpeed));
        }
        else
        {
            rb.gravityScale = player.Data.GravityScale;
        }
    }

    #endregion

    #region Jump logic

    private void Jump()
    {
        
        if (player.Input.isJumping && CanJump())
        {
            JumpForceApply();
            isJumping = true;
            return;
        }
        else if (!player.Input.isJumping && isJumping && rb.velocity.y > 0)
        {
            JumpCutForceApply();
            

        }

        if (player.Check.IsGrounded)
        {
            isJumping = false;
        }


    }
    public void JumpForceApply()
    {
        rb.velocity = new Vector2(rb.velocity.x, player.Data.JumpForce);
        isJumping = false;
    }
    private void JumpCutForceApply()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
    }
    public void JumpPad()
    {
        float force = player.Data.JumpForce;
        
        if (rb.velocity.y < 0)
            force -= rb.velocity.y;

        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }
    private bool CanJump()
    {
        return player.Check.LastTimeGrounded > 0 && !isJumping;
    }
    
    #endregion
    
    public void Teleport(Vector2 local)
    {
        rb.velocity = Vector2.zero;
        rb.position = local;
    }

}
