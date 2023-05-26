using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    [SerializeField] private Animator animaFade;

    public void CallFade(bool inOrOut)
    {
        if(inOrOut)
        {
            animaFade.Play("fadeIn");
            return;
        }
        
        animaFade.Play("fadeOut"); 
    }
}
