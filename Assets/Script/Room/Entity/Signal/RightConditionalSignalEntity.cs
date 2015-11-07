using UnityEngine;
using System.Collections;

public class RightConditionalSignalEntity : ConditionalSignalEntity
{

	protected override bool condition ()
	{
		return Game.GetInstance().player.getOrientation().Equals(Orientation.EAST);
	}
}

