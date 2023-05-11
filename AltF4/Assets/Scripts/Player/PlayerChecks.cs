using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecks : MonoBehaviour
{
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform groundDetectorPoint;
    [SerializeField] private Vector2 size;
    [SerializeField] private float maxAngleSlope;
    [SerializeField] private float slopeDetectorDistance;
    [SerializeField] private float slopeDetectorOffset;
    
    private PlayerCore player;
    private CapsuleCollider2D capsule;
    public bool IsGrounded { get; private set; }
    public float LastTimeGrounded { get; private set;}
    public bool IsFalling { get; private set; }
    public bool isOnSlop { get; private set; }
    public float SlopeAngle { get; private set; }
    public Vector2 SlopeDirection { get; private set; }

    void Awake()
    {
        player = GetComponent<PlayerCore>();
        capsule = GetComponent<CapsuleCollider2D>();
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        IsGrounded = OnGround();

        CoyoteTime();
        SlopeDetector();

        IsFalling = OnFall();
    }

    private void SlopeDetector()
    {
        Vector2 point = new Vector2(capsule.bounds.center.x + (capsule.bounds.extents.x - slopeDetectorOffset), capsule.bounds.center.y);
        RaycastHit2D hit = Physics2D.Raycast(point, Vector2.down, slopeDetectorDistance, ground);

        var hitColor = hit ? Color.green : Color.red;

        Debug.DrawRay(point, Vector2.down * slopeDetectorDistance, hitColor);

        if (hit)
        {
            SlopeDirection = Vector2.Perpendicular(hit.normal).normalized;
            SlopeAngle = Vector2.Angle(hit.normal, Vector2.up);
            isOnSlop = SlopeAngle != 0;

            Debug.DrawRay(hit.point, SlopeDirection, Color.blue);
            Debug.DrawRay(hit.point, hit.normal, Color.magenta);

        }
    }
   
    private bool OnGround()
    {
        var groundCheck = Physics2D.OverlapBox(groundDetectorPoint.position, size, 0, ground);
        return groundCheck;
    }

    private void CoyoteTime()
    {
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

    private bool OnFall()
    {
        if (player.rb.velocity.y < 0 && !OnGround())
            return true;
        else
            return false;
    }
    
    private void OnDrawGizmos() 
    {
        if (groundDetectorPoint == null) return;

        Gizmos.color = OnGround() ? Color.green : Color.red;
        Gizmos.DrawWireCube(groundDetectorPoint.position, new Vector3(size.x, size.y, 0));

    }
}
