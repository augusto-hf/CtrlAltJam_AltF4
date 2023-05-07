using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePassive : MonoBehaviour
{
    [SerializeField] private float defaultJumpFoce;

    private void Update()
    {
           
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var movement = other.gameObject.GetComponent<PlayerMovement>();
            var rb = other.gameObject.GetComponent<Rigidbody2D>();
            movement.JumpForceApply();
        }
        else if (other.gameObject.TryGetComponent<IObjectInteractColor>(out IObjectInteractColor interactor))
        {
            JumpImpulse(interactor.Rb);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent<IObjectInteractColor>(out IObjectInteractColor interactor))
        {
            JumpImpulse(interactor.Rb);
        }
    }

    public void JumpImpulse(Rigidbody2D rb)
    {
        float force = defaultJumpFoce;
        
        if (rb.velocity.y < 0)
            force -= rb.velocity.y;

        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
    }

    

}
