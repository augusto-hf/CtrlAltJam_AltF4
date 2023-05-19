using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindProximityCheck : MonoBehaviour
{
    private ParticleSystem windParticles;
    void Awake()
    {
        windParticles = GetComponent<ParticleSystem>();
        windParticles.enableEmission = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            windParticles.enableEmission = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            windParticles.enableEmission = false;
        }
    }
}
