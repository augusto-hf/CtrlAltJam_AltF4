using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Game Data/Player", fileName ="PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Horizontal Moviment")]
    [SerializeField] private float _horizontalAcceleration;
    [SerializeField] private float _horizontalDeceleration;
    [SerializeField] private float _maxHorizontalSpeed;

    [Space(2)]
    [Header("Run")]
    [SerializeField] private float _staminaDropMultiplier;
    [SerializeField] private float _maxRunSpeed;



    [Space(2)]
    [Header("Jump")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _jumpCutMultiplier;
    [SerializeField] private float _coyoteTime;

    [Space(2)]
    [Header("Fall")]
    [SerializeField] private float _fallMultiplier;
    [SerializeField] private float _maxFallSpeed;
    [SerializeField] private float _gravityScale;


    public float HorizontalAcceleration { get => _horizontalAcceleration; }
    public float HorizontalDeceleration { get => _horizontalDeceleration;}
    public float MaxHorizontalSpeed { get => _maxHorizontalSpeed; }

    public float JumpForce { get => _jumpForce; }
    public float CoyoteTime { get => _coyoteTime; }
    public float JumpCutMultiplier { get => _jumpCutMultiplier; }
    
    public float FallMultiplier { get => _fallMultiplier; }
    public float MaxFallSpeed { get => _maxFallSpeed; }
    public float GravityScale { get => _gravityScale; }

    public float MaxRunSpeed { get => _maxRunSpeed; }
    public float StaminaDropMultiplier { get => _staminaDropMultiplier; }

}
