using UnityEngine;
using System.Collections;

public class TreasureEntity : SignalEntity
{
	protected void Awake(){
		source  = "item_pickup";
	}

	public override void handleCollision(){
		base.handleCollision ();
		Game.GetInstance ().player.wait (1.5f);
	}

	public override bool destroyable(){
		return true;
	}

	public override void ask ()
	{
		SoundManager.instance.PlaySingle ("tomar-tesoro");
	}
}

