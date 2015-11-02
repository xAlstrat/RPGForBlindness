using UnityEngine;
using System.Collections;

public class MonsterEntity : RoomEntity
{

	private int hp = 3;

	public override void handleCollision(){
		SceneLoader loader = SceneLoader.GetInstance();
		Game.GetInstance ().enemy = this;
		loader.load("BattleState");
	}

	public int getHP(){
		return this.hp;
	}

	public void removeHP(int dmg){
		this.hp -= dmg;
	}
}

