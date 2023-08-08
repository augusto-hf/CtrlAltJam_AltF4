using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public Vector2 Axis { get; private set; }
    public float LastHorizontalAxis { get => GetLastHorizontalAxis(); } 
    public bool ColorButtonHold { get; private set; }
    public bool ColorButtonDown { get; private set; }
    public bool ColorButtonUp{ get; private set; }
    public bool TongueButtonHold { get; private set; }
    public bool TongueButtonDown { get; private set; }
    public bool TongueButtonUp { get; private set; }
    public bool LeftButtonHold { get; private set; }
    public bool RightButtonHold { get; private set; }

    private PlayerInputActions PlayerInputs;
    private InputAction moveInput, colorPowerInput, tongueInput;
    private float LocalLastHorizontalAxis = 1;

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

    }
    public void OnMove(InputAction.CallbackContext context)
    {
        Axis = context.ReadValue<Vector2>();
        //Debug.Log(Axis);
    }


    private float GetLastHorizontalAxis()
    {
        if (Axis.x > 0)
        {//Direita
            LocalLastHorizontalAxis = 1;
            LeftButtonHold = false;
            RightButtonHold = true;
        }

        else if (Axis.x < 0)
        {//Esquerda
            LocalLastHorizontalAxis = -1;
            LeftButtonHold = true;
            RightButtonHold = false;
        }

        else
        {
            LeftButtonHold = false;
            RightButtonHold = false;
        }

        return LocalLastHorizontalAxis;
    }

}
