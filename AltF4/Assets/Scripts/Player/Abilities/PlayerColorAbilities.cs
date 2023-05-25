using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorAbilities : MonoBehaviour
{

    [SerializeField] private float staminaDropMuiltiplier;
    [SerializeField] private float staminaTest;
    
    private PlayerCore player;
    private ColorType currentAbilityType;
    private BlobManager lastBlob;
    private PlayerStamina stamina;

    private int jumpCharge;
    private bool isJumping;

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
    }

    #region Run

    private void Run()
    {

        if (player.Controller.ColorButtonHold && Mathf.Abs(player.Controller.Axis.x) > 0 && stamina.CurrentStamina > PlayerStamina.MIN_STAMINA)
        {
            stamina.DecreaseStamina(staminaDropMuiltiplier);

            player.Movement.SetMaxSpeed(player.Data.MaxRunSpeed);
            
            if (stamina.CurrentStamina <= PlayerStamina.MIN_STAMINA)
            {
                player.ColorManager.ConsumeColor();
            }
        }
        else
        {
            player.Movement.SetMaxSpeed(player.Data.MaxHorizontalSpeed);
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
        else if (player.Check.IsFalling)
        {
            isJumping = false;
        }

        if (player.Check.IsGrounded && !player.Check.IsFalling && jumpCharge <= 0 && !isJumping)
        {
            player.ColorManager.ConsumeColor();
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
        return player.Check.LastTimeGrounded > 0 && !isJumping && jumpCharge > 0;
    }

    #endregion

    #region Control Buffs
    public void SetConsumeBuffs(ColorData data)
    {
        currentAbilityType = data.Type;

        switch(data.Type)
        {
            case ColorType.Blue :
                ResetBuffs();
                jumpCharge = data.JumpCharge;
                
                break;
            
            case ColorType.Orange:
                
                stamina.IncreaseStamina(data.StaminaAmount);
                
                break;
            
            default:
                
                ResetBuffs();
                
                break;


        }
    }
    public void SetPassiveBuff(ColorData data)
    {
        currentAbilityType = data.Type;

        switch(data.Type)
        {
            case ColorType.Blue :
                ResetBuffs();
                jumpCharge = data.JumpCharge;
                
                break;
            
            case ColorType.Orange:
                jumpCharge = 0;
                stamina.IncreaseStamina(data.StaminaAmount);
                
                break;
            
            default:
                
                ResetBuffs();
                
                break;
        }

    }
    public void ResetBuffs()
    {
        stamina.DecreaseStamina(PlayerStamina.MAX_STAMINA);
        player.Movement.SetMaxSpeed(player.Data.MaxHorizontalSpeed);
        jumpCharge = 0;
    }

    #endregion

}
