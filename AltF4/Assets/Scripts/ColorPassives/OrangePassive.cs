using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangePassive : MonoBehaviour
{
    [SerializeField] private float objectPassiveSpeed;
    [SerializeField] private int xDirection;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<PlayerCore>();
            ActivatePlayerPassive(player);
        }
        else if (other.gameObject.TryGetComponent<IObjectInteractColor>(out IObjectInteractColor interactor))
        {
            if (!interactor.CanInteract) return;

            MovePassiveObjectForce(interactor.Rb);
        }
    }
    private void OnCollisionStay2D(Collision2D other) 
    {
        if (other.gameObject.TryGetComponent<IObjectInteractColor>(out IObjectInteractColor interactor))
        {
            if (!interactor.CanInteract) return;

            MovePassiveObjectForce(interactor.Rb);
        }
        
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<PlayerCore>();

            DisablePlayerPassive(player);
        }

        if (other.gameObject.TryGetComponent<IObjectInteractColor>(out IObjectInteractColor interactor))
        {
            if (!interactor.CanInteract) return;
            
        }
    
        
    }


    public void ActivatePlayerPassive(PlayerCore player)
    {
        player.Movement.SetMaxSpeed(player.Data.MaxRunSpeed);
    }

    public void DisablePlayerPassive(PlayerCore player)
    {
        player.Movement.SetMaxSpeed(player.Data.MaxHorizontalSpeed);
    }

    private void MovePassiveObjectForce(Rigidbody2D rb)
    {
        float targetVelocity = xDirection * objectPassiveSpeed;
        float speedDiff = targetVelocity - rb.velocity.x;

        rb.AddForce((Vector2.right * xDirection) * objectPassiveSpeed);

    }

}
