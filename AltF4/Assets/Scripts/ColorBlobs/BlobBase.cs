using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobBase : MonoBehaviour, IColor
{
    [SerializeField] private ColorData colorData;
    public ColorData ColorData { get => colorData; }
    
}
