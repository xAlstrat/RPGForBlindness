using UnityEngine;
using System.Collections;

public class DoorEntity : SignalEntity
{

	protected void Awake(){
		source  = "crashDoor";
	}

	private void nextScene(){
		ApplicationData.setLastLevel (1 +ApplicationData.getLastLevel () % 3);
		SceneLoader.GetInstance ().cleanLoad ("LevelCompleted");
	}

	public override void handleCollision(){
		base.handleCollision ();
		Invoke ("nextScene", 3);
	}
}

