using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    float LastHorizontalAxis, LastVerticalAxis;
    public Vector2 Axis { get => GetAxis(); }
    public Vector2 LastAxis { get => GetLastAxis(); }
    public bool ColorButton { get; private set; }
    public bool TongueButton { get; private set; }

    // Update is called once per frame
    void Update()
    {
        ColorButton = Input.GetButton("ColorActionButton");
        TongueButton = Input.GetButton("Tongue");
    }

    private Vector2 GetAxis()
    {
        return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
    }

    private Vector2 GetLastAxis()
    {
        if (Input.GetAxis("Horizontal") != 0)
            LastHorizontalAxis = Input.GetAxis("Horizontal");
        if (Input.GetAxis("Vertical") != 0)
            LastVerticalAxis = Input.GetAxis("Vertical");

        return new Vector2(LastHorizontalAxis, LastVerticalAxis).normalized;
    }
}
