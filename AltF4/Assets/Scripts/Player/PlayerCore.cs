using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerCore : MonoBehaviour
{
    [SerializeField] private PlayerData data;

    public PlayerData Data { get => data; } 
    public PlayerChecks Check { get; private set; }
    public PlayerMovement Movement { get; private set;}
    public PlayerControl Input { get; private set; }

    public event Action<string> onPickColor;

    private void Awake()
    {
        Check = GetComponent<PlayerChecks>();
        Movement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        Input = GameObject.FindGameObjectWithTag("InputManager").GetComponent<PlayerControl>();
    }

    public void StopAndRunPlayer()
    {
        Movement.canMove = !Movement.canMove;
    }

    public void PickColor(string nameColor)
    {
        onPickColor?.Invoke(nameColor);
    }

    
}
