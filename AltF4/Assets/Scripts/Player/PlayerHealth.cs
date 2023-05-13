using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public event Action OnPlayerDeath;
    private CapsuleCollider2D capsule;
    private SpriteRenderer sprite;
    private bool isDead;

    public bool IsDead { get => isDead; }

    private void Awake()
    {
        capsule = GetComponent<CapsuleCollider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
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
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, Mathf.MoveTowards(sprite.color.a, 0, 1/Time.deltaTime));
            yield return null;
        }

        OnPlayerDeath?.Invoke();
    }

    public void Revive(Vector2 checkPointPosition)
    {
        this.transform.position = checkPointPosition;
        isDead = false;
        capsule.isTrigger = false;
    }
}
