using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [SerializeField] private Animator anima;

    [SerializeField] private bool active = true;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            if(active)
            {
                anima.Play("fadeIn");
                active = false;
            }
        }
    }
}
