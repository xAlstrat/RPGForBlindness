using UnityEngine;
using System.Collections;

/// <summary>
/// A keyboard movement controller.
/// 
/// This class allows to control the movement of the player using the keyboard.
/// </summary>
public class KeyboardMovementController : PlayerMovementController
{
	protected override bool leftEvent(){
		return Input.GetKey (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.LeftArrow);
	}

	protected override bool rightEvent(){
		return Input.GetKey (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.RightArrow);
	}

	protected override bool aheadEvent(){
		return Input.GetKey (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.UpArrow);
	}
}

