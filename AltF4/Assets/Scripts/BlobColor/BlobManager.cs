using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobManager : MonoBehaviour
{
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
    }
    public void RespawnPower()
    {
        beingUsed = false;
        VisualAndHitbox.SetActive(true);
    }
}
