using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private PlayerCore core;
    private CapsuleCollider2D capsule;
    private SpriteRenderer sprite;
    private GameManager saveManager;
    private bool isDead;
    private float alpha;
    private Color defaulColor;
    private Color invisibleColor;

    public bool IsDead { get => isDead; }

    private void Awake()
    {
        capsule = GetComponent<CapsuleCollider2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        defaulColor = sprite.color;
        invisibleColor = new Color (defaulColor.r, defaulColor.g, defaulColor.b, 0);
    }

    private void Start()
    {
        saveManager =  GameObject.FindWithTag("GameController").GetComponent<GameManager>();
    }


    public void Death()
    {
        StartCoroutine(DeathCoroutine());
    }

    IEnumerator DeathCoroutine()
    {
        capsule.isTrigger = true;
        isDead = true;

        while(sprite.color.a > 0)
        {
            alpha -= 0.1f * Time.deltaTime;
            sprite.color = Color.Lerp(defaulColor, invisibleColor, alpha);
            yield return null;
        }
        
        yield return new WaitForSeconds(1);
        
        saveManager.LoadGame();

    }

    public void Revive()
    {
        isDead = false;
        capsule.isTrigger = false;
        alpha = 1;
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, alpha);
    }
}
