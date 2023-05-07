using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Vector2 Axis { get => GetAxis(); }
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
}
