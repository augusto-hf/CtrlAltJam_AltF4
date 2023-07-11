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


    #region Enable/Disable

    private void Awake()
    {
        playerInputs = new PlayerInputActions();
        playerInputs.Player.Enable();
        playerInputs.Player.Move.performed += OnMove;
        playerInputs.Player.Lick.performed += OnLick;

    }
    private void OnEnable()
    {
        

    }

    private void OnDisable()
    {
        playerInputs.Player.Enable();
    }
    #endregion
    // Update is called once per frame
    void Update()
    {
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
    public void OnColorPower(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

        }
        else if (context.canceled)
        {

        }


        ColorButtonHold = context.ReadValue<bool>();
    }


    private Vector2 GetLastAxis()
    {
        return new Vector2(LastHorizontalAxis, LastVerticalAxis).normalized;
    }
}
