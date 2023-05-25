using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Obsolete]
public class ObjectParticlesManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem landingParticle, dragRightParticle, dragLeftParticle;
    [Header("Point Variables")]
    [SerializeField] private Transform downPoint;
    [SerializeField] private float downPointOffset;
    private Vector2 downPointLeft, downPointRight;
    [Header ("Ground Detector Variables")]
    [SerializeField] private LayerMask solid;

    [SerializeField] private Vector2 size;
    private bool AlreadyTouchedGround = false;
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
        if (LeftOnGround() || RightOnGround())
        {
            if (!AlreadyTouchedGround)
            {
                AlreadyTouchedGround = true;
                playOneTimeParticle(landingParticle, downPoint.position);
            }         
        }
        else if (!LeftOnGround() && !RightOnGround())
        {
            AlreadyTouchedGround = false;
        }
    }

    void particleOnDrag()
    {
        if (Mathf.Abs(rb.velocity.x) > 0.5f)
        {
            if (LeftOnGround())
            {
                playConstantParticle(dragLeftParticle);
            }
            else
                stopConstantParticle(dragLeftParticle);
            if (RightOnGround())
            {
                playConstantParticle(dragRightParticle);
            } 
            else
                stopConstantParticle(dragRightParticle);
        }
        else
        {
            stopConstantParticle(dragLeftParticle);
            stopConstantParticle(dragRightParticle);
        }
    }
    private bool LeftOnGround()
    {
        downPointLeft = new Vector2(downPoint.position.x - downPointOffset, downPoint.position.y);
        var groundCheck = Physics2D.OverlapBox(downPointLeft, size, 0, solid);
        return groundCheck;
    }
    private bool RightOnGround()
    {
        downPointRight = new Vector2(downPoint.position.x + downPointOffset, downPoint.position.y);
        var groundCheck = Physics2D.OverlapBox(downPointRight, size, 0, solid);
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

        Gizmos.color = LeftOnGround() ? Color.green : Color.red;
        Gizmos.DrawWireCube(downPointLeft, new Vector3(size.x, size.y, 0));

        Gizmos.color = RightOnGround() ? Color.green : Color.red;
        Gizmos.DrawWireCube(downPointRight, new Vector3(size.x, size.y, 0));

    }
#endif
}
