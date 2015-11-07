using UnityEngine;
using System.Collections;

public class CollisionManager
{

	public static void collide(Vector2 pos){
		RoomEntity entity = Room.GetInstance ().getEntityAt (pos);
		if (entity == null)
			return;
		if (entity.destroyable ()) {
			Room.GetInstance().removeEntity(pos);
			GameObject.Destroy(entity.gameObject, 4f);
		}
		entity.handleCollision ();
	}

}

