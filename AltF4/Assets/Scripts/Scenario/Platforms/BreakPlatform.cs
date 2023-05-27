using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakPlatform : MonoBehaviour
{
    [SerializeField] private GameObject platformObj;
    [SerializeField] private float timeForFalling = 1f;
    [SerializeField] private float timeForFallingMax = 1f;
    [SerializeField] private float timeActive = 1f;

    private Transform platformTransform;
    private Rigidbody2D  rigid;
    private PlatformDetect detect;

    private void Awake() 
    {
        platformTransform = platformObj.GetComponent<Transform>();
        rigid = platformObj.GetComponent<Rigidbody2D>();
        detect = platformObj.GetComponent<PlatformDetect>();
    }

    void FixedUpdate()
    {
        if(detect.playerTouch)
        {
            timeForFalling -= Time.deltaTime;  
        }

        if(timeForFalling <= 0)
        {
            StartCoroutine(Falling());
        }
    }

    IEnumerator Falling()
    {
        rigid.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(timeActive);

        platformTransform.rotation = this.transform.rotation;
        platformTransform.position = this.transform.position;
        rigid.bodyType = RigidbodyType2D.Kinematic;
        detect.playerTouch = false;
        timeForFalling = timeForFallingMax;
        platformObj.SetActive(true);
    }
}
