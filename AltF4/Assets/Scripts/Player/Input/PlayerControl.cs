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
    private PlayerInputActions PlayerInputs;
    private InputAction moveInput, colorPowerInput, tongueInput;

    #region Enable/Disable

    private void Awake()
    {
        // para entender mais de como todo o codigo funciona, de uma olhada na introdução ao novo input system, na parte sobre migração
        PlayerInputs = new PlayerInputActions();

        //moveInput = PlayerInputs.Player.Move; fica dando (1,0)

        colorPowerInput = PlayerInputs.Player.ColorPower;
        tongueInput = PlayerInputs.Player.Lick;

        
    }
    private void OnEnable()
    {
        PlayerInputs.Enable();
    }
    private void OnDisable()
    {
        PlayerInputs.Disable();
    }
    #endregion
    // Update is called once per frame
    void Update()
    {
        
        ColorButtonDown = colorPowerInput.WasPerformedThisFrame();
        ColorButtonHold = colorPowerInput.IsPressed();
        ColorButtonUp = colorPowerInput.WasReleasedThisFrame();
        

        TongueButtonDown = tongueInput.WasPerformedThisFrame();
        TongueButtonHold = tongueInput.IsPressed();
        TongueButtonUp = tongueInput.WasReleasedThisFrame();

        if (Axis.x != 0)
            LastHorizontalAxis = Axis.x;

        if (Axis.y != 0)
            LastVerticalAxis = Axis.y;

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        Axis = context.ReadValue<Vector2>();
        //Debug.Log(Axis);
    }


    private Vector2 GetLastAxis()
    {
        return new Vector2(LastHorizontalAxis, LastVerticalAxis).normalized;
    }
}
