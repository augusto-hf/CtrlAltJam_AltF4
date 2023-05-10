using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecks : MonoBehaviour
{
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform groundDetectorPoint;
    [SerializeField] private Vector2 size;
    [SerializeField] private float maxAngleSlope;
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

    public bool IsOnSlop(out float slopeAngle)
    {
        float angle = 0;
        Vector2 point = new Vector2(this.transform.position.x + (capsule.bounds.extents.x - 0.2f), this.transform.position.y - capsule.bounds.extents.y);

        RaycastHit2D ray = Physics2D.Raycast(point, this.transform.right, 0.2f, ground);
        Debug.DrawRay(point, this.transform.right * 0.2f, Color.green);

        if (ray)
        {
            Debug.DrawRay(ray.point, ray.normal, Color.magenta);
            Debug.DrawRay(ray.point, Vector2.Perpendicular(ray.normal), Color.blue);
            
            angle = Vector2.Angle(ray.normal, this.transform.up);

        }

        slopeAngle = angle;

        bool onSlop = angle > maxAngleSlope;

        return onSlop;

    }
    public bool IsOnSlop()
    {
        float angle = 0;
        Vector2 point = new Vector2(this.transform.position.x + (capsule.bounds.extents.x - 0.2f), this.transform.position.y - capsule.bounds.extents.y);

        RaycastHit2D ray = Physics2D.Raycast(point, this.transform.right, 0.2f, ground);

        if (ray)
        {
            angle = Vector2.Angle(ray.normal, this.transform.up);

        }

        bool onSlop = angle > maxAngleSlope;

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
