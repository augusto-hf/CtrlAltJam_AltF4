using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stalactite : MonoBehaviour
{
    [SerializeField] private float newGravity;
    [SerializeField] private ButterflyManager butterFly;
    [SerializeField] private AudioSource audio;
    [SerializeField] private ParticleSystem particles;
    private Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(butterFly.used)
            {
                rigid.gravityScale = newGravity;
                Debug.Log("aaaa");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            particles.Play();
            audio.Play();
            rigid.bodyType  = RigidbodyType2D.Static;
        }
    }

}
