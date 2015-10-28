using UnityEngine;
using System.Collections;

public class MonsterEntity : RoomEntity
{

	public override void handleCollision(){
		SceneLoader loader = SceneLoader.GetInstance();
		loader.load("BattleState");
	}
}

