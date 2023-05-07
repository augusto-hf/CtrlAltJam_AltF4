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
        if (other.gameObject.TryGetComponent<IObjectInteractColor>(out IObjectInteractColor interactor))
        {
            interactor.Rb.velocity = Vector2.zero;
            MovePassiveObject(interactor.Rb);
        }
    }
    private void OnCollisionStay2D(Collision2D other) 
    {
        if (other.gameObject.TryGetComponent<IObjectInteractColor>(out IObjectInteractColor interactor))
        {
            Debug.Log("Dynamic Object");
            MovePassiveObject(interactor.Rb);
        }
        
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<PlayerCore>();
            DisablePlayerPassive(player);
        }
        else if(other.gameObject.TryGetComponent<IObjectInteractColor>(out IObjectInteractColor interactor))
        {
            interactor.Rb.velocity = Vector2.zero;
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

    private void MovePassiveObject(Rigidbody2D rb)
    {
        //Debug.Log(rb.velocity.x);
        float targetVeloticy = xDirection * objectPassiveSpeed;
        float speedDiff = targetVeloticy - rb.velocity.x;

        rb.AddForce(speedDiff * Vector2.right);

    }

}
