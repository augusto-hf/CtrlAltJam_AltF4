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


        rb.AddForce(speedDif * data.HorizontalAcceleration * Vector2.right);

    }
}
