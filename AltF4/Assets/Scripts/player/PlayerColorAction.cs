using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorAction : MonoBehaviour
{
    [SerializeField] private PlayerControl input;
    [SerializeField] private IColor currentColor;

    void Update()
    {
        if (input.ColorButton)
        {
            currentColor?.Action(this.gameObject);
        }
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        
    }
    
}
