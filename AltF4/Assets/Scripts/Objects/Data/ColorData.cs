using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Color Data", menuName ="Game Data/Color")]
public class ColorData : ScriptableObject 
{
    [SerializeField] private ColorType _colorType;    
    [SerializeField] private int _staminaAmount;
    [SerializeField] private int _jumpCharge;

    public int StaminaAmount { get => Mathf.Clamp(_staminaAmount, 0, PlayerStamina.MAX_STAMINA); }
    public int JumpCharge { get => _jumpCharge; }
    public ColorType Type { get => _colorType; }
}
