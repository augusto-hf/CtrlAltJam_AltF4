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
            var player = other.gameObject.GetComponent<PlayerCore>();
            var rb = other.gameObject.GetComponent<Rigidbody2D>();

            player.Movement.JumpBuff();
            JumpImpulse(rb, player.Data.JumpForce);

        }
        else if (other.gameObject.TryGetComponent<IObjectInteractColor>(out IObjectInteractColor interactor))
        {
            if (!interactor.CanInteract) return;

            JumpImpulse(interactor.Rb, defaultJumpFoce);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<PlayerCore>(); 
            player.Movement.JumpBuff();

        }
        else if (other.gameObject.TryGetComponent<IObjectInteractColor>(out IObjectInteractColor interactor))
        {
            if (!interactor.CanInteract) return;
            JumpImpulse(interactor.Rb, defaultJumpFoce);
        }
    }

    public void JumpImpulse(Rigidbody2D rb, float force)
    {
        float currentForce = force;
        
        if (rb.velocity.y < 0)
            currentForce -= rb.velocity.y;

        rb.AddForce(Vector2.up * currentForce, ForceMode2D.Impulse);
    }

    

}
