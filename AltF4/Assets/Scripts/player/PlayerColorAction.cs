using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerColorAction : MonoBehaviour
{
    private PlayerCore player;
    [SerializeField] private GameObject StartingColorReference;
    private IColor currentColor;
    private BlobManager lastBlob;

    public IColor CurrentColor { get => currentColor; }

    private void Awake()
    {
        currentColor = StartingColorReference.gameObject.GetComponent<IColor>();
    }
    private void Start()
    {
        player = GetComponent<PlayerCore>();
    }

    void Update()
    {
        currentColor?.Action(player);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("ColorPower"))
        {
            if(lastBlob != null)
                lastBlob.RespawnPower();

            lastBlob = other.gameObject.GetComponentInParent<BlobManager>();
            lastBlob.PickPower();
            currentColor.ResetAction(player);
            currentColor = lastBlob.blobColor;
            player.PickColor(lastBlob.nameColor);
            //PRECISO DE UM NOME/ID PARA AS CORES - feito já XD
        }
    }
    
}
