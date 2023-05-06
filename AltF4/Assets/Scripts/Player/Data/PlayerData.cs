using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Player/Data", fileName ="PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Horizontal Moviment")]
    [SerializeField] private float _horizontalAcceleration;
    [SerializeField] private float _horizontalDeceleration;
    [SerializeField] private float _maxHorizontalSpeed;
    [SerializeField] private float _maxSpeedRun;


    [Space(2)]
    [Header("Jump")]
    [SerializeField] private float _jumpForce;

    [Space(2)]
    [Header("Fall")]
    [SerializeField] private float _fallMultiplier;
    [SerializeField] private float _maxFallSpeed;
    [SerializeField] private float _gravityScale;


    public float HorizontalAcceleration { get => _horizontalAcceleration; }
    public float HorizontalDeceleration { get => _horizontalDeceleration;}
    public float MaxHorizontalSpeed { get => _maxHorizontalSpeed; }
    public float MaxSpeedRun { get => _maxSpeedRun; }
    public float JumpForce { get => _jumpForce; }
    public float FallMultiplier { get => _fallMultiplier; }
    public float MaxFallSpeed { get => _maxFallSpeed; }
    public float GravityScale { get => _gravityScale; }
    

}
