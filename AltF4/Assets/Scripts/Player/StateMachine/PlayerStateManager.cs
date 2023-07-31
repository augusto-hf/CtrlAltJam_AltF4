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

    void Start()
    {
        
    }

    void Update()
    {
        


    }
}