using UnityEngine;
using System.Collections.Generic;
using XInputDotNetPure;

/// <summary>
/// Player.
/// 
/// This class (for now) offer all we need to move the character through the room.
/// </summary>
public class Player : MonoBehaviour
{
    private int maxHp;
	private int hp;
	private int boostsToLevelUp;
	private bool ableToMove;

	private AbilityStates[] currentAbilityStates;
	public AbilityStates[] CurrentAbilityStates{
		get{return currentAbilityStates;}
		set{currentAbilityStates = value;}
	}

	private Dictionary<AbilityStates, int> abilitiesBaseDamage;
	private Dictionary<AbilityStates, int> boosts;

	/// <summary>
	/// The movement restriction of the player.
	/// </summary>
	public PlayerMovementRestriction movementRestriction;

	/// <summary>
	/// The movement speed of the character.
	/// </summary>
	public float speed = 2f;

	/// <summary>
	/// The coord position relative to the room.
	/// </summary>
	private Vector2 position;

	/// <summary>
	/// The current direction where the character is facing.
	/// </summary>
	private int currentDir;

	/// <summary>
	/// The posible directions of the player.
	/// </summary>
	private Vector2[] directions;

	/// <summary>
	/// The current state of the player.
	/// </summary>
	public PlayerState state;


	/// <summary>
	/// The last state of the player before him started to wait.
	/// </summary>
	private PlayerState lastState;

	/// <summary>
	/// The time the player should wait in the wait state.
	/// </summary>
	private float waitTime = 0f;

	public Vector2 direction{
		get{return directions[currentDir];}
	}

	/// <summary>
	/// The countdown for smooth movement.
	/// </summary>
	private float countdown;

	/// <summary>
	/// The start position before moving the player.
	/// </summary>
	private Vector3 startPosition;

	/// <summary>
	/// The destination position where we want to move the player.
	/// </summary>
	private Vector3 destPosition;

	/// <summary>
	/// The start rotation before rotating  the player.
	/// </summary>
	private Quaternion startRotation;

	/// <summary>
	/// The destination rotation the player should have after rotating.
	/// </summary>
	private Quaternion destRotation;

	void Awake(){
		DontDestroyOnLoad (transform.gameObject);
	}

	void Start(){
		directions = new Vector2[]{
			new Vector2(0, -1), //north
			new Vector2(1, 0), //east
			new Vector2(0, 1), //south
			new Vector2(-1, 0) //west
		};
		currentDir = 0;
		startRotation = Quaternion.identity;
		destRotation = Quaternion.identity;
		state = PlayerState.NO_STAFF;

		source = GetComponent<AudioSource>();

		//faces order: front (initial): 0, left: 1, back: 2, right 3, up 4, down 5
		//always starts with water ability unlocked
		currentAbilityStates = new AbilityStates[]{
			AbilityStates.AGUA,
			AbilityStates.TIERRA,
			AbilityStates.FUEGO,
			AbilityStates.VIENTO,
			AbilityStates.NATURALEZA,
			AbilityStates.ARCANO
		};

		boostsToLevelUp = 3;

		ableToMove = true;

		//base damage for each ability starts at 1
		abilitiesBaseDamage = new Dictionary<AbilityStates, int>();

		//boosts for each ability starts at 0
		//every 3 boosts for some ability, the base damage for said ability increases by 1
		boosts = new Dictionary<AbilityStates, int>();

		foreach(AbilityStates AS in System.Enum.GetValues(typeof(AbilityStates))){
			abilitiesBaseDamage.Add(AS, 1);
			boosts.Add(AS, 0);
		}



	}

	void FixedUpdate ()
	{
		if (state == PlayerState.STOPPED)
			return;
		if (state == PlayerState.MOVING) {
			updatePosition ();
		} else if (state == PlayerState.TURNING) {
			updateRotation ();
		} else if (state == PlayerState.WAITING) {
			waitTime -= Time.deltaTime;
			if(waitTime <= 0)
				state = lastState;
		}

	}

	/// <summary>
	/// Updates the position of the player.
	/// </summary>
	private void updatePosition(){
		if (Vector3.Distance (transform.position, destPosition) <= 0.025f) {
			transform.position = destPosition;
			state = PlayerState.STOPPED;
			CollisionManager.collide(position);
			countdown = 0f;
			return;
		}
		countdown -= Time.deltaTime * speed;
		transform.position = Vector3.Lerp (startPosition, destPosition, 1f - countdown);
	}

	/// <summary>
	/// Updates the rotation of the player.
	/// </summary>
	private void updateRotation(){
		if (Quaternion.Angle (transform.rotation, destRotation) <= 0.1f) {
			transform.rotation = destRotation;
			state = PlayerState.STOPPED;
			countdown = 0f;
			return;
		}
		countdown -= Time.deltaTime * speed;
		transform.rotation = Quaternion.Lerp (startRotation, destRotation, 1f - countdown);
	}

	/// <summary>
	/// Gets the current position.
	/// </summary>
	/// <returns>The position.</returns>
	public Vector2 getPosition(){
		return position;
	}

	/// <summary>
	/// Sets the position.
	/// Don't use this to move the character. This method just set the coord position
	/// of the player in the room without moving it.
	/// </summary>
	/// <param name="pos">Position.</param>
	public void setPosition(Vector2 pos){
		position = pos;
	}

	/// <summary>
	/// Sets the position.
	/// Don't use this to move the character. This method just set the coord position
	/// of the player in the room without moving it..
	/// </summary>
	/// <param name="i">X Coord.</param>
	/// <param name="j">Y Coord</param>
	public void setPosition(int i, int j){
		position.x = i;
		position.y = j;
	}

	/// <summary>
	/// Move this instance one coord ahead smoothly.
	/// </summary>

	private bool toggle = true;
	private AudioSource source;
	public AudioClip paso1, paso2;

	public void move(){
		if (state == PlayerState.NO_STAFF) {
			noStaffAlert();
		}
		if (state != PlayerState.STOPPED || !ableToMove)
			return;

		Vector2 dest = position + direction;
		//CollisionManager.collide(dest);
		if (!canMove(dest)) {

            GamePad.SetVibration((PlayerIndex)0, 0.2f, 0.2f);
            movementRestriction.playCrashSound(dest);

            GamePad.SetVibration((PlayerIndex)0, 0.0f, 0.0f);
            return;
		}


		position += direction;
		startPosition = transform.position;
		destPosition = Room.GetInstance ().getWorldPosition (getPosition());
		countdown = 1f;
		state = PlayerState.MOVING;

		if (toggle)
			source.PlayOneShot (paso1);
		else
			source.PlayOneShot (paso2);

		toggle = !toggle;
	}

	/// <summary>
	/// Turns to the right smoothly.
	/// </summary>
	public void turnRight(){
		if (state == PlayerState.NO_STAFF) {
			noStaffAlert();
		}
		if (state != PlayerState.STOPPED || !ableToMove) {
			return;
		}
		makeTurningRightSound ();
		currentDir = (currentDir + 1) % 4;
		rotate (90f);
	}

	/// <summary>
	/// Turns to the left smoothly.
	/// </summary>
	public void turnLeft(){
		if (state == PlayerState.NO_STAFF) {
			noStaffAlert();
		}
		if (state != PlayerState.STOPPED || !ableToMove) {
			return;
		}
		makeTurningLeftSound ();
		currentDir--;
		currentDir = currentDir >= 0 ? currentDir : 3;
		rotate (-90f);
	}

	/// <summary>
	/// Rotate the character the specified degrees
	/// </summary>
	/// <param name="degrees">Degrees.</param>
	private void rotate(float degrees){
		
		startRotation = transform.rotation;
		destRotation = transform.rotation * Quaternion.Euler (Vector3.up * degrees);
		countdown = 1f;
		state = PlayerState.TURNING;
	}

	/// <summary>
	/// True if the player can move one coord ahead.
	/// </summary>
	/// <returns><c>true</c>, if the movement could be done, <c>false</c> otherwise.</returns>
	private bool canMove(Vector2 pos){
		return movementRestriction.canMove (pos);
	}

	public void removeHP(int dmg){
		this.hp -= dmg;
        if (this.hp < 0)
        {
            this.setHP(0);
        }
	}

    public void setHP(int hp)
    {
        this.hp = hp;
    }

	public int getHP(){
		return this.hp;
	}

    public void setMaxHP(int maxHp)
    {
        this.maxHp = maxHp;
    }

    public int getMaxHP()
    {
        return this.maxHp;
    }

    public void toggleMovementBlock(){
		ableToMove = !ableToMove;
	}

	public void enableMovement(){
		ableToMove = true;
	}

	public void disableMovement(){
		ableToMove = false;
	}

	public int getBaseDamage(AbilityStates ability){
		return abilitiesBaseDamage[ability];
	}

	public void setBaseDamage(AbilityStates ability, int dmg){
		abilitiesBaseDamage[ability] = dmg;
	}

	public int getBoost(AbilityStates ability){
		return boosts[ability];
	}

	public void addBoost(AbilityStates ability, int amount = 1){
		boosts[ability] += amount;
		abilitiesBaseDamage[ability] += boosts[ability] / boostsToLevelUp;
		boosts[ability] = boosts[ability] % boostsToLevelUp;
	}
	
	private void makeTurningSound(){
		SoundManager.instance.PlaySingle("turning");
	}

	private void makeTurningLeftSound(){
		SoundManager.instance.PlayDirectionalSingle("turning", -1.0f)
	}
	
	private void makeTurningRightSound(){
		SoundManager.instance.PlayDirectionalSingle("turning", 1.0f)
	}

	private void noStaffAlert(){
		wait (1);
		SoundManager.instance.PlaySingle("incorrect");
		Debug.Log("Debes sostener el baston antes de continuar");
	}

	public void pickStaff(){
		state = PlayerState.STOPPED;
		wait (1);
		SoundManager.instance.PlaySingle("correct");
		Debug.Log("Staff picked");
	}

	public Orientation getOrientation(){
		switch (currentDir) {
		case 0:
			return Orientation.NORTH;
		case 1:
			return Orientation.EAST;
		case 2:
			return Orientation.SOUTH;
		default:
			return Orientation.WEST;
		}
	}

	public void wait(float seconds){
		waitTime = seconds;
		if (state == PlayerState.WAITING)
			return;
		lastState = state;
		state = PlayerState.WAITING;

	}

	public void askAhead(){
		Vector2 dest = position + direction;
		Room.GetInstance ().ask (dest);
		wait (0.5f);
	}

	public void askRight(){
		int rightDir = (currentDir + 1) % 4;
		Vector2 dest = position + directions [rightDir];
		Room.GetInstance ().ask (dest);
		wait (0.5f);
	}

	public void askLeft(){
		int leftDir = (currentDir-1) >= 0 ? (currentDir-1) : 3;
		Vector2 dest = position + directions [leftDir];
		Room.GetInstance ().ask (dest);
		wait (0.5f);
	}
}

