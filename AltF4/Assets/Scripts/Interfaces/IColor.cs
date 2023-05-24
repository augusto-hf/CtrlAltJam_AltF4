using UnityEngine;

public enum ColorType 
{
    NoColor,
    Blue,
    Orange
}

public interface IColor
{
    public ColorData ColorData { get; }

}