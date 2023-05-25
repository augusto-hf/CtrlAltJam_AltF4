using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Obsolete]
public class ObjectParticlesManager : MonoBehaviour
{
    [Header("Particle Variables")]
    [SerializeField] private ParticleSystem landingParticle, dragRightParticle, dragLeftParticle;
    [SerializeField] private Transform downPoint, downRightPoint, downLeftPoint;
    [Header ("Ground Detector Variables")]
    [SerializeField] private LayerMask solid;
    [SerializeField] private LayerMask ground;
    [SerializeField] private Transform groundDetectorPoint;
    [SerializeField] private Vector2 size;
    private bool alreadyTouchedGround = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        particleOnLanding();
        particleOnDrag();
    }
    void particleOnLanding()
    {
        if (OnGround() && !alreadyTouchedGround)
        {
            alreadyTouchedGround = true;
            playOneTimeParticle(landingParticle, downPoint.position);
        }
        if (!OnGround())
        {
            alreadyTouchedGround = false;
        }
    }

    void particleOnDrag()
    {
        if (OnGround() && rb.velocity.x > 0.5f)
        {
            stopConstantParticle(dragLeftParticle);
            playConstantParticle(dragRightParticle);
        }
        else if (OnGround() && rb.velocity.x < -0.5f)
        {
            stopConstantParticle(dragRightParticle);
            playConstantParticle(dragLeftParticle);
        }
        else
        {
            stopConstantParticle(dragLeftParticle);
            stopConstantParticle(dragRightParticle);
        }
    }
    private bool OnGround()
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
