using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    [SerializeField] private PlayerData data;
    [SerializeField] private PlayerControl input;
    
    public Rigidbody2D rb;
    private bool canMove;
    private float curretnMaxSpeed;

    private void Awake()
    {
        canMove = true;
        curretnMaxSpeed = data.MaxHorizontalSpeed;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HorizontalMovement();
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

    public void SetMaxSpeed(float maxSpeed)
    {
        curretnMaxSpeed = maxSpeed;
    }
}
