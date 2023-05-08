using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class InteractObject : MonoBehaviour, IObjectInteractColor
{
    [SerializeField] private bool canInteract;

    public Rigidbody2D Rb { get; private set; }
    public bool CanInteract { get => canInteract; }

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (this.transform.position.y < other.transform.position.y - 0.8f)
            {
                Debug.Log("attach");
                other.transform.SetParent(this.transform);
            }

        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("nao attach");
            other.transform.SetParent(null);

        }    
    }

}
