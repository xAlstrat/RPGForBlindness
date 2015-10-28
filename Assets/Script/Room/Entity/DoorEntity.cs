using UnityEngine;
using System.Collections;

public class DoorEntity : RoomEntity
{

	public override void handleCollision(){
		Debug.Log("door collision");;
	}
}

