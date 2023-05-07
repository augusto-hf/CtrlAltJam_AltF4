using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorAction : MonoBehaviour
{
    [SerializeField] private GameObject StartingColorReference;
    private IColor currentColor;
    private BlobManager lastBlob;
    private PlayerControl inputScript;

    private void Awake()
    {
        currentColor = StartingColorReference.gameObject.GetComponent<IColor>();
    }
    private void Start()
    {
        PlayerMovement moveScript = this.GetComponent<PlayerMovement>();
        inputScript = moveScript.Input;
    }
    void Update()
    {
        currentColor?.Action(this.gameObject, inputScript.ColorButton);
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
