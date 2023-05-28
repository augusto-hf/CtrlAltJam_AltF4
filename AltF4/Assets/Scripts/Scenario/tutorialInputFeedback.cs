using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialInputFeedback : MonoBehaviour
{
    [SerializeField] private SpriteRenderer keySpriteRenderer;
    [SerializeField] private PlayerCore player;
    [SerializeField] private Color pressedColor;
    [SerializeField] private string inputName, alternativeInputName;
    
    private bool playerIsNear = false;
    void Start()
    {
        verifyNullOnStart();
    }

    void Update()
    {
        if(playerIsNear)
            pressVisualButton(Input.GetKey(inputName), Input.GetKey(alternativeInputName));
    }

    void pressVisualButton(bool input, bool alternativeinput)
    {
        if (input || alternativeinput)
            keySpriteRenderer.color = pressedColor;

        else if (!input && !alternativeinput)
            keySpriteRenderer.color = Color.white;
    }

    private void verifyNullOnStart()
    {
        if (keySpriteRenderer == null)
            keySpriteRenderer = GetComponent<SpriteRenderer>();

        if (inputName == "")
            inputName = "0";

        if (alternativeInputName == "")
            alternativeInputName = "0";

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
