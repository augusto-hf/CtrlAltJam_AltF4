using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDetect : MonoBehaviour
{
    [SerializeField] private LayerMask ground;
    public bool playerTouch = false;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerTouch = true;
        }

        if (ground == (ground | (1 << other.gameObject.layer)))
        {
            this.gameObject.SetActive(false);
        }
    }
}
