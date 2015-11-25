using UnityEngine;
using System.Collections;

public class WaitEntity : SignalEntity
{
	private float duration = 0f;
 public override void handleCollision ()
	{
		base.handleCollision ();
		Game.GetInstance ().player.wait (duration);
	}

	public void setDuration(float duration){
		this.duration = duration;
	}

	public override bool destroyable ()
	{
		return true;
	}
}

