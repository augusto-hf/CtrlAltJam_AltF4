using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialInputFeedback : MonoBehaviour
{
    [SerializeField] private SpriteRenderer keySpriteRenderer;
    [SerializeField] private Sprite keyImage_Released, keyImage_Pressed, keyImageAlt_Released, keyImageAlt_Pressed;
    [SerializeField] private PlayerControl control;
    [SerializeField] private Color idleColor, pressedColor;
    [SerializeField] private string command;
    private bool playerIsNear = false, inputToVerify;

    void Start()
    {     
        verifyNullOnStart();

        switch (command)
        {
            case "tongue":
                inputToVerify = control.TongueButtonHold;
                break;
            case "color":
                inputToVerify = control.ColorButtonHold;
                break;
            case "left":
                inputToVerify = control.LeftButtonHold;
                break;
            case "right":
                inputToVerify = control.RightButtonHold;
                break;
            default:
                inputToVerify = control.TongueButtonHold;
                break;
        }

    }

    void Update()
    {
        

        pressVisualButton(inputToVerify);
    }

    void pressVisualButton(bool input)
    {
        if (input)
            keySpriteRenderer.color = pressedColor;

        else if (!input)
            keySpriteRenderer.color = idleColor;
    }

    private void verifyNullOnStart()
    {
        if (keySpriteRenderer == null)
            keySpriteRenderer = GetComponent<SpriteRenderer>();
        if (keyImageAlt_Released == null || keyImageAlt_Released == null)
        {

        }
    }

    #region trigger
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsNear = true;
            keySpriteRenderer.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerIsNear = false;
            keySpriteRenderer.enabled = false;
        }
    }
    #endregion
}
