using UnityEngine;
using System.Collections;

public class LeftConditionalSignalEntity : ConditionalSignalEntity
{

	protected override bool condition ()
	{
		return Game.GetInstance().player.getOrientation().Equals(Orientation.WEST);
	}
}

