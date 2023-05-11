using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PhysicsMaterial2D playerSlope;
    [SerializeField] private PhysicsMaterial2D playerDefault;
    [SerializeField] private PhysicsMaterial2D noFriction;
    private PlayerCore player;
    private Rigidbody2D rb;
    private float currentMaxSpeed;
    private bool isJumping;
    private bool isOnSlop;
    public bool canMove = false;

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
        Debug.DrawRay(this.transform.position, rb.velocity.normalized, Color.blue);
        Checkers();
        Walk();
        VerticalMovement();
    }

    private void Checkers()
    {

        if (player.Check.IsGrounded)
        {
            HasBluePassive = false;
            isJumping = false;

        }

        if (player.Check.isOnSlop && player.Controller.Axis.x == 0)
        {
            Debug.Log("b");
            rb.sharedMaterial = playerSlope;
        }
        else
        {
            rb.sharedMaterial = noFriction;
        }
        
    }

    #region Horizontal Movement
    private void Walk()
    {
        if (!canMove) return;

        if (player.Check.isOnSlop)
        {
            
            float targetVelocityX = player.Check.SlopeDirection.x * -player.Controller.Axis.x * currentMaxSpeed;
            float targetVelocityY = player.Check.SlopeDirection.y * -player.Controller.Axis.x * currentMaxSpeed;
            Vector2 targetVelocity = new Vector2(targetVelocityX, targetVelocityY);

            Debug.Log(targetVelocity);
            
            Vector2 speedToReach = targetVelocity - rb.velocity;
            
            float accelRate = Mathf.Abs(targetVelocity.magnitude) > 0.01f ? player.Data.HorizontalAcceleration : player.Data.HorizontalDeceleration;

            rb.AddForce(speedToReach * accelRate);

        }
        else
        {
            float targetVeloticy = player.Controller.Axis.x * currentMaxSpeed;

            float speedToReach = targetVeloticy - rb.velocity.x;
            
            float accelRate = Mathf.Abs(targetVeloticy) > 0.01f ? player.Data.HorizontalAcceleration : player.Data.HorizontalDeceleration;
            
            float xMovement  = speedToReach * accelRate;
            
            rb.AddForce(Vector2.right * xMovement , ForceMode2D.Force);
        }
        
        
        
        

    }
    public void SetMaxSpeed(float maxSpeed)
    {
        currentMaxSpeed = maxSpeed;
    }

    #endregion 
    
    #region Vertical Movement
    
    private void VerticalMovement()
    {
        
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
        isJumping = true;
        rb.velocity = new Vector2(rb.velocity.x, player.Data.JumpForce);
    }

    public void JumpCutForceApply()
    {
        rb.AddForce(Vector2.down * rb.velocity.y * player.Data.JumpCutMultiplier, ForceMode2D.Impulse);
    }

    public void SetBluePassive() => HasBluePassive = true;
    
    #endregion
    

}
