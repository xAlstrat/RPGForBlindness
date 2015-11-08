using UnityEngine;
using System.Collections;

public class DoorEntity : SignalEntity
{

	protected void Awake(){
		source  = "crashDoor";
	}

	private void nextScene(){
		ApplicationData.setLastLevel (2 -(ApplicationData.getLastLevel () + 1) % 2);
		SceneLoader.GetInstance ().cleanLoad ("LevelCompleted");
	}

	public override void handleCollision(){
		base.handleCollision ();
		Invoke ("nextScene", 3);
	}
}

