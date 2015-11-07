using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public abstract class PlayerBattleController : BattleController
{
    /*
    PlayerIndex playerIndex = 0;
    GamePadState state;
    GamePadState prevState;

    */

    public void Update()
    {
       /*  getMovement(); */
    }

    protected abstract void getMovement();

}