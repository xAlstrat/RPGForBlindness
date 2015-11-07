using UnityEngine;
using System.Collections;

public abstract class RoomEntity : MonoBehaviour
{
	public abstract void handleCollision();
	public virtual bool destroyable(){
		return false;
	}
}

