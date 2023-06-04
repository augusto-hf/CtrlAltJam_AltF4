using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorAbilities : MonoBehaviour
{
    [SerializeField] private float staminaTest;
    
    private PlayerCore player;
    private ColorType currentAbilityType;
    private BlobManager lastBlob;
    private PlayerStamina stamina;

    private int jumpCharge;
    private bool isJumping;

    private float staminaRefreshMultiplier;
    private bool canStaminaRefresh;

    public float StaminaAmount { get => stamina.CurrentStamina; }
    public int JumpCharge { get => jumpCharge; }
    public bool IsJumping { get => isJumping; }

    private void Awake()
    {
        player = GetComponent<PlayerCore>();
        stamina = new PlayerStamina(); 
    }

    private void Update()
    {
        staminaTest = stamina.CurrentStamina;

        switch(currentAbilityType)
        {
            case ColorType.Blue :
                JumpControl();
                break;
            
            case ColorType.Orange:
                Run();
                break;

            case ColorType.NoColor:
                break;
            
            default:
                break;
        }

        JumpUpdate();

    }

    #region Run

    private void Run()
    {
        if (!player.Controller.ColorButtonHold && stamina.CurrentStamina < PlayerStamina.MAX_STAMINA && canStaminaRefresh)
        {
            stamina.IncreaseStamina(staminaRefreshMultiplier);
            return;
        }

        if (player.Controller.ColorButtonHold && Mathf.Abs(player.Controller.Axis.x) > 0 && stamina.CurrentStamina > PlayerStamina.MIN_STAMINA)
        {
            if (canStaminaRefresh)
            {
                if (stamina.CurrentStamina - player.Data.StaminaDropMultiplier <= PlayerStamina.MIN_STAMINA)
                {
                    stamina.DecreaseStamina(0);
                }
            }
            else
            {
                stamina.DecreaseStamina(player.Data.StaminaDropMultiplier);
            }

            player.Movement.SetMaxSpeed(player.Data.MaxRunSpeed);
                
        }
        else
        {
            player.Movement.SetMaxSpeed(player.Data.MaxHorizontalSpeed);
        }
        
        if (stamina.CurrentStamina <= PlayerStamina.MIN_STAMINA)
        {
            
            player.ColorManager.ConsumeColor();
        }

    }

    #endregion

    #region Jump
    private void JumpControl()
    {

        if (player.Controller.ColorButtonDown && CanJump())
        {
            jumpCharge--;
            isJumping = true;
            JumpForceApply();
        }
        else if (player.Controller.ColorButtonUp && player.Movement.Velocity.y > 0 && isJumping)
        {
            JumpCutForceApply();
        }
        
        
    }

    public void JumpForceApply()
    {
        isJumping = true;

        player.rb.velocity = new Vector2(player.rb.velocity.x, 0);
        player.rb.AddForce(Vector2.up * player.Data.JumpForce, ForceMode2D.Impulse);
    }

    public void JumpCutForceApply()
    {
        isJumping = false;
        player.rb.AddForce(Vector2.down * player.rb.velocity.y * player.Data.JumpCutMultiplier, ForceMode2D.Impulse);
    }
    
    private bool CanJump()
    {
        return !isJumping && jumpCharge > 0;
    }

    private void JumpUpdate()
    {
        if (jumpCharge <= 0 && currentAbilityType == ColorType.Blue)
        {
            player.ColorManager.ConsumeColor();
        }
        
        if (player.Check.IsFalling)
        {
            isJumping = false;
        }
    }

    #endregion

    #region Control Buffs
    public void SetConsumeBuffs(ColorData data)
    {
        currentAbilityType = data.Type;

        switch(data.Type)
        {
            case ColorType.Blue :
                ResetAllBuffs();
                jumpCharge = data.JumpCharge;
                
                break;
            
            case ColorType.Orange:
                
                stamina.IncreaseStamina(data.StaminaAmount);
                
                break;
            
            default:
                
                ResetAllBuffs();
                
                break;


        }
    }
    public void SetPassiveBuff(ColorData data)
    {
        currentAbilityType = data.Type;

        switch(data.Type)
        {
            case ColorType.Blue :
                ResetAllBuffs();
                jumpCharge = data.JumpCharge;
                
                break;
            
            case ColorType.Orange:
                jumpCharge = 0;
                canStaminaRefresh = true;
                staminaRefreshMultiplier = data.StaminaAmount;
                break;
            
            default:
                
                ResetAllBuffs();
                
                break;
        }

    }
    public void ResetAllBuffs()
    {
        ResetJumpBuffs();
        ResetRunBuffs();
    }

    public void TurnOffOrangePassive()
    {
        canStaminaRefresh = false;
        staminaRefreshMultiplier = 0;
    }

    public void ResetJumpBuffs()
    {
        jumpCharge = 0;
    }
    public void ResetRunBuffs()
    {
        stamina.DecreaseStamina(PlayerStamina.MAX_STAMINA);
        player.Movement.SetMaxSpeed(player.Data.MaxHorizontalSpeed);
        canStaminaRefresh = false;
        staminaRefreshMultiplier = 0;
    }

    #endregion

}
