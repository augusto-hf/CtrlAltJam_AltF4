using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Vector2 Axis { get => GetAxis(); }
    public bool ColorButton { get; private set; }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(GetAxis());
        ColorButton = Input.GetButton("ColorActionButton");

        if (ColorButton)
        {
            //Debug.Log("Color Button");
            ColorAction();
        }
    }
    private void ColorAction()
    {
        
    }

    private Vector2 GetAxis()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
    }
}
