using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecks : MonoBehaviour
{
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform groundDetector;
    [SerializeField] private float detectionRange;
    public bool IsGrounded { get; private set; }

    // Update is called once per frame
    void FixedUpdate()
    {
        IsGrounded = OnGround();
    }
    private bool OnGround()
    {
        var groundCheck = Physics2D.Raycast(groundDetector.position, Vector2.down, detectionRange, ground);
        return groundCheck;
    }
}
