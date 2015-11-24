using UnityEngine;
using System.Collections;

using XInputDotNetPure;

public class RoomMasterController : MonoBehaviour {
	PlayerIndex playerIndex = 0;
	GamePadState state;
	GamePadState prevState;

	// Para saber si ha iniciado o no el juego
	private bool game_started;

	// Para testear niveles
	public int test_level;

	// Use this for initialization
	void Start () {
		game_started = false;
		state = GamePad.GetState(playerIndex);
		prevState = state;
		SoundManager.instance.PlayMusic("Hidden Agenda");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// Funcion para ver si el boton de inicio ha sido presionado
	public bool GameStartButtonIsPressed () {
		bool return_value = false;
		// Input de Teclado
		if (Input.GetKeyDown (KeyCode.Space) == true) {
			return_value = true;
		}
		// Input de Joystick
		prevState = state;
		state = GamePad.GetState(playerIndex);
		if ((prevState.Buttons.Start == ButtonState.Released) && (state.Buttons.Start == ButtonState.Pressed)) {
			return_value = true;
		}

		// Comenzar el juego si no ha comenzado
		if ((game_started == false) && return_value) {
			StartGame ();
		}

		// Default
		return return_value;
	}

	// Que hacer si el juego inicia
	private void StartGame() {
		game_started = true;
	}

	// Funcion para saber si el juego ha comenzado
	public bool isGameStarted () {
		return game_started;
	}

	// Funcion para obtener el nivel del cuarto geometrico, utilizado por las murallas para saber cuando aparecer
	public int GetGameGeomLevel () {
		if (test_level != -1) {
			return test_level;
		}

		// Default
		return 0;
	}

}
