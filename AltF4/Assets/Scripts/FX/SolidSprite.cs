using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolidSprite : MonoBehaviour
{
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        ColorSprite(Color.white);
    }

    public void ColorSprite(Color SetColor)
    {
        sprite.color = SetColor; 
    }

    public void ActivateSprite()
    {
        this.gameObject.SetActive(true);
    }

    public void DisableSprite()
    {
        this.gameObject.SetActive(false);
    }
}
