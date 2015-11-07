using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// A keyboard movement controller.
/// 
/// This class allows to control the movement of the player using the keyboard.
/// </summary>
public class KeyboardMovementController : PlayerMovementController


{
    protected override void getMovement()
    {
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            player.turnLeft();
        }
        else if(Input.GetKey(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            player.turnRight();
        }
        else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            player.move();
        }
    }
}

