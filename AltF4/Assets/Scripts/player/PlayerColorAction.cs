using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorAction : MonoBehaviour
{
    private PlayerControl input;
    private IColor currentColor;

    void Update()
    {
        if (input.ColorButton)
        {
            currentColor?.Action();
        }
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        
    }
    
}
