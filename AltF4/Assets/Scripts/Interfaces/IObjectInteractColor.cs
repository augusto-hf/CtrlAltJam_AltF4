using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectInteractColor
{
    Rigidbody2D Rb { get; }

    bool CanInteract { get; }
    bool CanMove { get; set; }
    Vector2 Direction { get; set;}

    
}
