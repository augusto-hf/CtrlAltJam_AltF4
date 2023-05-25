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
    public PlayerColorAbilities Abilities { get => abilities; }
    public BlobManager LastBlob { get => lastBlob; }

    private void Awake()
    {
        player = GetComponent<PlayerCore>();
        abilities = GetComponent<PlayerColorAbilities>();
        GiveNoColor();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        TakeBlobColor(other.gameObject);
    }
    
    public void TakeBlobColor(GameObject colorObject)
    {
        if (colorObject.CompareTag("ColorPower"))
        {
            RespawnLastBlob();

            lastBlob = colorObject.gameObject.GetComponentInParent<BlobManager>();
            lastBlob.PickPower();

            
            if (currentColor != null)
            {
                if (currentColor.ColorData.Type != lastBlob.blobColor.ColorData.Type)
                {
                    abilities.ResetAllBuffs();
                }
            }

            currentColor = lastBlob.blobColor;
            abilities.SetConsumeBuffs(currentColor.ColorData);
            player.PickColor(lastBlob.nameColor);
        }
    }

    public void TakePassiveColor(IColor color)
    {
        ConsumeColor();
        currentColor = color;
        abilities.SetPassiveBuff(color.ColorData);
    }

    public void ConsumeColor()
    {
        RespawnLastBlob();
        GiveNoColor();
        abilities.SetConsumeBuffs(currentColor.ColorData);
    }

    private void RespawnLastBlob()
    {
        if (lastBlob == null) return;

        lastBlob.RespawnPower();
        lastBlob = null;

    }

    public void GiveNoColor()
    {
        currentColor = StartingColorReference.gameObject.GetComponent<IColor>();
    }
}
