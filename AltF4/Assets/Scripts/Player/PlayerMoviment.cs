using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{
    [SerializeField] private PlayerData data;
    [SerializeField] private PlayerControl input;
    
    private Rigidbody2D rb;
    private bool canMove;

    private void Awake()
    {
        canMove = true;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HorizontalMovement();
    }

    private void HorizontalMovement()
    {
        if (!canMove) return;

        float targetVeloticy = input.Axis.x * data.MaxHorizontalSpeed;
        float speedDif = targetVeloticy - rb.velocity.x;

        float accelRate = Mathf.Abs(targetVeloticy) > 0.01f ? data.HorizontalAcceleration : data.HorizontalDeceleration;

        float moviment = speedDif * accelRate;


        rb.AddForce(moviment * Vector2.right);

    }
}
