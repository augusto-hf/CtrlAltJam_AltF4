using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private Animator anima;

    [SerializeField] private bool active = true;
    [SerializeField] private SetTextLocalized text;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(text.NewTextLocalized());
            
            if(active)
            {
                anima.Play("fadeIn");
                active = false;
            }
        }
    }
}
