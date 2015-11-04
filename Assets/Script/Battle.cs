using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Battle : MonoBehaviour {
	
	private BattleStates currentState;
    private AbilityStates currentAbility;
	private MonsterEntity enemy;
	private Player player;
	private SceneLoader loader;
	public AudioClip agua;
	public AudioClip fuego;
	public AudioClip trueno;

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

    public enum AbilityStates
    {
        AGUA,
        TIERRA,
        FUEGO,
        VIENTO,
        NATURALEZA, 
        ARCANO 
    }

    // Use this for initialization
    void Start () {

        

        player = Game.GetInstance ().player;
		enemy = Game.GetInstance ().enemy;
		currentState = BattleStates.START;
        currentAbility = AbilityStates.AGUA;

		loader = SceneLoader.GetInstance();


    }
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(player.getHP());
        //Debug.Log(enemy.getHP());
        hpText.text = "HP: " + player.getHP().ToString();
        enemyHpText.text = "HP: " + enemy.getHP().ToString();
        abilityText.text = "ABILITY: " + currentAbility.ToString();
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
            winText.text = "You win!!";
            currentState = BattleStates.END;
			loader.load(loader.persistentScenes[0]);
			break;
		case(BattleStates.DEFEAT):
                //ir a pantalla gameover
            winText.text = "You lose";
            currentState = BattleStates.END;
			loader.load("Welcome");
			break;
		}
		
	}
	
    public int calculateDamage()
    {
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

	public void playerTurn(){
		if (endTurn ()) {
			currentState = BattleStates.ENEMY_TURN;
		} else if (playerAttack ()) {
			//hardcodeado, hay que considerar debilidades o boosts
			int dmg = calculateDamage();
			enemy.removeHP (dmg);
			if (enemy.getHP () <= 0) {
                Game.GetInstance().playerHP.text = "HP: "+ player.getHP();
				currentState = BattleStates.VICTORY;
			} else {
				currentState = BattleStates.ENEMY_TURN;
			}
		} else if (leftEvent ()) {
            currentAbility = AbilityStates.AGUA;
			SoundManager.instance.PlaySingle(agua);
			//algo que ver con habilidades
		} else if (rightEvent ()) {
            currentAbility = AbilityStates.FUEGO;
            SoundManager.instance.PlaySingle(fuego);
			//algo que ver con habilidades
		} else if (aheadEvent ()) {
            currentAbility = AbilityStates.TIERRA;
            SoundManager.instance.PlaySingle(trueno);
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
	