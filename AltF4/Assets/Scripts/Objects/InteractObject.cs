using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class InteractObject : MonoBehaviour, IObjectInteractColor
{
    [SerializeField] private bool canInteract;

    public Rigidbody2D Rb { get; private set; }
    public bool CanInteract { get => canInteract; }
    public bool CanMove { get; set;}
    public Vector2 Direction { get; set;}

    private BoxCollider2D box;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        box = GetComponent<BoxCollider2D>();

    }

    private void FixedUpdate()
    {
        if (CanMove)
        {
            Rb.AddForce(Direction.normalized * 2);
        }
    }


    private void OnCollisionEnter2D(Collision2D other) 
    {
        float plataformHeight = this.transform.position.y + box.bounds.extents.y;

        if (other.transform.position.y > plataformHeight)
        {
            if (other.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D body))
            {
                body.transform.SetParent(this.transform);
            }
        }
        
    }

    

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<Rigidbody2D>(out Rigidbody2D body))
        {
            body.transform.SetParent(null);
        }
    }

}
