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
    public PlayerControl Controller { get; private set; }
    public PlayerColorAction Color { get; private set; }
    public PlayerHook Hook { get; private set; }
    public PlayerAnimation Animation { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public event Action<string> onPickColor;

    private void Awake()
    {
        Check = GetComponent<PlayerChecks>();
        Movement = GetComponent<PlayerMovement>();
        Color = GetComponent<PlayerColorAction>();
        Hook = GetComponent<PlayerHook>();
        Animation = GetComponent<PlayerAnimation>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Controller = GameObject.FindWithTag("InputManager").GetComponent<PlayerControl>();
    }

    private void Update()
    {
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
