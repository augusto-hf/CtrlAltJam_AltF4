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
        if (keySpriteRenderer == null)
            keySpriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(!playerIsNear)
            pressVisualButton(Input.GetKey(inputName), Input.GetKey(alternativeInputName));
    }

    void pressVisualButton(bool input, bool alternativeinput)
    {
        if(input || alternativeinput)
            keySpriteRenderer.color = pressedColor;

        else if (!input && !alternativeinput)
            keySpriteRenderer.color = Color.white;
    }

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
}
