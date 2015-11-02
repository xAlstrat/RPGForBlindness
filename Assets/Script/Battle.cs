using UnityEngine;
using System.Collections;

public class Battle : MonoBehaviour {
	
	private BattleStates currentState;
	private MonsterEntity enemy;
	private Player player;
	private SceneLoader loader;
	
	public enum BattleStates{
		START,
		PLAYER_TURN,
		ENEMY_TURN,
		VICTORY,
		DEFEAT,
		END
	}
	
	// Use this for initialization
	void Start () {

		player = Game.GetInstance ().player;
		enemy = Game.GetInstance ().enemy;
		currentState = BattleStates.START;

		loader = SceneLoader.GetInstance();
	}
	
	// Update is called once per frame
	void Update () {
		
		switch (currentState) {
		case(BattleStates.START):
			currentState = BattleStates.PLAYER_TURN;
			break;
		case(BattleStates.PLAYER_TURN):
			playerTurn();
			break;
		case(BattleStates.ENEMY_TURN):
			enemyTurn();
			break;
		case(BattleStates.VICTORY):
			//volver a nivel
			Game.GetInstance ().enemy = null;
			currentState = BattleStates.END;
			loader.load(loader.persistentScenes[0]);
			break;
		case(BattleStates.DEFEAT):
			//ir a pantalla gameover
			currentState = BattleStates.END;
			loader.load("Welcome");
			break;
		}
		
	}
	
	public void playerTurn(){
		if (endTurn ()) {
			currentState = BattleStates.ENEMY_TURN;
		} else if (playerAttack ()) {
			//hardcodeado, hay que considerar debilidades o boosts
			int dmg = 1;
			enemy.removeHP (dmg);
			if (enemy.getHP () <= 0) {
				currentState = BattleStates.VICTORY;
			} else {
				currentState = BattleStates.ENEMY_TURN;
			}
		} else if (leftEvent ()) {
			//algo que ver con habilidades
		} else if (rightEvent ()) {
			//algo que ver con habilidades
		} else if (aheadEvent ()) {
			//algo que ver con habilidades
		}
	}
	
	public void enemyTurn(){
		//hacer mejor comportamiento enemigo
		int dmg = Random.Range (0, 5);
		player.removeHP (dmg);

		if (player.getHP () > 0) {
			currentState = BattleStates.PLAYER_TURN;
		}
		else {
			currentState = BattleStates.DEFEAT;
		}
	}
	
	protected bool leftEvent(){
		return Input.GetKeyUp (KeyCode.LeftArrow);
	}
	
	protected bool rightEvent(){
		return Input.GetKeyUp (KeyCode.RightArrow);
	}
	
	protected bool aheadEvent(){
		return Input.GetKeyUp (KeyCode.UpArrow);
	}
	
	protected bool playerAttack(){
		return Input.GetKeyUp (KeyCode.J);
	}
	
	protected bool endTurn(){
		return Input.GetKeyUp (KeyCode.K);
	}
}
	