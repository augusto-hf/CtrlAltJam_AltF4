using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorAction : MonoBehaviour
{
    [SerializeField] private PlayerControl input;
    [SerializeField] private GameObject StartingColorReference;
    private IColor currentColor;
    private BlobManager lastBlob;

    private void Awake()
    {
        currentColor = StartingColorReference.gameObject.GetComponent<IColor>();
    }
    void Update()
    {
        currentColor?.Action(this.gameObject, input.ColorButton);
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("ColorPower"))
        {
            if(lastBlob != null)
                lastBlob.RespawnPower();

            lastBlob = other.gameObject.GetComponentInParent<BlobManager>();
            lastBlob.PickPower();
            currentColor.ResetAction(this.gameObject);
            currentColor = lastBlob.blobColor;
        }
    }
    
}
