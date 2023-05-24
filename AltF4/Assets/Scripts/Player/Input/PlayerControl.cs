using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    float LastHorizontalAxis, LastVerticalAxis;
    public Vector2 Axis { get => GetAxis(); }
    public Vector2 LastAxis { get => GetLastAxis(); } 
    public bool ColorButtonHold { get; private set; }
    public bool ColorButtonDown { get; private set; }
    public bool ColorButtonUp{ get; private set; }
    public bool TongueButton { get; private set; }

    // Update is called once per frame
    void Update()
    {
        ColorButtonHold = Input.GetButton("ColorActionButton");
        ColorButtonDown = Input.GetButtonDown("ColorActionButton");
        ColorButtonUp = Input.GetButtonUp("ColorActionButton");
        
        TongueButton = Input.GetButton("Tongue");

        if (Input.GetAxis("Horizontal") != 0)
            LastHorizontalAxis = Input.GetAxisRaw("Horizontal");

        if (Input.GetAxis("Vertical") != 0)
            LastVerticalAxis = Input.GetAxisRaw("Vertical");

    }

    private Vector2 GetAxis()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private Vector2 GetLastAxis()
    {
        return new Vector2(LastHorizontalAxis, LastVerticalAxis).normalized;
    }
}
