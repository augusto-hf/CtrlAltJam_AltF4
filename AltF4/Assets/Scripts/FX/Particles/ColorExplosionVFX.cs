using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorExplosionVFX : MonoBehaviour
{
    [ColorUsageAttribute(true, true)]
    public Color color;

    private ParticleSystem particles;

    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
    }

    private void Start()
    {
        particles.Play();
        Destroy(this.gameObject, 1);
    }

}
