using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    private PlayerCore player;
    private Animator animator;
    [SerializeField] private ParticleSystem jumpBlueParticles, landingParticle, walkingParticle;
    [SerializeField] private Transform downFeetPoint, behindFeetPoint, tonguePoint;

    private bool alreadyTouchedGround = false;

    private void Start()
    {
        player = GetComponent<PlayerCore>();
    }

    void Update()
    {
        particleOnPowerJump();
        particleOnLanding();

        particleOnWalk();
        particleOnRun();
    }
    #region Movement Particles
    void particleOnWalk()
    {
        if (player.Check.OnGround() && player.rb.velocity.x > 0)
        {
            if (player.Color.CurrentColor.Type == ColorType.Orange && player.Controller.ColorButton)
                return;
            else
                playParticle(jumpBlueParticles, behindFeetPoint.position);
        }
    }
    void particleOnRun()
    {
        if (player.Check.OnGround() && player.rb.velocity.x > 0)
        {
            if (player.Color.CurrentColor.Type == ColorType.Orange && player.Controller.ColorButton)
                playParticle(jumpBlueParticles, behindFeetPoint.position);
        }
    }
    #endregion

    #region Jumping&Landing Particles
    void particleOnPowerJump()
    {
        if (player.Color.CurrentColor.Type == ColorType.Blue && player.Check.OnGround() && player.Controller.ColorButton)
        {
            playParticle(jumpBlueParticles, downFeetPoint.position);
        }
    }
    void particleOnLanding()
    {
        if (player.Check.OnGround() && !alreadyTouchedGround)
        {
            alreadyTouchedGround = true;
            playParticle(landingParticle, downFeetPoint.position);
        }
        if (!player.Check.OnGround())
        {
            alreadyTouchedGround = false;
        }
    }

    #endregion
    public void playParticle(ParticleSystem particle, Vector3 location)
    {
        Instantiate(particle, location, Quaternion.identity);
    }
}
