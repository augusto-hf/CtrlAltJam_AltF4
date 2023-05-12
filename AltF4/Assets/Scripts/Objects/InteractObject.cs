using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class InteractObject : MonoBehaviour, IObjectInteractColor
{
    [SerializeField] private bool canInteract;
    [SerializeField] private Transform groundDetectorPoint;
    [SerializeField] private Vector2 size;
    [SerializeField] private LayerMask ground;

    public bool moveTestAAAAAAAAAAAAH = false;
    public Rigidbody2D Rb { get; private set; }
    public bool CanInteract { get => canInteract; }
    public bool playerOnTop { get; set;}
    public Vector2 LastVelocityDirection { get; private set;}

    private BoxCollider2D box;
    private Vector2 lastCenterOfMass;
    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();

    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(Rb.velocity.magnitude) > 0)
        {
            LastVelocityDirection = Rb.velocity.normalized;
        }

    }


    private void OnCollisionEnter2D(Collision2D other) 
    {

        if (PlayerAboveObject(other.transform) && other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D body))
            {
                body.transform.SetParent(this.transform);
            }
        }
        
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        if(PlayerAboveObject(other.transform) && other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D body))
            {
                
            }
        }
    }

    

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (other.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D body))
            {        
                playerOnTop = false;
                body.transform.SetParent(null);
            }
        }
        
    }

    private bool ObjectOnGround()
    {
        var groundCheck = Physics2D.OverlapBox(groundDetectorPoint.position, size, 0, ground);
        return groundCheck;
    }

    private void OnDrawGizmos()
    {
        if (Rb == null) return;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(Rb.position,Rb.position + (Rb.velocity.normalized * Rb.velocity.magnitude));
        
    }

    private bool PlayerAboveObject(Transform player)
    {
        float plataformHeight = this.transform.position.y + box.bounds.extents.y;

        if (player.position.y > plataformHeight)
        {
            return true;
        }

        return false;
    }

}
