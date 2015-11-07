using UnityEngine;
using System.Collections;

public class GeometricEntity : RoomEntity
{

	public override void handleCollision(){
		Debug.Log("geometric collision");
	}

	public override bool destroyable(){
		return true;
	}
}

