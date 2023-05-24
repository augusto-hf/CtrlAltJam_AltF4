using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangeAction : MonoBehaviour, IColor
{
    [SerializeField] private ColorData _colorData;
    public ColorData ColorData { get => _colorData; }
}
