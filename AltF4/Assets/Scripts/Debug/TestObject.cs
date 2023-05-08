using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestObject : MonoBehaviour
{
    
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        rb.velocity = Vector2.right * 2;
        
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(this.transform);
        }
        
    }
    private void OnCollisionExit2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
        
    }
}
