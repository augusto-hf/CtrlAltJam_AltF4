using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{

    PlayerBaseState currentState;
    PlayerWalkingState walkingState = new PlayerWalkingState();
    PlayerTongueState tongueState = new PlayerTongueState();
    PlayerFallingState fallingState = new PlayerFallingState();
    PlayerPowerJumpingState powerJumpingState = new PlayerPowerJumpingState();
    PlayerPowerRunningState runningState = new PlayerPowerRunningState();


    // Start is called before the first frame updateasdgasdgasdfghasdfhsdfhsdfhdfgjd
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
