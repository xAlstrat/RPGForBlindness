using UnityEngine;
using System.Collections;

public class TreasureEntity : RoomEntity
{

	public override void handleCollision(){
		Debug.Log("treasure collision");
	}
}

