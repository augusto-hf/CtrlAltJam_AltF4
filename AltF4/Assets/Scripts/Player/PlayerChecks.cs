using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecks : MonoBehaviour
{
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform groundDetectorPoint;
    [SerializeField] private Vector2 size;
    private PlayerCore player;
    private CapsuleCollider2D capsule;
    public bool IsGrounded { get; private set; }
    public float LastTimeGrounded { get; private set;}

    void Awake()
    {
        player = GetComponent<PlayerCore>();
        capsule = GetComponent<CapsuleCollider2D>();
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

    public bool IsOnSlop(out Vector2 slopeDirection)
    {
        Vector2 direction = this.transform.right;
        bool onSlop = false;
        Vector2 point = new Vector2(this.transform.position.x + (capsule.bounds.extents.x - 0.2f), this.transform.position.y);

        RaycastHit2D ray = Physics2D.Raycast(point, Vector2.down, 2f, ground);
        Debug.DrawRay(point, Vector3.down, Color.green);

        if (ray)
        {
            Debug.DrawRay(ray.point, ray.normal, Color.magenta);
            Debug.DrawRay(ray.point, Vector2.Perpendicular(ray.normal), Color.blue);
            
            onSlop = true;
            direction = Vector2.Perpendicular(ray.normal).normalized;

        }

        slopeDirection = direction;
        return onSlop;

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
