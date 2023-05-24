using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    
    public bool canMove = false;

    [SerializeField] private Vector2 speed;
    private PlayerCore player;
    private Rigidbody2D rb;
    private float currentMaxSpeed;
    private int jumpCharges;
    private bool isJumping;
    private bool isFacingRight;

    public Vector2 Velocity { get => rb.velocity; }
    public bool HasBluePassive { get; private set;}
    public bool IsFacingRight { get => isFacingRight; }
    
    private void Awake()
    {
        player = GetComponent<PlayerCore>();
        rb = GetComponent<Rigidbody2D>();
        
        isFacingRight = this.transform.rotation.eulerAngles.y == 0;


        rb.gravityScale = player.Data.GravityScale;
        currentMaxSpeed = player.Data.MaxHorizontalSpeed;
    }

    private void FixedUpdate()
    {
        Debug.DrawRay(this.transform.position, rb.velocity.normalized, Color.cyan);
        
        Checkers();
        HorizontalMovement();
        VerticalMovement();
        
        speed = Velocity;
    }

    private void Checkers()
    {
        if (player.Health.IsDead)
        {
            StopAllMovement();
            return;            
        }
        else
        {
            RestoreAllMovement();
        }
        
    }

    #region Horizontal Movement

    private void HorizontalMovement()
    {
        Flip();
        Walk();
    }

    private void Walk()
    {
        if (!canMove) return;

        if (player.Check.IsFacingWall)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }

        float targetVeloticy = player.Controller.Axis.x * currentMaxSpeed;
        float speedToReach = targetVeloticy - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetVeloticy) > 0.01f) ? player.Data.HorizontalAcceleration : player.Data.HorizontalDeceleration;

        rb.velocity = new Vector2(rb.velocity.x + (Time.fixedDeltaTime  * speedToReach * accelRate), rb.velocity.y);

    }
    
    public void SetMaxSpeed(float maxSpeed)
    {
        currentMaxSpeed = maxSpeed;
    }
    
    private void Flip()
    {
        if ((isFacingRight && player.Controller.Axis.x < 0 || !isFacingRight && player.Controller.Axis.x > 0))
        {
            if (!player.Controller.TongueButton)
            {
                isFacingRight = !isFacingRight;
                this.transform.Rotate(0, 180, 0);
            }
            else
                return;
        }
    }

    #endregion 
    
    #region Vertical Movement
    
    private void VerticalMovement()
    {
        Fall();
    }

    private void Fall()
    {
        if (player.Check.IsFalling)
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

    public void StopAllMovement()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
    }

    public void RestoreAllMovement()
    {
        rb.isKinematic = false;
        canMove = true;
    }
    

}
