using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobManager : MonoBehaviour
{
    public string nameColor;
    
    public bool beingUsed = false;
    [SerializeField] private GameObject VisualAndHitbox;
    public IColor blobColor;

    private void Awake()
    {
        blobColor = GetComponent<IColor>();
    }

    public void PickPower()
    {
        beingUsed = true;
        VisualAndHitbox.SetActive(false);
        StopAllCoroutines();
    }

    public void RespawnPower()
    {
        beingUsed = false;
        StartCoroutine(TimeToRespawn());
    }

    IEnumerator TimeToRespawn()
    {
        yield return new WaitForSeconds(0.4f);
        VisualAndHitbox.SetActive(true);
    }

}
