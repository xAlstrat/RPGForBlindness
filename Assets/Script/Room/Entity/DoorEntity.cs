using UnityEngine;
using System.Collections;

public class DoorEntity : RoomEntity
{

	public override void handleCollision(){
		ApplicationData.setLastLevel (2 -(ApplicationData.getLastLevel () + 1) % 2);
		SceneLoader.GetInstance ().cleanLoad ("HallState");
	}
}

