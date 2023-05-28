using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTattooColorChange : MonoBehaviour
{
    private ColorType lastColorPower, nextColorPower;
    private Color lastColor, currentColor, nextColor;

    [SerializeField] private PlayerCore player;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private GameObject noColorReference;
    [SerializeField] private Color blue, orange, noColor;
    [SerializeField] private float changeSpeed, timeElapsed, lerpDuration;
    void Start()
    {
        lastColorPower = noColorReference.gameObject.GetComponent<IColor>().ColorData.Type;
        lastColor = noColor;
        currentColor = noColor;

    }

    void Update()
    {
        if (nextColorPower == ColorType.Blue)
        {
            nextColor = blue;
        }
        else if (nextColorPower == ColorType.Orange)
        {
            nextColor = orange;
        }
        else if (nextColorPower == ColorType.NoColor)
        {
            nextColor = noColor;
        }


        if (currentColor != nextColor)
        {

                
                playerSprite.material.color = currentColor = Color.Lerp(lastColor, nextColor, 1);
            
        }
        else
        {
            timeElapsed = 0;
            lastColor = currentColor;
        }
    }
}
