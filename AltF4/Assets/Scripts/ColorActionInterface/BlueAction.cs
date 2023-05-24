using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueAction : MonoBehaviour, IColor
{
    [SerializeField] private ColorData _colorData;
    public ColorData ColorData { get => _colorData; }

}
