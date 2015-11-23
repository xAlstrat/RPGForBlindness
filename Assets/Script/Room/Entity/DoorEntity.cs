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
		Game.GetInstance().player.disableMovement();
		Game.GetInstance ().player.wait (3);
		Invoke ("nextScene", 3);
	}

	public override void ask ()
	{
		Debug.Log("Puerta");
	}
}

