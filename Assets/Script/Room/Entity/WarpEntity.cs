using UnityEngine;
using System.Collections;

public class WarpEntity : SignalEntity
{

	protected void Awake(){
		source  = "warp";
	}
	
	private void nextScene(){
		SceneLoader.GetInstance ().load("GeometricState");
	}
	
	public override void handleCollision(){
		base.handleCollision ();
		Invoke ("nextScene", 3);
	}
}

