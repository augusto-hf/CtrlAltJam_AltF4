using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangePassive : MonoBehaviour, IColor
{
    [SerializeField] private ColorData colorData;
    [SerializeField] private ButterflyManager butterlfy;
    public ColorData ColorData { get => colorData;}

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (!butterlfy.used) return;

        if (other.gameObject.CompareTag("Player"))
        {
            var colorManager = other.gameObject.GetComponent<PlayerColorManager>();

            SetPlayerOrangeColor(colorManager);

        }   
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!butterlfy.used) return;
        
        if (other.gameObject.CompareTag("Player"))
        {
            var colorManager = other.gameObject.GetComponent<PlayerColorManager>();
            SetPlayerOrangeColor(colorManager);

        }   
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if (!butterlfy.used) return;

        if (other.gameObject.CompareTag("Player"))
        {
            var colorManager = other.gameObject.GetComponent<PlayerColorManager>();

            if (colorManager.Abilities.StaminaAmount <= PlayerStamina.MAX_STAMINA )
            {
                SetPlayerOrangeColor(colorManager);
            }

        }   
    }

    private void OnCollisionStay(Collision other) 
    {
        if (!butterlfy.used) return;

        if (other.gameObject.CompareTag("Player"))
        {
            var colorManager = other.gameObject.GetComponent<PlayerColorManager>();

            if (colorManager.Abilities.StaminaAmount <= PlayerStamina.MAX_STAMINA)
            {
                SetPlayerOrangeColor(colorManager);
            }

        }   
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if (!butterlfy.used) return;
        
        if (other.gameObject.CompareTag("Player"))
        {
            var colorManager = other.gameObject.GetComponent<PlayerColorManager>();

            colorManager.Abilities.TurnOffOrangePassive();

        }   
    }

    private void SetPlayerOrangeColor(PlayerColorManager manager) 
    {
        if (manager.CurrentColor.ColorData.Type == colorData.Type) 
        {
            manager.Abilities.SetPassiveBuff(ColorData);
        }
        else
        {
            manager.TakePassiveColor(this);
        }
    }

}
