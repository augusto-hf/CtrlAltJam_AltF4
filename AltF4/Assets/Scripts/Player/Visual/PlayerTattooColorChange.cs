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
    private bool isInstantColor;
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
            if (isInstantColor)
                StartCoroutine(changeInstantColor());
            else
                changeRelativeColor();
        }
        
    }
    private void identifyColorChange()
    {
        switch (nextColorPower)
        {
            case ColorType.Blue:
                lastColor = currentColor;
                nextColor = blue;
                isInstantColor = true;
                break;

            case ColorType.Orange:
                lastColor = currentColor;
                nextColor = orange;
                isInstantColor = false;
                break;

            case ColorType.NoColor:
                lastColor = currentColor;
                nextColor = noColor;
                isInstantColor = true;
                break;
        }
    }
    private IEnumerator changeInstantColor()
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
    private void changeRelativeColor()
    {
        Debug.Log(currentColor);
        playerSprite.material.color = currentColor = Color.Lerp(lastColor, nextColor, (player.Abilities.StaminaAmount / PlayerStamina.MAX_STAMINA) * Time.deltaTime);
    }
}
