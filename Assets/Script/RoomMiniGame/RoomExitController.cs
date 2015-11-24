using UnityEngine;
using System.Collections;

public class RoomExitController : MonoBehaviour {
	private Rigidbody rb;
	private bool started;
	private RoomMasterController master_room_controller;

    // Use this for initialization
    void Start () {
		rb = GetComponent<Rigidbody>();
		started = false;
		master_room_controller = GameObject.Find ("RoomFloor").GetComponent<RoomMasterController> ();
	}
	
    void Update()
    {

        
    }
	// Update is called once per frame
	void FixedUpdate () {
		// Empieza el juego si no ha iniciado
		if ((started == false) && master_room_controller.GameStartButtonIsPressed ())
		{
			StartGame();
		}
	}

	void StartGame () {
		rb.isKinematic = false;
		started = true;
	}
}
