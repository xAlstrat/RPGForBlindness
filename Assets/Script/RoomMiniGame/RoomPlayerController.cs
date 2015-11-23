﻿using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class RoomPlayerController : MonoBehaviour {

	public float speed;
	private bool started;
	
	private Rigidbody rb;

    PlayerIndex playerIndex = 0;
    GamePadState state;
    GamePadState prevState;

    void Start ()
	{
		rb = GetComponent<Rigidbody>();
		started = false;
	}

	void FixedUpdate () {
        // Empieza el juego si no ha iniciado
        prevState = state;
        state = GamePad.GetState(playerIndex);
        if (started == false && (Input.GetKeyDown (KeyCode.Space) == true || (prevState.Buttons.Start == ButtonState.Pressed || state.Buttons.Start == ButtonState.Pressed))) {
			StartGame ();
		}
		// Movimiento
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		
		rb.AddForce (movement * speed);
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
		transform.position = new Vector3 (0.0f, 0.5f, 0.0f);


		// En otros objetos:
			// Seteamos la posicion de la camara en (0,6.9,-12.4);
			// Seteamos la posicion de la camara
			// Elegimos uno de los 3 setup

		// Declaramos que el juego ha comenzado
		started = true;

	}
}
