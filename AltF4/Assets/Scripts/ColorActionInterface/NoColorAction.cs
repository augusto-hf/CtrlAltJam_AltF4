using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoColorAction : MonoBehaviour, IColor
{
    public ColorType Type { get => ColorType.NoColor; }
    public void Action(PlayerCore player)
    {
        
    }
    public void ResetAction(PlayerCore player)
    {
        
    }
}
