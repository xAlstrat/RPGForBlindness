using UnityEngine;
using System.Collections;

public class TreasureEntity : SignalEntity
{
	protected void Awake(){
		source  = "item_pickup";
	}

	public override void handleCollision(){
		base.handleCollision ();
		Debug.Log("treasure collision");
	}

	public override bool destroyable(){
		return true;
	}

	public override void ask ()
	{
		Debug.Log("Tesoro");
	}
}

