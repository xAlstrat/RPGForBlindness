using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Battle : MonoBehaviour {
	
	private BattleStates currentState;
    private AbilityStates currentAbility;
	private AbilityStates[] abilities;
	private MonsterEntity enemy;
	private Player player;
	private SoundMap soundMap;
	private SceneLoader loader;
	/*public AudioClip agua;
	public AudioClip fuego;
	public AudioClip trueno;*/

    public Text winText;
    public Text hpText;
    public Text enemyHpText;
    public Text abilityText;


	public enum BattleStates{
		START,
		PLAYER_TURN,
		ENEMY_TURN,
		VICTORY,
		DEFEAT,
		END
	}

    public enum Rotation{
		LEFT,
		RIGHT,
		UP,
		DOWN
	}

    void Start(){

        player = Game.GetInstance ().player;
		enemy = Game.GetInstance ().enemy;
		currentState = BattleStates.START;
		abilities = player.CurrentAbilityStates;
		currentAbility = abilities[0];
		soundMap = new SoundMap();
		loader = SceneLoader.GetInstance();
    }
	
	void Update(){

        hpText.text = "HP: " + player.getHP().ToString();
        enemyHpText.text = "HP: " + enemy.getHP().ToString();
        abilityText.text = "Habilidad: " + currentAbility.ToString();

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

				Game.GetInstance ().enemy = null;
	            winText.text = "¡Ganaste!";
				player.CurrentAbilityStates = abilities;
	            currentState = BattleStates.END;
				loader.load(loader.persistentScenes[0]);
				break;

			case(BattleStates.DEFEAT):

	            winText.text = "Has perdido";
	            currentState = BattleStates.END;
				loader.load("Welcome");
				break;
		}
		
	}


    public int calculateDamage(){

        int damage = 0;
        switch (currentAbility)
        {
            case (AbilityStates.AGUA):
                damage = 3;
                break;
            case (AbilityStates.TIERRA):
                damage = 4;
                break;
            case (AbilityStates.FUEGO):
                damage = 5;
                break;

        }

        return damage;
    }


	public void rotateCube(Rotation direction){
		//orden: inicial 0, izq 1, atras 2, der 3, arriba 4, abajo 5

		AbilityStates aux = abilities[0];;

		switch(direction){

			case(Rotation.LEFT):
				abilities[0] = abilities[3];
				abilities[3] = abilities[2];
				abilities[2] = abilities[1];
				abilities[1] = aux;
				break;

			case(Rotation.RIGHT):
				abilities[0] = abilities[1];
				abilities[1] = abilities[2];
				abilities[2] = abilities[3];
				abilities[3] = aux;
				break;

			case(Rotation.UP):
				abilities[0] = abilities[5];
				abilities[5] = abilities[2];
				abilities[2] = abilities[4];
				abilities[4] = aux;
				break;

			case(Rotation.DOWN):
				abilities[0] = abilities[4];
				abilities[4] = abilities[2];
				abilities[2] = abilities[5];
				abilities[5] = aux;
				break;
		}
	}


	public void playerTurn(){

		if (endTurn ()) {
			currentState = BattleStates.ENEMY_TURN;
		}
		else if (playerAttack ()) {

			int dmg = calculateDamage();
			enemy.removeHP (dmg);
			string clip = soundMap.getAttackClip(currentAbility);
			SoundManager.instance.PlaySingle(clip);
			if (enemy.getHP () <= 0) {
                Game.GetInstance().playerHP.text = "HP: "+ player.getHP();
				currentState = BattleStates.VICTORY;
			} else {
				currentState = BattleStates.ENEMY_TURN;
			}
		}
		else if (leftEvent ()) {
			rotateCube(Rotation.LEFT);
			currentAbility = abilities[0];
			string clip = soundMap.getSelectionClip(currentAbility);
			SoundManager.instance.PlaySingle(clip);
		}
		else if (rightEvent ()) {
			rotateCube(Rotation.RIGHT);
            currentAbility = abilities[0];
			string clip = soundMap.getSelectionClip(currentAbility);
			SoundManager.instance.PlaySingle(clip);
		}
		else if (aheadEvent ()) {
			rotateCube(Rotation.UP);
			currentAbility = abilities[0];
			string clip = soundMap.getSelectionClip(currentAbility);
			SoundManager.instance.PlaySingle(clip);
		}
		else if (backEvent()){
			rotateCube(Rotation.DOWN);
			currentAbility = abilities[0];
			string clip = soundMap.getSelectionClip(currentAbility);
			SoundManager.instance.PlaySingle(clip);
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

	protected bool backEvent(){
		return Input.GetKeyUp (KeyCode.DownArrow);
	}
	
	protected bool playerAttack(){
		return Input.GetKeyUp (KeyCode.J);
	}
	
	protected bool endTurn(){
		return Input.GetKeyUp (KeyCode.K);
	}
}
	