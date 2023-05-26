using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterflyManager : MonoBehaviour
{
    public string nameColor;

    public bool beingUsed = false;
    [SerializeField] private GameObject Visual;
    [SerializeField] private CutsceneManager Cutscene;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        Visual.active = false;
    }
}
