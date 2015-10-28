using UnityEngine;
using System.Collections;

public class TrapEntity : RoomEntity
{

	public override void handleCollision(){
		SceneLoader.GetInstance ().cleanLoad ("HallState");
	}
}

