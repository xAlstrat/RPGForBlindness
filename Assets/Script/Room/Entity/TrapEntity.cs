using UnityEngine;
using System.Collections;

public class TrapEntity : RoomEntity
{

	public override void handleCollision(){
		Debug.Log("trap collision");
	}
}

