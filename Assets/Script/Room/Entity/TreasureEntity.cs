using UnityEngine;
using System.Collections;

public class TreasureEntity : SignalEntity
{
	protected void Awake(){
		source  = "crashTreasure";
	}

	public override void handleCollision(){
		base.handleCollision ();
		Debug.Log("treasure collision");
	}

	public override bool destroyable(){
		return true;
	}
}

