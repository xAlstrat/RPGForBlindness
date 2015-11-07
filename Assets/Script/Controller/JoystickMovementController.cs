using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System;

/// <summary>
/// A keyboard movement controller.
/// 
/// This class allows to control the movement of the player using the keyboard.
/// </summary>
public class JoystickMovementController : PlayerMovementController
{
    PlayerIndex playerIndex = 0;
    GamePadState state;
    GamePadState prevState;


    protected override void getMovement()
    {
        prevState = state;
        state = GamePad.GetState(playerIndex);
        /*
        if ((prevState.Buttons.X == ButtonState.Released && state.Buttons.X == ButtonState.Pressed)|| (prevState.Buttons.X == ButtonState.Pressed && state.Buttons.X == ButtonState.Pressed))
        {
            player.turnLeft();
        }
        if (prevState.Buttons.B == ButtonState.Released && state.Buttons.B == ButtonState.Pressed)
        {
            player.turnRight();
        }
        if (prevState.Buttons.Y == ButtonState.Released && state.Buttons.Y == ButtonState.Pressed)
        {
            player.move();
        }
        */

        if (state.ThumbSticks.Left.X > 0)
        {
            player.turnRight();
        }
        else if (state.ThumbSticks.Left.X < 0)
        {
            player.turnLeft();
        }

        if (state.ThumbSticks.Left.Y > 0)
        {
            player.move();
        }

        Vector2 pos = Game.GetInstance().player.getPosition();
        if (pos.x == 6.0)
        {
            GamePad.SetVibration(playerIndex, 0.5f, 0.5f);
        }
        else
        {
            GamePad.SetVibration(playerIndex, 0.0f, 0.0f);
        }

    }
}