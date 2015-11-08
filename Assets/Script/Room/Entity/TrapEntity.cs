using UnityEngine;
using System.Collections;

public class TrapEntity : SignalEntity
{
	protected void Awake(){
		source  = "crashTrap";
	}

	public override void handleCollision(){
		base.handleCollision ();
		SceneLoader.GetInstance ().cleanLoad ("HallState");
	}
}

