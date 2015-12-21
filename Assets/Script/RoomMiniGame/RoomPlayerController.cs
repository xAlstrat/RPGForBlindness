using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class RoomPlayerController : MonoBehaviour {
	
	public float speed;
	private bool started;
	private Vector3 originalposition;
	
	private Rigidbody rb;
	
	PlayerIndex playerIndex = 0;
	GamePadState state;
	GamePadState prevState;

	private RoomMasterController master_room_controller;
	
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		started = false;
		originalposition = transform.position;
		master_room_controller = GameObject.Find ("RoomFloor").GetComponent<RoomMasterController> ();
	}
	
	void FixedUpdate () {
		// Empieza el juego si no ha iniciado
		if ((started == false) && master_room_controller.GameStartButtonIsPressed ()) {
			StartGame ();
		} else {
			master_room_controller.PlayerMovement(gameObject, speed);
			// Evitar que se siga moviendo
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;
		}
	}
	
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("GeomWall"))
		{
			GamePad.SetVibration((PlayerIndex)0, 0.1f, 0.1f);
		}
	}
	
	void OnCollisionStay(Collision other)
	{
		if (other.gameObject.CompareTag("GeomWall"))
		{
			GamePad.SetVibration((PlayerIndex)0, 0.1f, 0.1f);
		}
	}
	
	void OnCollisionExit(Collision other)
	{
		if (other.gameObject.CompareTag("GeomWall"))
		{
			GamePad.SetVibration((PlayerIndex)0, 0.0f, 0.0f);
		}
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Endblock"))
		{
			Debug.Log("trigered");
			if (started) {
				other.gameObject.SetActive (false);
				Game.GetInstance ().player.increaseOneGeomLevel ();
				SceneLoader.GetInstance().load("HallStateIncomplete");
			}
		}
		
	}
	
	void StartGame ()
	{
		// Eliminar las fuerzas
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;
		// Devolvemos al jugador al punto inicial
		transform.position = originalposition;
		
		
		// En otros objetos:
		// Seteamos la posicion de la camara en (0,6.9,-12.4);
		// Seteamos la posicion de la camara
		// Elegimos uno de los 3 setup
		
		// Declaramos que el juego ha comenzado
		started = true;
		
	}
}
