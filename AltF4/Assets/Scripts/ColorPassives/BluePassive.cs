using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePassive : MonoBehaviour
{
    [SerializeField] private float defaultJumpFoce;
    [SerializeField] private float playerJumpBuff;
    [SerializeField] private float playerInpulseAddtional;
    [SerializeField] private Vector2 direction;

    private void Update()
    {
           
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<PlayerCore>();
            var rb = other.gameObject.GetComponent<Rigidbody2D>();

            player.Movement.SetBluePassive();

            float additionalBuff = 0;
            
            if (player.Color.CurrentColor.Type == ColorType.Blue)
            {
                additionalBuff = playerJumpBuff;
            }
            else
            {
                additionalBuff = playerInpulseAddtional;
            }


            Impulse(rb, player.Data.JumpForce + additionalBuff, direction);

        }
        else if (other.gameObject.TryGetComponent<IObjectInteractColor>(out IObjectInteractColor interactor))
        {
            if (!interactor.CanInteract) return;

            Impulse(interactor.Rb, defaultJumpFoce, direction);
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<PlayerCore>(); 
            player.Movement.SetBluePassive();

        }
        else if (other.gameObject.TryGetComponent<IObjectInteractColor>(out IObjectInteractColor interactor))
        {
            if (!interactor.CanInteract) return;

            Impulse(interactor.Rb, defaultJumpFoce, direction);
        }
    }

    public void Impulse(Rigidbody2D rb, float force, Vector2 direction)
    {
        float currentForce = force;
        Vector2 impulseDirection = Vector2.zero;

        rb.velocity = Vector2.zero;

        impulseDirection = direction == Vector2.zero ? Vector2.up : direction.normalized;

        rb.AddForce(impulseDirection * currentForce, ForceMode2D.Impulse);
    }

}
