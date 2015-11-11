using UnityEngine;
using System.Collections;

public class GeomElementsController : MonoBehaviour {

	private Vector3 center;
	private Vector3 originalposition;
	// Restringe la rotacion, en reflexion
	public bool rotation_locked;
	// Restringe la traslacion, en reflexion
	public bool translation_locked;

	// Use this for initialization
	void Start () {
		// Se guarda la posicion inicial del centro del cuarto geometrico
		center = GameObject.Find("RoomFloor").transform.position;

		originalposition = transform.position;
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
}
