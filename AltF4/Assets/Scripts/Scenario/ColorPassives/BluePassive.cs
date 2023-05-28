using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePassive : MonoBehaviour, IColor
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
            SetPlayerBlueColor(colorManager);

        }   
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        if (!butterlfy.used) return;

        if (other.gameObject.CompareTag("Player"))
        {
            var colorManager = other.gameObject.GetComponent<PlayerColorManager>();

            if (colorManager.Abilities.JumpCharge <= 0)
            {
                SetPlayerBlueColor(colorManager);
            }

        }   
    }



    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!butterlfy.used) return;
        
        if (other.gameObject.CompareTag("Player"))
        {
            var colorManager = other.gameObject.GetComponent<PlayerColorManager>();
            SetPlayerBlueColor(colorManager);

        }   
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if (!butterlfy.used) return;

        if (other.gameObject.CompareTag("Player"))
        {
            var colorManager = other.gameObject.GetComponent<PlayerColorManager>();

            if (colorManager.Abilities.JumpCharge <= 0)
            {
                SetPlayerBlueColor(colorManager);
            }

        }   
    }

    private void SetPlayerBlueColor(PlayerColorManager manager)
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
