using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialInputFeedback : MonoBehaviour
{
    [SerializeField] private SpriteRenderer keySpriteRenderer;
    [SerializeField] private PlayerControl control;
    [SerializeField] private Color idleColor, pressedColor;
    [SerializeField] private bool isTongue, isColorPower, isMoveNegative, isMovePositive;
    private bool playerIsNear = false, inputToVerify;
    void Start()
    {
        verifyNullOnStart();
        if (isTongue)
        {
            inputToVerify = control.TongueButtonHold;
        }
        else if (isColorPower)
        {
            inputToVerify = control.ColorButtonHold;
        }
    }

    void Update()
    {
        if(playerIsNear)
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
