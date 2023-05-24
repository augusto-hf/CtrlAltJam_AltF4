using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerColorManager : MonoBehaviour
{
    [SerializeField] private GameObject StartingColorReference;

    private PlayerCore player;
    private PlayerColorAbilities abilities;
    private IColor currentColor;
    private BlobManager lastBlob;

    public IColor CurrentColor { get => currentColor; }

    private void Awake()
    {
        player = GetComponent<PlayerCore>();
        abilities = GetComponent<PlayerColorAbilities>();
        GiveNoColor();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        TakeColor(other.gameObject);
    }
    
    public void TakeColor(GameObject colorObject)
    {
        if (colorObject.CompareTag("ColorPower"))
        {
            if (lastBlob != null)
                lastBlob.RespawnPower();

            lastBlob = colorObject.gameObject.GetComponentInParent<BlobManager>();
            lastBlob.PickPower();

            
            if (currentColor != null)
            {
                if (currentColor.ColorData.Type != lastBlob.blobColor.ColorData.Type)
                {
                    abilities.ResetBuffs();
                }
            }

            currentColor = lastBlob.blobColor;
            abilities.SetConsumeBuffs(currentColor.ColorData);
            player.PickColor(lastBlob.nameColor);
        }
    }

    public void ConsumeColor()
    {
        if (lastBlob == null ) return;

        lastBlob.RespawnPower();
        lastBlob = null;
        GiveNoColor();
        abilities.SetConsumeBuffs(currentColor.ColorData);
    }

    public void GiveNoColor()
    {
        currentColor = StartingColorReference.gameObject.GetComponent<IColor>();
    }
}
