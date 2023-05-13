using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private CapsuleCollider2D capsule;
    private SpriteRenderer sprite;
    private SaveManager saveManager;
    private bool isDead;

    public bool IsDead { get => isDead; }

    private void Awake()
    {
        capsule = GetComponent<CapsuleCollider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        saveManager =  GameObject.FindWithTag("GameController").GetComponent<SaveManager>();
    }


    public void Death()
    {
        StartCoroutine(DeathCoroutine());
    }

    IEnumerator DeathCoroutine()
    {
        capsule.isTrigger = true;
        isDead = true;
        
        while (sprite.color.a > 0)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, Mathf.MoveTowards(sprite.color.a, 0, Time.deltaTime/2));
            yield return null;
        }
        Revive();
        saveManager.Load();

    }

    public void Revive()
    {
        isDead = false;
        capsule.isTrigger = false;
    }
}
