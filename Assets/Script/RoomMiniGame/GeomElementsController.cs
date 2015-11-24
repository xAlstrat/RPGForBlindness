using UnityEngine;
using System.Collections;

public class GeomElementsController : MonoBehaviour {

	private Vector3 center;
	private Vector3 originalposition;
	// Restringe la rotacion, en reflexion
	public bool rotation_locked;
	// Restringe la traslacion, en reflexion
	public bool translation_locked;

	// Para saber desde que nivel aparece este elemento
	public int appears_from_level;

	// Controlador Maestro
	private RoomMasterController master_room_controller;

	// Use this for initialization
	void Start () {
		// Se guarda la posicion inicial del centro del cuarto geometrico
		center = GameObject.Find("RoomFloor").transform.position;

		originalposition = transform.position;

		master_room_controller = GameObject.Find ("RoomFloor").GetComponent<RoomMasterController> ();

		SetApparition (master_room_controller.GetGameGeomLevel ());
	}

	// Reflexion respecto al eje horizontal
	public void HorizontalReflect () {
		float distancez = originalposition.z - center.z;
		float newz = originalposition.z - (2 * distancez);
		// cambiar posicion
		if (rotation_locked == false) {
			transform.Rotate (0.0f, 90.0f, 0.0f);
		}
		if (translation_locked == false) {
			transform.position = new Vector3 (originalposition.x, originalposition.y, newz);
		}

	}

	// Reflexion respecto al eje Vertical
	public void VerticalReflect () {
		float distancex = originalposition.x - center.x;
		float newx = originalposition.x - (2 * distancex);
		// cambiar posicion
		if (rotation_locked == false) {
			transform.Rotate (0.0f, 90.0f, 0.0f);
		}
		if (translation_locked == false) {
			transform.position = new Vector3 (newx, originalposition.y, originalposition.z);
		}
	}

	// Aparece o desaparece el objeto, segun el nivel que se le entrega y el nivel que posee
	public void SetApparition(int actual_level) {
		if (actual_level >= appears_from_level) {
			gameObject.SetActive (true);
		} else {
			gameObject.SetActive (false);
		}
	}
}
