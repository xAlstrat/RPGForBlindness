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
		if (state.Buttons.A == ButtonState.Pressed) {
			if(player.state == PlayerState.NO_STAFF){
				player.pickStaff();
				return;
			}
			if(player.state != PlayerState.STOPPED)
				return;
			if(upEvent())
				player.askAhead();
			else if(rightEvent())
				player.askRight();
			else if(leftEvent())
				player.askLeft();
			return;
		}
        else if (rightEvent())
        {
            player.turnRight();
        }
        else if (leftEvent())
        {
            player.turnLeft();
        }

        if (upEvent())
        {
            player.move();
        }

        Vector2 pos = Game.GetInstance().player.getPosition();
        float distance = Room.GetInstance().getMinDistanceMonster(pos);
        if (distance<3.0f)
        {
            float coef = 0.1f + ((3.0f - distance) * 0.2f / 3.0f);
            GamePad.SetVibration(playerIndex, coef, coef);
        }
        else
        {
            GamePad.SetVibration(playerIndex, 0.0f, 0.0f);
        }

    }

	private bool leftEvent(){
		return state.ThumbSticks.Left.X < 0;
	}
	
	private bool rightEvent(){
		return state.ThumbSticks.Left.X > 0;
	}
	
	private bool upEvent(){
		return state.ThumbSticks.Left.Y > 0;
	}
}