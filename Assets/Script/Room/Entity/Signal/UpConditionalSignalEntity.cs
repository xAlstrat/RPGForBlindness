using UnityEngine;
using System.Collections;

public class UpConditionalSignalEntity : ConditionalSignalEntity
{

	protected override bool condition ()
	{
		return Game.GetInstance().player.getOrientation().Equals(Orientation.NORTH);
	}
}

