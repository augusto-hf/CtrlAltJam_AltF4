using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorAbilities : MonoBehaviour
{
    private PlayerCore player;
    private BlobManager lastBlob;

    private int jumpCharge;
    private int staminaAmount;

    public int StaminaAmount { get => staminaAmount; }
    public int JumpCharge { get => jumpCharge; }

    private void Awake()
    {
        player = GetComponent<PlayerCore>();
    }

    private void Update()
    {
        if (player.Controller.ColorButton)
        {
            Debug.Log(player.ColorManager.CurrentColor.ColorData.Type);
        }
    }

    public void SetConsumeBuffs(ColorData data)
    {
        Debug.Log("a");
        switch(data.Type)
        {
            case ColorType.Blue :
                
                jumpCharge += data.JumpCharge;
                
                break;
            
            case ColorType.Orange:
                
                staminaAmount = data.StaminaAmount;
                
                break;
            
            default:
                
                ResetBuffs();
                
                break;


        }
    }
    public void SetPassiveBuff(ColorData data)
    {
        switch(data.Type)
        {
            case ColorType.Blue :
                
                jumpCharge += data.JumpCharge;
                
                break;
            
            case ColorType.Orange:
                
                staminaAmount += data.StaminaAmount;
                
                break;
            
            default:
                
                ResetBuffs();
                
                break;
        }

    }
    public void ResetBuffs()
    {
        staminaAmount = 0;
        jumpCharge = 0;
    }
}
