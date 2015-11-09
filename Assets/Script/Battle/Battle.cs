using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using XInputDotNetPure;

public class Battle : MonoBehaviour {

	private bool wasDeathSfxPlayed;
	
	private BattleStates currentState;
    private AbilityStates currentAbility;
	private AbilityStates[] abilities;

	private MonsterEntity enemy;
    public double enemyMaxHp;
	private Player player;
	private SoundMap soundMap;
	private SceneLoader loader;

    public Text winText;
    public Text hpText;
    public Text enemyHpText;
    public Text abilityText;

    PlayerIndex playerIndex = 0;
    GamePadState state;
    GamePadState prevState;

    public enum BattleStates{
		START,
		PLAYER_TURN,
		ENEMY_TURN,
		VICTORY,
		DEFEAT,
		END
	}

    void Start(){

		wasDeathSfxPlayed = false;

        player = Game.GetInstance ().player;
		enemy = Game.GetInstance ().enemy;
        enemyMaxHp = enemy.getHP();
        GamePad.SetVibration(playerIndex, 0.0f, 0.0f); //hardcode

        currentState = BattleStates.START;
		abilities = player.CurrentAbilityStates;
		currentAbility = abilities[0];
		soundMap = new SoundMap();
		loader = SceneLoader.GetInstance();
		SoundManager.instance.PlaySingle("Monster");
    }
	
	void Update(){

        prevState = state;
        state = GamePad.GetState(playerIndex);

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

                Room.GetInstance().removeMonster(enemy.getPosition());
                Game.GetInstance ().enemy = null;
                
                winText.text = "¡Ganaste!";
				player.CurrentAbilityStates = abilities;

				if(!wasDeathSfxPlayed){
					SoundManager.instance.PlaySingle("monster_death");
					wasDeathSfxPlayed = true;
				}
				
				if(!SoundManager.instance.isEfxPlaying() && wasDeathSfxPlayed){
					currentState = BattleStates.END;
					loader.load(loader.persistentScenes[0]);
					SoundManager.instance.PlayMusic("Hidden Agenda");
				}
				break;

			case(BattleStates.DEFEAT):

	            winText.text = "Has perdido";
	            currentState = BattleStates.END;
				loader.cleanLoad("HallState");
				break;
		}
		
	}


    public int calculateDamage(){

        int baseDamage, finalDamage;
		double multiplier;

		baseDamage = player.getBaseDamage(currentAbility);

		multiplier = enemy.getMultiplier(currentAbility);

		finalDamage = (int)System.Math.Floor(baseDamage * multiplier);

        return finalDamage;
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

		//Debug.Log(currentAbility + " base damage: " + player.getBaseDamage(currentAbility));

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
        else if (myHP())
        {
            string clip = "playerhp"+ System.Math.Ceiling((double)player.getHP()*10 / player.getMaxHP()) * 10;
            SoundManager.instance.PlaySingle(clip);
        }
        else if (enemyHP())
        {
            string clip = "enemyhp" + System.Math.Ceiling(10 * enemy.getHP() / enemyMaxHp) * 10;
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
		return Input.GetKeyUp (KeyCode.LeftArrow) || (prevState.DPad.Left == ButtonState.Released && state.DPad.Left == ButtonState.Pressed);
	}
	
	protected bool rightEvent(){
		return Input.GetKeyUp (KeyCode.RightArrow) || (prevState.DPad.Right == ButtonState.Released && state.DPad.Right == ButtonState.Pressed);
	}
	
	protected bool aheadEvent(){
		return Input.GetKeyUp (KeyCode.UpArrow) || (prevState.DPad.Up == ButtonState.Released && state.DPad.Up == ButtonState.Pressed);
	}

	protected bool backEvent(){
		return Input.GetKeyUp (KeyCode.DownArrow) || (prevState.DPad.Down == ButtonState.Released && state.DPad.Down == ButtonState.Pressed);
	}

    protected bool myHP()
    {
        return Input.GetKeyUp(KeyCode.U) || (prevState.Triggers.Left >0);
    }

    protected bool enemyHP()
    {
        return Input.GetKeyUp(KeyCode.I) || (prevState.Triggers.Right > 0);
    }

    protected bool playerAttack(){
		return Input.GetKeyUp (KeyCode.J) || (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed);
	}
	
	protected bool endTurn(){
		return Input.GetKeyUp (KeyCode.K) || (prevState.Buttons.B == ButtonState.Released && state.Buttons.B == ButtonState.Pressed);
	}
}
	