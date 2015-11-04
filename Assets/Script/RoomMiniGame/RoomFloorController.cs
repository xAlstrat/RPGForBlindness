using UnityEngine;
using System.Collections;

public class RoomFloorController : MonoBehaviour {
	private bool started;
	// Use this for initialization
	void Start () {
		started = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Empieza el juego si no ha iniciado
		if (started == false && Input.GetKeyDown (KeyCode.Space) == true) {
			StartGame ();
		}
	}

	void StartGame ()
	{
		// Rotar el plano
		transform.Rotate (0.0f, 180.0f, 0.0f);
		started = true;
	}
}
