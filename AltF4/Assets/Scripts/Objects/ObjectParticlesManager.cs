using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Obsolete]
public class ObjectParticlesManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem landingParticle, dragRightParticle, dragLeftParticle;
    [Header("Point Variables")]
    [SerializeField] private Transform downPoint, downRightPoint, downLeftPoint;
    [Header ("Ground Detector Variables")]
    [SerializeField] private LayerMask solid;

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
        if (OnGround() && Mathf.Abs(rb.velocity.x) > 0.5f)
        {
            playConstantParticle(dragLeftParticle);
            playConstantParticle(dragRightParticle);
        }
        else
        {
            stopConstantParticle(dragLeftParticle);
            stopConstantParticle(dragRightParticle);
        }
    }
    private bool OnGround()
    {
        var groundCheck = Physics2D.OverlapBox(downPoint.position, size, 0, solid);
        return groundCheck;
    }
    #region Particles
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
    #endregion
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (downPoint == null) return;

        Gizmos.color = OnGround() ? Color.green : Color.red;
        Gizmos.DrawWireCube(downPoint.position, new Vector3(size.x, size.y, 0));

    }
#endif
}
