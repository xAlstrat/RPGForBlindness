using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using XInputDotNetPure;

public class Battle : MonoBehaviour {

	private bool wasDeathSfxPlayed;
	private bool wasActionSfxPlayed;
	private bool wasVictoryMusicPlayed;
	private bool extraPlayerSoundsNeeded;
	private bool wasEnemySfxPlayed;

	private string atkSuffix;
	private string enemyAtkSfx;
	private string enemyAtkSpeech;

	private TurnPhase currentPhase;
	private TurnPhase enemyPhase;
	private Rotation currentRotation;
	
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

	enum TurnPhase{
		BEGINNING,
		SELECTION,
		CONFIRMATION,
		CALCULATIONS,
		VISIBLE_ACTIONS,
		ENDING
	}


    void Start(){

		wasDeathSfxPlayed = false;
		wasActionSfxPlayed = false;
		wasVictoryMusicPlayed = false;
		extraPlayerSoundsNeeded = false;

		atkSuffix = "";
		enemyAtkSfx = "";
		enemyAtkSpeech = "";

		currentPhase = TurnPhase.BEGINNING;
		enemyPhase = TurnPhase.BEGINNING;

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

        if (ApplicationData.getLastLevel() == 2)
        {
            SoundManager.instance.PlaySingle("instrucciones_batalla");

        }
    }
	
	void Update(){

        prevState = state;
        state = GamePad.GetState(playerIndex);

        hpText.text = "HP: " + player.getHP().ToString();
        enemyHpText.text = "HP: " + enemy.getHP().ToString();
        abilityText.text = "Habilidad: " + currentAbility.ToString();

        switch (currentState) {

			case(BattleStates.START):
				wasDeathSfxPlayed = false;
				wasVictoryMusicPlayed = false;
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
				
				if(!wasVictoryMusicPlayed && !SoundManager.instance.isEfxPlaying()){
					SoundManager.instance.StopMusic();
					SoundManager.instance.PlaySingle("battle_victory_music");
					wasVictoryMusicPlayed = true;
				}
				
				if(!SoundManager.instance.isEfxPlaying() && wasVictoryMusicPlayed){
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

	private void selection(){
		if(endTurn())
			currentPhase = TurnPhase.ENDING;
		else if(playerAttack())
			currentPhase = TurnPhase.CONFIRMATION;
	}

	private void rotate(){
		if(leftEvent() || rightEvent() || aheadEvent() || backEvent()){
			rotateCube(currentRotation);
			currentAbility = abilities[0];
			string clip = soundMap.getSelectionClip(currentAbility);
			SoundManager.instance.PlaySingle(clip);
		}
	}

	private void feedBack(){
		string audioClip = "";

		if (askMyHP()){
			audioClip = "playerhp"+ System.Math.Ceiling((double)player.getHP()*10 / player.getMaxHP()) * 10;
		}
		else if (askEnemyHP()){
			audioClip = "enemyhp" + System.Math.Ceiling(10 * enemy.getHP() / enemyMaxHp) * 10;
		}
		else if(askCurrentAbility()){
			audioClip = "current_ability_" + currentAbility.ToString().ToLower();
		}

		if(!audioClip.Equals(""))
			SoundManager.instance.PlaySingle(audioClip);
	}

	public void playerTurn(){

		switch(currentPhase){
			case(TurnPhase.BEGINNING):
				wasActionSfxPlayed = false;
				currentPhase = TurnPhase.SELECTION;
				break;
			case(TurnPhase.SELECTION):
				rotate();
				feedBack();
				selection();
				break;
			case(TurnPhase.CONFIRMATION):
				currentPhase = TurnPhase.CALCULATIONS;
				break;
			case(TurnPhase.CALCULATIONS):
				int dmg = calculateDamage();
				enemy.removeHP (dmg);

				if(dmg <= 0){
					extraPlayerSoundsNeeded = true;
					atkSuffix = "immune";
				}
				else if(enemy.getMultiplier(currentAbility) == 1){
					extraPlayerSoundsNeeded = true;
					atkSuffix = "regular";
				}
				else if(enemy.getMultiplier(currentAbility) == 0.5 && dmg > 0){
					extraPlayerSoundsNeeded = true;
					atkSuffix = "ineffective";
				}
				else if(enemy.getMultiplier(currentAbility) == 2){
					extraPlayerSoundsNeeded = true;
					atkSuffix = "very_effective";
				}

				if(enemy.getHP() <= 0)
					extraPlayerSoundsNeeded = false;

				currentPhase = TurnPhase.VISIBLE_ACTIONS;
				break;
			case(TurnPhase.VISIBLE_ACTIONS):
				string clip = soundMap.getAttackClip(currentAbility);

				if(!wasActionSfxPlayed){
					SoundManager.instance.PlaySingle(clip);
					wasActionSfxPlayed = true;
				}

				if(extraPlayerSoundsNeeded){
					if(!SoundManager.instance.isEfxPlaying()){
						SoundManager.instance.PlaySingle("attack_" + atkSuffix);
						extraPlayerSoundsNeeded = false;
					}
					//por si se quieren saltar el feedback del ataque
					else if(Input.GetKeyUp (KeyCode.H)){
						extraPlayerSoundsNeeded = false;
						SoundManager.instance.StopSingle();
						currentPhase = TurnPhase.ENDING;
					}
				}
				else
					currentPhase = TurnPhase.ENDING;

				break;
			case(TurnPhase.ENDING):
				if (enemy.getHP () <= 0) {
					Game.GetInstance().playerHP.text = "HP: "+ player.getHP();
					currentState = BattleStates.VICTORY;
				} else {
					currentState = BattleStates.ENEMY_TURN;
				}
				currentPhase = TurnPhase.BEGINNING;
				break;
		}
	}


	public void enemyTurn(){
	//hacer mejor comportamiento enemigo

		switch(enemyPhase){
			case(TurnPhase.BEGINNING):
				wasEnemySfxPlayed = false;
				enemyPhase = TurnPhase.SELECTION;
				break;
			case(TurnPhase.SELECTION):
				enemyPhase = TurnPhase.CONFIRMATION;
				break;
			case(TurnPhase.CONFIRMATION):
				enemyPhase = TurnPhase.CALCULATIONS;
				break;
			case(TurnPhase.CALCULATIONS):
				int dmg = Random.Range (0, 11);
				player.removeHP (dmg);

				if(dmg == 0){
					enemyAtkSpeech = "enemy_attack_miss_speech";
					enemyAtkSfx = "enemy_attack_miss_sfx";
				}

				if(0 < dmg && dmg <= 7){
					enemyAtkSpeech = "enemy_attack_light_hit_speech";
					enemyAtkSfx = "enemy_attack_light_hit_sfx";
				}

				if(7 < dmg && dmg <= 10){
					enemyAtkSpeech = "enemy_attack_heavy_hit_speech";
					enemyAtkSfx = "enemy_attack_heavy_hit_sfx";
				}

				enemyPhase = TurnPhase.VISIBLE_ACTIONS;
				break;
			case(TurnPhase.VISIBLE_ACTIONS):
				if(!SoundManager.instance.isEfxPlaying() && !extraPlayerSoundsNeeded && !wasEnemySfxPlayed){
					SoundManager.instance.PlaySingle(enemyAtkSfx);
					wasEnemySfxPlayed = true;
				}
				else if(!SoundManager.instance.isEfxPlaying() && wasEnemySfxPlayed){
					SoundManager.instance.PlaySingle(enemyAtkSpeech);
					enemyPhase = TurnPhase.ENDING;
				}
				else if(Input.GetKeyUp (KeyCode.H)){
					SoundManager.instance.StopSingle();
					enemyPhase = TurnPhase.ENDING;
				}
				break;
			case(TurnPhase.ENDING):
				if (player.getHP () > 0)
				currentState = BattleStates.PLAYER_TURN;
				else
				currentState = BattleStates.DEFEAT;

				enemyPhase = TurnPhase.BEGINNING;
				break;
		}
	}
	
	protected bool leftEvent(){
		currentRotation = Rotation.LEFT;
		return Input.GetKeyUp (KeyCode.LeftArrow) || (prevState.DPad.Left == ButtonState.Released && state.DPad.Left == ButtonState.Pressed);
	}
	
	protected bool rightEvent(){
		currentRotation = Rotation.RIGHT;
		return Input.GetKeyUp (KeyCode.RightArrow) || (prevState.DPad.Right == ButtonState.Released && state.DPad.Right == ButtonState.Pressed);
	}
	
	protected bool aheadEvent(){
		currentRotation = Rotation.UP;
		return Input.GetKeyUp (KeyCode.UpArrow) || (prevState.DPad.Up == ButtonState.Released && state.DPad.Up == ButtonState.Pressed);
	}

	protected bool backEvent(){
		currentRotation = Rotation.DOWN;
		return Input.GetKeyUp (KeyCode.DownArrow) || (prevState.DPad.Down == ButtonState.Released && state.DPad.Down == ButtonState.Pressed);
	}

    protected bool askMyHP(){
        return Input.GetKeyUp(KeyCode.U) || (prevState.Triggers.Left >0);
    }

    protected bool askEnemyHP(){
        return Input.GetKeyUp(KeyCode.I) || (prevState.Triggers.Right > 0);
    }

	protected bool askCurrentAbility(){
		return Input.GetKeyUp (KeyCode.O) || (prevState.Buttons.X == ButtonState.Released && state.Buttons.X == ButtonState.Pressed);
	}

    protected bool playerAttack(){
		return Input.GetKeyUp (KeyCode.J) || (prevState.Buttons.A == ButtonState.Released && state.Buttons.A == ButtonState.Pressed);
	}
	
	protected bool endTurn(){
		return Input.GetKeyUp (KeyCode.K) || (prevState.Buttons.B == ButtonState.Released && state.Buttons.B == ButtonState.Pressed);
	}
}