using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecks : MonoBehaviour
{
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform groundDetectorPoint;
    [SerializeField] private Vector2 size;
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
        var groundCheck = Physics2D.OverlapBox(groundDetectorPoint.position, size, 0, ground);
        return groundCheck;
    }

    private void OnDrawGizmos() 
    {
        if (groundDetectorPoint == null) return;

        Gizmos.color = OnGround() ? Color.green : Color.red;
        Gizmos.DrawWireCube(groundDetectorPoint.position, new Vector3(size.x, size.y, 0));

    }
}
