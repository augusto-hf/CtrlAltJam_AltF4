using UnityEngine;

public enum ColorType 
{
    NoColor,
    Blue,
    Orange
}

public interface IColor
{
    public ColorType Type { get; }
    public void Action(PlayerCore player);

    public void ResetAction(PlayerCore player);

}