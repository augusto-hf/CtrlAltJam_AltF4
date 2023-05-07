using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerData data;
    [SerializeField] private PlayerChecks check;
    private PlayerControl input;

    private Rigidbody2D rb;
    private bool canMove;
    private float curretnMaxSpeed;

    public PlayerData Data { get => data;}
    public PlayerControl Input { get => input;}
    private void Awake()
    {
        input = GameObject.FindGameObjectWithTag("InputManager").GetComponent<PlayerControl>();
        canMove = true;
        curretnMaxSpeed = data.MaxHorizontalSpeed;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = data.GravityScale;
    }

    private void FixedUpdate()
    {
        HorizontalMovement();
        VerticalMovement();
    }

    private void HorizontalMovement()
    {
        if (!canMove) return;

        float targetVeloticy = input.Axis.x * curretnMaxSpeed;
        float speedDif = targetVeloticy - rb.velocity.x;

        float accelRate = Mathf.Abs(targetVeloticy) > 0.01f ? data.HorizontalAcceleration : data.HorizontalDeceleration;

        float moviment = speedDif * accelRate;


        rb.AddForce(moviment * Vector2.right);

    }
    private void VerticalMovement()
    {
        Fall();
    }

    private void Fall()
    {
        if (rb.velocity.y < 0 && !check.IsGrounded)
        {
            rb.gravityScale = data.GravityScale * data.FallMultiplier;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -data.MaxFallSpeed));
        }
        else
        {
            rb.gravityScale = data.GravityScale;
        }
    }
    public void SetMaxSpeed(float maxSpeed)
    {
        curretnMaxSpeed = maxSpeed;
    }
    public void Jump()
    {
        float force = data.JumpForce;
        
        if (rb.velocity.y < 0)
            force -= rb.velocity.y;

        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    public void Teleport(Vector2 local)
    {
        rb.velocity = Vector2.zero;
        rb.position = local;
    }
}
