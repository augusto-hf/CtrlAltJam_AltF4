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
    [SerializeField] private float transitionTime;
    void Start()
    {
        lastColorPower = noColorReference.gameObject.GetComponent<IColor>().ColorData.Type;
        lastColor = noColor;
        currentColor = noColor;
    }

    void Update()
    {
        nextColorPower = player.ColorManager.CurrentColor.ColorData.Type;

        identifyColorChange();

        if (currentColor != nextColor)
        {
            //playerSprite.material.color = currentColor = nextColor;
            StartCoroutine(changeColor());           
        }
        
    }
    private void identifyColorChange()
    {
        if (nextColorPower == ColorType.Blue)
        {
            lastColor = currentColor;
            nextColor = blue;
        }
        else if (nextColorPower == ColorType.Orange)
        {
            lastColor = currentColor;
            nextColor = orange;
        }
        else if (nextColorPower == ColorType.NoColor)
        {
            lastColor = currentColor;
            nextColor = noColor;
        }
    }
    private IEnumerator changeColor()
    {
        float percentage = 0;
        while (currentColor != nextColor)
        {
            playerSprite.material.color = currentColor = Color.Lerp(lastColor, nextColor, percentage);
            percentage += Time.deltaTime / transitionTime;
            yield return null;
        }
        lastColor = currentColor;
    }
}
