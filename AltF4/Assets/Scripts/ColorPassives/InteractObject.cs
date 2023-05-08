using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class InteractObject : MonoBehaviour, IObjectInteractColor
{
    public Rigidbody2D Rb { get; private set; }
    public bool CanInteract { get; private set; }

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
    }
}
