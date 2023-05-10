using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerCore player;
    private Rigidbody2D rb;
    public bool canMove = false;
    private float currentMaxSpeed;

    public Vector2 Velocity { get => rb.velocity; }
    public bool HasBluePassive { get; private set;}
    
    private void Awake()
    {
        player = GetComponent<PlayerCore>();
        rb = GetComponent<Rigidbody2D>();
        
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

        Vector2 direction = Vector2.right;
        
        float targetVeloticy = player.Controller.Axis.x * currentMaxSpeed;
        float speedDif = targetVeloticy - rb.velocity.x;
        float accelRate = Mathf.Abs(targetVeloticy) > 0.01f ? player.Data.HorizontalAcceleration : player.Data.HorizontalDeceleration;
        float movement = speedDif * accelRate;

        if (player.Check.IsOnSlop(out Vector2 slopDir))
        {
            movement *= -1;
            direction = slopDir;
        }
        else 
        {

        }

        rb.AddForce(movement * direction);

        Debug.Log($" foce added : { movement} direction { direction}");

    }
    public void SetMaxSpeed(float maxSpeed)
    {
        currentMaxSpeed = maxSpeed;
    }

    #endregion 
    
    #region Vertical Movement
    
    private void VerticalMovement()
    {
        if (player.Check.IsGrounded)
        {
            HasBluePassive = false;
        }
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

    #region Jump

    public void JumpForceApply()
    {
        rb.velocity = new Vector2(rb.velocity.x, player.Data.JumpForce);
    }

    public void JumpCutForceApply()
    {
        rb.AddForce(Vector2.down * rb.velocity.y * player.Data.JumpCutMultiplier, ForceMode2D.Impulse);
    }

    public void SetBluePassive() => HasBluePassive = true;
    
    #endregion
    

}
