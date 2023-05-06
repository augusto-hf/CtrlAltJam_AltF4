using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChecks : MonoBehaviour
{
    [SerializeField] private LayerMask ground;
    public bool IsGrounded { get; private set; }

    // Update is called once per frame
    void FixedUpdate()
    {
        IsGrounded = OnGround();
    }
    private bool OnGround()
    {
        var groundCheck = Physics2D.Raycast(transform.position, Vector2.down, 0.7f);
        return groundCheck.collider != null && groundCheck.collider.CompareTag("Ground");
    }
}
