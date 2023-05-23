using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    private PlayerCore player;
    private Animator animator;
    [SerializeField] private ParticleSystem jumpDust, fallDust, blueJumpDust;
    [SerializeField] private Transform downFeetPoint, behindFeetPoint, tongueFeetPoint;

    void Update()
    {

    }
    #region Jump&Fall Particles
    void particleOnJump()
    {

    }
    void particleOnFall()
    {
        if (player.Check.IsFalling && player.Check.OnGround())
        {
            playParticle(fallDust, downFeetPoint.position);
        }
    }

    #endregion
    public void playParticle(ParticleSystem particle, Vector3 location)
    {
        Instantiate(particle, location, Quaternion.identity);
    }
}
