using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecks : MonoBehaviour
{
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform groundDetector;
    [SerializeField] private float detectionRange;
    private PlayerCore player;
    public bool IsGrounded { get; private set; }
    public float LastTimeGrounded { get; private set;}

    void Awake()
    {
        player = GetComponent<PlayerCore>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        IsGrounded = OnGround();
        
        if (OnGround())
        {
            LastTimeGrounded = player.Data.CoyoteTime;
        }
        else if (!OnGround())
        {
            LastTimeGrounded -= Time.deltaTime;
            if (LastTimeGrounded < 0)
            {
                LastTimeGrounded = 0;
            }
        }
    
    }
    private bool OnGround()
    {
        var groundCheck = Physics2D.Raycast(groundDetector.position, Vector2.down, detectionRange, ground);
        
        return groundCheck;
    }
}
