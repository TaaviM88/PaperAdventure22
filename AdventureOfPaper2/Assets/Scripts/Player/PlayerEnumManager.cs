using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnumManager : MonoBehaviour
{
    public PlayerMoveState moveState = PlayerMoveState.idle;

    public PlayerLookDir lookDir = PlayerLookDir.right;


    public void SetMoveState(PlayerMoveState newState)
    {
        moveState = newState;
    }

    public void SetLookDirection(PlayerLookDir newState)
    {
        lookDir = newState;
    }

    public PlayerLookDir GetLookDir()
    {
        return lookDir;
    }

    public PlayerMoveState GetMoveState()
    {
        return moveState;
    }
}
