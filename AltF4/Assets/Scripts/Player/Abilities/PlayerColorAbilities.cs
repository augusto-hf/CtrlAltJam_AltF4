using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorAbilities : MonoBehaviour
{
    [SerializeField] private float staminaCurrent;
    
    private PlayerCore player;
    private ColorType currentAbilityType;
    private BlobManager lastBlob;
    private PlayerStamina stamina;
    private GhostEffect ghost;

    private int jumpCharge;
    private float jumpBufferTimer;
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
        ghost = GetComponentInChildren<GhostEffect>();
        
    }

    private void FixedUpdate()
    {
        staminaCurrent = stamina.CurrentStamina;   
    }

    private void Update()
    {
        JumpUpdate();
        JumpInputBuffer();


        switch (currentAbilityType)
        {
            case ColorType.Blue:
                JumpPerform();
                break;

            case ColorType.Orange:
                Run();
                break;

            case ColorType.NoColor:
                break;

            default:
                break;
        }

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

            ghost.ShowGhostEffect(currentAbilityType);

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
    private void JumpPerform()
    {

        if (jumpBufferTimer > 0 && CanJump())
        {
            jumpCharge--;
            jumpBufferTimer = 0;
            isJumping = true;
            JumpForceApply();
            player.ColorManager.ConsumeColor();
        }
        
        
    }

    public void JumpForceApply()
    {
        player.rb.velocity = new Vector2(player.rb.velocity.x, 0);
        player.rb.AddForce(Vector2.up * player.Data.JumpForce, ForceMode2D.Impulse);
    }

    public void JumpCutForceApply()
    {
        player.rb.AddForce(Vector2.down * player.rb.velocity.y * player.Data.JumpCutMultiplier, ForceMode2D.Impulse);
    }
    
    private bool CanJump()
    {
        return !isJumping && jumpCharge > 0;
    }

    private void JumpUpdate()
    {

        if (isJumping)
        {
            ghost.ShowGhostEffect(currentAbilityType);
        }

        if (player.Controller.ColorButtonUp && player.Movement.Velocity.y > 0 && isJumping)
        {
            JumpCutForceApply();            
        }
        else if (player.Check.IsFalling && isJumping)
        {   
            isJumping = false;
        }
        
    }

    private void JumpInputBuffer()
    {
        if (player.Controller.ColorButtonDown)
        {
            jumpBufferTimer = player.Data.JumpBufferTime;
        }

        if (jumpBufferTimer > 0)
        {
            jumpBufferTimer -= Time.deltaTime;
            if (jumpBufferTimer <= 0)
            {
                jumpBufferTimer = 0;
            }
        }
    }

    #endregion

    #region Control Buffs
    public void SetConsumeBuffs(ColorData data)
    {
        currentAbilityType = data.Type;
        ghost.SwitchColor(currentAbilityType);

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
        ghost.SwitchColor(currentAbilityType);

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
