using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    private PlayerCore player;
    private Animator animator;
    [SerializeField] private ParticleSystem jumpBlueParticles, landingParticle, walkingParticle, runningParticle;
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

        particleOnWalkAndRun();

    }
    #region Movement Particles
    void particleOnWalkAndRun()
    {
        if (player.Check.OnGround() && Mathf.Abs(player.rb.velocity.x) > 0.5f && Mathf.Abs(player.Controller.Axis.x) > 0)
        {
            if (player.ColorManger.CurrentColor.ColorData.Type != ColorType.Orange) 
            {
                playConstantParticle(runningParticle);
            }
            else
            {
                if (player.Controller.ColorButtonHold)
                {
                    stopConstantParticle(walkingParticle);
                    playConstantParticle(runningParticle);
                }
                else
                {
                    stopConstantParticle(runningParticle);
                }
            }
        }           
        else
        {
            stopConstantParticle(walkingParticle);
            stopConstantParticle(runningParticle);
        }
           
    }
    #endregion

    #region Jumping&Landing Particles
    void particleOnPowerJump()
    {
        if (player.ColorManager.CurrentColor.ColorData.Type == ColorType.Blue && player.Check.OnGround() && player.Controller.ColorButtonDown)
        {
            playOneTimeParticle(jumpBlueParticles, downFeetPoint.position);
        }
    }
    void particleOnLanding()
    {
        if (player.Check.OnGround() && !alreadyTouchedGround)
        {
            alreadyTouchedGround = true;
            playOneTimeParticle(landingParticle, downFeetPoint.position);
        }
        if (!player.Check.OnGround())
        {
            alreadyTouchedGround = false;
        }
    }

    #endregion
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
