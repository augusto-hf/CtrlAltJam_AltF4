using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Color Data", menuName ="Game Data/Color")]
public class ColorData : ScriptableObject 
{
    [SerializeField] private ColorType _colorType;    
    [SerializeField] private float _staminaAmount;
    [SerializeField] private int _jumpCharge;

    public float StaminaAmount { get => Mathf.Clamp(_staminaAmount, PlayerStamina.MIN_STAMINA, PlayerStamina.MAX_STAMINA); }
    public int JumpCharge { get => Mathf.Clamp(_jumpCharge, 0, 1); }
    public ColorType Type { get => _colorType; }
}
