using UnityEngine;
using System.Collections;

/// <summary>
/// A player movement controller.
/// 
/// This class allows to control the movement of the player.
/// </summary>
public abstract class PlayerMovementController : PlayerController
{

	public void Update ()
	{
		if (rightEvent ()) {
			player.turnRight ();
		} else if (leftEvent ()) {
			player.turnLeft ();
		} else if (aheadEvent ()) {
			player.move();
		}
	}

	/// <summary>
	/// Left movement event.
	/// </summary>
	/// <returns><c>true</c>, if event was received, <c>false</c> otherwise.</returns>
	protected abstract bool leftEvent();

	/// <summary>
	/// Right movement event.
	/// </summary>
	/// <returns><c>true</c>, if event was received, <c>false</c> otherwise.</returns>
	protected abstract bool rightEvent();

	/// <summary>
	/// Aheads movement event.
	/// </summary>
	/// <returns><c>true</c>, if event was received, <c>false</c> otherwise.</returns>
	protected abstract bool aheadEvent();
}

