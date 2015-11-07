using UnityEngine;
using System.Collections;

public class DownConditionalSignalEntity : ConditionalSignalEntity
{

	protected override bool condition ()
	{
		return Game.GetInstance().player.getOrientation().Equals(Orientation.SOUTH);
	}
}

