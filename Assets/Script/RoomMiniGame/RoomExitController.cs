using UnityEngine;
using System.Collections;

public class RoomExitController : MonoBehaviour {
	private Rigidbody rb;
	private bool started;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		started = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Empieza el juego si no ha iniciado
		if (started == false && Input.GetKeyDown (KeyCode.Space) == true) {
			StartGame ();
		}
	}

	void StartGame () {
		rb.isKinematic = false;
	}
}
