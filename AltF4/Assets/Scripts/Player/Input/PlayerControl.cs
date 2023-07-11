using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    float LastHorizontalAxis, LastVerticalAxis;
    public Vector2 Axis { get; private set; }
    public Vector2 LastAxis { get => GetLastAxis(); } 
    public bool ColorButtonHold { get; private set; }
    public bool ColorButtonDown { get; private set; }
    public bool ColorButtonUp{ get; private set; }
    public bool TongueButtonHold { get; private set; }
    public bool TongueButtonDown { get; private set; }
    public bool TongueButtonUp { get; private set; }
    public PlayerInputActions playerInputs;
    private InputAction move, lick, colorpower;

    #region Enable/Disable

    private void Awake()
    {
        playerInputs = new PlayerInputActions();
    }
    private void OnEnable()
    {
        move = playerInputs.Player.Move;
        lick = playerInputs.Player.Lick;
        colorpower = playerInputs.Player.ColorPower;
        move.Enable();
        lick.Enable();
        colorpower.Enable();
    }

    private void OnDisable()
    {
        move.Disable();
        lick.Disable();
        colorpower.Disable();
    }
    #endregion
    // Update is called once per frame
    void Update()
    {
        ColorButtonHold = colorpower.IsPressed();
        ColorButtonDown = colorpower.WasPressedThisFrame();
        ColorButtonUp = colorpower.WasReleasedThisFrame();
        
        TongueButtonHold = lick.IsPressed();
        TongueButtonDown = lick.WasPressedThisFrame();
        TongueButtonUp = lick.WasReleasedThisFrame();

        if (Input.GetAxis("Horizontal") != 0)
            LastHorizontalAxis = Input.GetAxisRaw("Horizontal");

        if (Input.GetAxis("Vertical") != 0)
            LastVerticalAxis = Input.GetAxisRaw("Vertical");

    }


    public void OnMove(InputAction.CallbackContext value)
    {
        Axis = value.ReadValue<Vector2>();
    }
    public void OnLick(InputAction.CallbackContext value)
    {
        TongueButtonDown = value.ReadValue<bool>();
    }
    public void OnColorPower(InputAction.CallbackContext value)
    {
        ColorButtonHold = value.ReadValue<bool>();
    }


    private Vector2 GetLastAxis()
    {
        return new Vector2(LastHorizontalAxis, LastVerticalAxis).normalized;
    }
}
