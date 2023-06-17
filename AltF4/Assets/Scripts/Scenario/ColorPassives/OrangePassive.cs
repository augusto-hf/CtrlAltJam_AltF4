using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangePassive : MonoBehaviour, IColor
{
    
    [SerializeField] private ColorData colorData;
    [SerializeField] private ButterflyManager butterlfy;
    public ColorData ColorData { get => colorData;}

    private bool isCharged;

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (!butterlfy.used) return;

        if (other.gameObject.CompareTag("Player"))
        {
            var colorManager = other.gameObject.GetComponent<PlayerColorManager>();
            
            colorManager.ColorParticles.PlayAbsorbColor(colorData.Type);
            
            SetPlayerOrangeColor(colorManager);
            
            if (colorManager.Abilities.StaminaAmount < PlayerStamina.MAX_STAMINA) isCharged = false;

        }   
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!butterlfy.used) return;
        
        if (other.gameObject.CompareTag("Player"))
        {
            var colorManager = other.gameObject.GetComponent<PlayerColorManager>();
            
            SetPlayerOrangeColor(colorManager);

            colorManager.ColorParticles.PlayAbsorbColor(colorData.Type);

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
                
                if (colorManager.Abilities.StaminaAmount == PlayerStamina.MAX_STAMINA && !isCharged)
                {
                    isCharged = true;
                    
                    colorManager.ColorParticles.ResetAbsorbColor();
                    colorManager.ColorParticles.StopAbsorbColor();
                    colorManager.ColorParticles.PlayExplosionColor();
                }
            }

        }   
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        if (!butterlfy.used) return;

        if (other.gameObject.CompareTag("Player"))
        {
            var colorManager = other.gameObject.GetComponent<PlayerColorManager>();
            
            if (colorManager.Abilities.StaminaAmount <= PlayerStamina.MAX_STAMINA)
            {
                SetPlayerOrangeColor(colorManager);
                
                if (colorManager.Abilities.StaminaAmount == PlayerStamina.MAX_STAMINA && !isCharged)
                {
                    isCharged = true;
                    
                    colorManager.ColorParticles.ResetAbsorbColor();
                    colorManager.ColorParticles.StopAbsorbColor();
                    colorManager.ColorParticles.PlayExplosionColor();
                }
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
            
            colorManager.ColorParticles.ResetAbsorbColor();
            colorManager.ColorParticles.StopAbsorbColor();

            isCharged = false;

        }   
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var colorManager = other.gameObject.GetComponent<PlayerColorManager>();

            colorManager.Abilities.TurnOffOrangePassive();
            
            colorManager.ColorParticles.ResetAbsorbColor();
            colorManager.ColorParticles.StopAbsorbColor();
            
            isCharged = false;

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
