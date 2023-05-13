using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecks : MonoBehaviour
{
    [SerializeField] private LayerMask solid;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform groundDetectorPoint;
    [SerializeField] private Vector2 size;
    [SerializeField] private float maxAngleSlope;
    [SerializeField] private float slopeDetectorDistance;
    [SerializeField] private float slopeDetectorOffset;
    [SerializeField] private float wallCheckDistance;
    
    private PlayerCore player;
    private CapsuleCollider2D capsule;
    public bool IsGrounded { get; private set; }
    public float LastTimeGrounded { get; private set;}
    public bool IsFalling { get; private set; }
    public bool isOnSlop { get; private set; }
    public bool IsFacingWall {get; private set;}
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
        OnWall();

        IsFalling = OnFall();
    }

    private void SlopeDetector()
    {
        int direction = player.Movement.IsFacingRight ? 1 : -1;
        Vector2 pointRight = new Vector2(capsule.bounds.center.x + (capsule.bounds.extents.x - slopeDetectorOffset) * direction , capsule.bounds.center.y);
        Vector2 pointLeft = new Vector2(capsule.bounds.center.x - (capsule.bounds.extents.x - slopeDetectorOffset) * direction , capsule.bounds.center.y);

        RaycastHit2D hitRight = Physics2D.Raycast(pointRight, Vector2.down, slopeDetectorDistance, ground);
        RaycastHit2D hitLeft = Physics2D.Raycast(pointLeft, Vector2.down, slopeDetectorDistance, ground);

        bool hit = hitLeft || hitRight;

        var hitColor = hit ? Color.green : Color.red;

        Debug.DrawRay(pointRight, Vector2.down * slopeDetectorDistance, hitColor);
        Debug.DrawRay(pointLeft, Vector2.down * slopeDetectorDistance, hitColor);

        if (hitRight)
        {
            SlopeDirection = Vector2.Perpendicular(hitRight.normal).normalized;
            SlopeAngle = Vector2.Angle(hitRight.normal, Vector2.up);
            isOnSlop = SlopeAngle != 0;

            Debug.DrawRay(hitRight.point, SlopeDirection, Color.blue);
            Debug.DrawRay(hitRight.point, hitRight.normal, Color.magenta);

        }
        else if (hitLeft)
        {
            SlopeDirection = Vector2.Perpendicular(hitLeft.normal).normalized;
            SlopeAngle = Vector2.Angle(hitLeft.normal, Vector2.up);
            isOnSlop = SlopeAngle != 0;

            Debug.DrawRay(hitLeft.point, SlopeDirection, Color.blue);
            Debug.DrawRay(hitLeft.point, hitLeft.normal, Color.magenta);
        }

    }
   
    public bool OnGround()
    {
        var groundCheck = Physics2D.OverlapBox(groundDetectorPoint.position, size, 0, solid);
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

    public bool OnFall()
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

    public void OnWall()
    {
        int direction = player.Movement.IsFacingRight ? 1 : -1;
        Vector2 point = new Vector2(capsule.bounds.center.x + capsule.bounds.extents.x  * direction , capsule.bounds.center.y);
        RaycastHit2D wallPoint = Physics2D.Raycast(point, this.transform.right, wallCheckDistance, ground);
        var hitColor = wallPoint ? Color.green : Color.red;

        Debug.DrawRay(point, this.transform.right * wallCheckDistance, hitColor);

        if (wallPoint)
        {
            IsFacingWall = Vector2.Angle(wallPoint.normal, Vector2.up) == 0;
        }
        else
        {
            IsFacingWall = false;
        }
        
    }

}
