using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorParticlesVFX : MonoBehaviour
{
    [Header("Blue Atributes")]
    [ColorUsageAttribute(true, true)]
    [SerializeField] private Color blueColor;
    [SerializeField] private Material blueMaterial;
    [SerializeField] private Material blueTrailMaterial;
    [SerializeField] private GameObject blueExplosionEffect;

    [Space(2)]
    [Header("Orange Atributes")]   
    [ColorUsageAttribute(true, true)]
    [SerializeField] private Color orangeColor;
    [SerializeField] private Material orangeMaterial;
    [SerializeField] private Material orangeTrailMaterial;
    [SerializeField] private GameObject orangeExplosionEffect;

    [Space(2)]
    [Header("Particles")]
    [SerializeField] private ParticleSystem absorveParticlesFront;
    [SerializeField] private ParticleSystem absorveParticlesBack;
    private GameObject explosionEffect;

    private void Awake() {

        AdjustColor(ColorType.Blue);

        absorveParticlesBack.Stop();
        absorveParticlesFront.Stop();
        
    }

    public void PlayAbsorbColor(ColorType type)
    {
        AdjustColor(type);
        absorveParticlesBack.Play();
        absorveParticlesFront.Play();
        
    }

    public void StopAbsorbColor()
    {
        absorveParticlesBack.Stop();
        absorveParticlesFront.Stop();
    }

    public void ResetAbsorbColor()
    {
        absorveParticlesBack.Clear();
        absorveParticlesFront.Clear();
    }

    public void PlayExplosionColor()
    {
        Instantiate(explosionEffect, this.transform.position, Quaternion.identity);
    }

    public void AdjustColor(ColorType type) 
    {

        switch(type)
        {
            case ColorType.Blue:

                explosionEffect = blueExplosionEffect;

                absorveParticlesBack.startColor = blueColor;
                absorveParticlesBack.GetComponent<ParticleSystemRenderer>().material = blueMaterial;
                absorveParticlesBack.GetComponent<ParticleSystemRenderer>().trailMaterial = blueTrailMaterial;
                
                absorveParticlesFront.startColor = blueColor;
                absorveParticlesFront.GetComponent<ParticleSystemRenderer>().material = blueMaterial;
                absorveParticlesFront.GetComponent<ParticleSystemRenderer>().trailMaterial = blueTrailMaterial;

            break;
        }

    }

}
