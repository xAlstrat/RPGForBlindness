using UnityEngine;
using System.Collections.Generic;

public class MonsterEntity : RoomEntity
{

	private int hp = 15;
	private Dictionary<AbilityStates, double> stats;
	private double[] multipliers = new double[]{0.5, 1, 2};

	public override void handleCollision(){
		SceneLoader loader = SceneLoader.GetInstance();
		Game.GetInstance ().enemy = this;
		initializeStats();
		loader.load("BattleState");
	}

	private void initializeStats(){
		stats = new Dictionary<AbilityStates, double>();
		//Debug.Log("stats");

		foreach(AbilityStates AS in System.Enum.GetValues(typeof(AbilityStates))){
			int rand = Random.Range(0, 3);
			stats.Add (AS, multipliers[rand]);

			//Debug.Log(AS.ToString() + ": " + multipliers[rand]);
		}
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

	public double getMultiplier(AbilityStates ability){
		return stats[ability];
	}

	public override bool destroyable(){
		return true;
	}
}

