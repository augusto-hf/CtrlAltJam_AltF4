using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SavePoint : MonoBehaviour
{
    [SerializeField] private bool saved = false;

    public event Action<Transform> onSavePoint;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Player"))
        {
            if(!saved)
            {
                onSavePoint?.Invoke(other.transform);
                saved = true;
            }
        }
    }
}
