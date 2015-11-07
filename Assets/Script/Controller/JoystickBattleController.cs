using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class JoystickBattleController : PlayerBattleController {

    PlayerIndex playerIndex = 0;
    GamePadState state;
    GamePadState prevState;

    protected override void getMovement()
    {
        /*
        if (prevState.DPad.Left == ButtonState.Released && state.DPad.Left == ButtonState.Pressed) { cube.turnLeft(); }
        else if (prevState.DPad.Right == ButtonState.Released && state.DPad.Right == ButtonState.Pressed) { cube.turnRight(); }
        else if (prevState.DPad.Up == ButtonState.Released && state.DPad.Up == ButtonState.Pressed) { cube.turnUp(); }
        else if (prevState.DPad.Down == ButtonState.Released && state.DPad.Down == ButtonState.Pressed) { cube.turnDown(); }
        else if (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed) { cube.attack(); }
        else if (prevState.Buttons.B == ButtonState.Released && state.Buttons.B == ButtonState.Pressed) { cube.hold(); }
        */

    }
}
