using UnityEngine;
using System.Collections;

public class MonsterEntity : RoomEntity
{

	private int hp = 100;

	public override void handleCollision(){
		SceneLoader loader = SceneLoader.GetInstance();
		Game.GetInstance ().enemy = this;
		loader.load("BattleState");
	}

    public void setHP(int hp)
    {
        this.hp = hp;
    }

	public int getHP(){
		return this.hp;
	}

	public void removeHP(int dmg){
		this.hp -= dmg;
        if (this.hp < 0)
        {
            this.hp = 0;
        }
	}
}

