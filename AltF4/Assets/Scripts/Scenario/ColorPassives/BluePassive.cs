using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePassive : MonoBehaviour, IColor
{
    [SerializeField] private float defaultAbsorveTime;
    [SerializeField] private ColorData colorData;
    [SerializeField] private ButterflyManager butterlfy;

    public ColorData ColorData { get => colorData;}

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (!butterlfy.used) return;

        if (other.gameObject.CompareTag("Player"))
        {
            var colorManager = other.gameObject.GetComponent<PlayerColorManager>();

            if (colorManager.Abilities.JumpCharge <= 0)
            {
                StartCoroutine(TimeToAbsorve(defaultAbsorveTime, colorManager));
            }

        }   
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (!butterlfy.used) return;
        
        if (other.gameObject.CompareTag("Player"))
        {
            var colorManager = other.gameObject.GetComponent<PlayerColorManager>();
            
            if (colorManager.Abilities.JumpCharge <= 0)
            {
                StartCoroutine(TimeToAbsorve(defaultAbsorveTime, colorManager));
            }

        }   
    }


    private void OnCollisionExit2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var colorManager = other.gameObject.GetComponent<PlayerColorManager>();

            StopAllCoroutines();
            
            colorManager.ColorParticles.ResetAbsorbColor();
            colorManager.ColorParticles.StopAbsorbColor();

        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var colorManager = other.gameObject.GetComponent<PlayerColorManager>();

            StopAllCoroutines();
            
            colorManager.ColorParticles.ResetAbsorbColor();
            colorManager.ColorParticles.StopAbsorbColor();

        }
    }


    private void SetPlayerBlueColor(PlayerColorManager manager)
    {
        if (manager.CurrentColor.ColorData.Type == colorData.Type)
        {
            manager.RespawnLastBlob();
            manager.Abilities.SetPassiveBuff(ColorData);
        }
        else
        {
            manager.TakePassiveColor(this);
        }
    }

    private IEnumerator TimeToAbsorve(float time, PlayerColorManager manager)
    {
        manager.ColorParticles.PlayAbsorbColor(colorData.Type);
        
        yield return new WaitForSeconds(time);
        
        manager.ColorParticles.StopAbsorbColor();
        manager.ColorParticles.PlayExplosionColor();

        SetPlayerBlueColor(manager);
    }

}
