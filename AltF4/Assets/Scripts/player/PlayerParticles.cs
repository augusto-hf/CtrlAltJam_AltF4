using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParticles : MonoBehaviour
{
    [SerializeField] private ParticleSystem dust;
    [SerializeField] private Transform pointDust;

    public void playDust()
    {
        Instantiate(dust, pointDust.position, Quaternion.identity);
    }
}
