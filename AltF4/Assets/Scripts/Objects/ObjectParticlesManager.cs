using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectParticlesManager : MonoBehaviour
{
    [Header("Particle Variables")]
    [SerializeField] private ParticleSystem landingParticles, dragParticles;
    [SerializeField] private Transform downPoint, downRightPoint, downLeftPoint;
    [Header ("Ground Detector Variables")]
    [SerializeField] private LayerMask solid;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform groundDetectorPoint;
    [SerializeField] private Vector2 size;

    void Update()
    {
        
    }
    void particleOnLanding()
    {

    }
    void particleOnDrag()
    {

    }
    public bool OnGround()
    {
        var groundCheck = Physics2D.OverlapBox(groundDetectorPoint.position, size, 0, solid);
        return groundCheck;
    }
    void playOneTimeParticle(ParticleSystem particle, Vector3 location)
    {
        Instantiate(particle, location, Quaternion.identity);
    }
    void playConstantParticle(ParticleSystem particle)
    {
        particle.enableEmission = true;
    }
    void stopConstantParticle(ParticleSystem particle)
    {
        particle.enableEmission = false;
    }
}
