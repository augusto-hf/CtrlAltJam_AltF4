using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectInteractColor
{
    Rigidbody2D Rb { get; }
    Vector2 LastVelocityDirection { get; }
    bool CanInteract { get; }
    bool playerOnTop { get; set; }

    
}
