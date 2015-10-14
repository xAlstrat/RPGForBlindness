using UnityEngine;
using System.Collections;

public class RoomCameraController : MonoBehaviour {

	public GameObject player;

	private bool started;
	
	private Vector3 offset;
	private Vector3 offset2;
	
	void Start ()
	{
		started = false;
		offset = transform.position - player.transform.position;
		offset2 = new Vector3 (0.0f, 6.9f, -12.4f);
	}
	
	void LateUpdate ()
	{
		// Empieza el juego si no ha iniciado
		if (started == false && Input.GetKeyDown (KeyCode.Space) == true) {
			StartGame ();
		}
		transform.position = player.transform.position + offset;
	}

	void StartGame ()
	{
		// Seteamos la posicion de la camara en (0,6.9,-12.4);
		// transform.position = offset2;
		// offset = offset2;
		//transform.Rotate (-90.0f, -90.0f, 0);
		started = true;
	}
}
