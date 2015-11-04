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
		// Rotar el plano 60º
		// transform.Rotate (0.0f, 60.0f, 0.0f);
		GenerateTransformation ();
		started = true;
	}

	void GenerateTransformation()
	{
		// Elegir etre rotar, reflejar y trasladar
		int caseSwitch = Random.Range(0,3); // creates a number between 0 and 2
		switch (caseSwitch)
		{
		case 0:
			// Rotar
			// Izquierda o Derecha
			int direction_selector = Random.Range(0,2); // numero entre 0 y 1
			int direction;
			string direction_r_string;
			switch (direction_selector)
			{
			case 0:
				//Izquierda
				direction = 1;
				direction_r_string = "Derecha";
				break;
			default:
				//Derecha
				direction = -1;
				direction_r_string = "Izquierda";
				break;
			}
			// Grados
			float angle;
			int angle_selector = Random.Range (0,3); // numero entre 0 y 2
			// Transformacion
			switch (angle_selector)
			{
			case 0:
				angle = 45.0f;
				break;
			case 1:
				angle = 90.0f;
				break;
			default:
				angle = 135.0f;
				break;
			}
			// Rotar
			transform.Rotate (0.0f, angle * direction, 0.0f);
			Debug.LogError("El cuarto ha rotado hacia la " + direction_r_string + " " + angle.ToString() + " grados");
			break;
		case 1:
			// Trasladar
			// Horizontal o Vertical
			int axis_t_selector = Random.Range (0, 2); // 0 o 1
			int side_selector = Random.Range (0, 2); // 0 o 1
			Vector3 old_t_position = transform.position;
			float old_t_x = old_t_position.x;
			float old_t_y = old_t_position.y;
			float old_t_z = old_t_position.z;
			string axis_t_string;
			string side_t_string;

			switch (axis_t_selector)
			{
			case 0:
				// Horizontal
				axis_t_string = "Horizontalmente";
				switch (side_selector)
				{
				case 0:
					//Izquierda
					side_t_string = "la Izquierda 5 espacios";
					transform.position = new Vector3(old_t_x + 5, old_t_y, old_t_z);
					break;
				default:
					// Derecha
					side_t_string = "la Derecha 7 espacios";
					transform.position = new Vector3(old_t_x - 7, old_t_y, old_t_z);
					break;
				}
				break;
			default:
				// Vertical
				axis_t_string = "Verticalmente";
				switch (side_selector)
				{
				case 0:
					// Arriba
					side_t_string = "Arriba 3 espacios";
					transform.position = new Vector3(old_t_x, old_t_y, old_t_z - 3);
					break;
				default:
					// Abajo
					side_t_string = "Abajo 3 espacios";
					transform.position = new Vector3(old_t_x, old_t_y, old_t_z + 3);
					break;
				}
				break;
			}
			Debug.LogError("El cuarto se ha trasladado " + axis_t_string + " " + side_t_string);
			break;
		default:
			// Reflejar
			// Horizontal o Verticalmente
			int axis_selector = Random.Range (0, 2); // 0 o 1
			Vector3 old_position = GameObject.FindGameObjectWithTag("Endblock").transform.position;
			float old_x = old_position.x;
			float old_y = old_position.y;
			float old_z = old_position.z;
			switch (axis_selector)
			{
			case 0:
				// Vertical
				transform.Rotate (0.0f, 180.0f, 0.0f);
				GameObject.FindGameObjectWithTag("Endblock").transform.position = new Vector3( - old_x + 6, old_y, old_z);
				Debug.LogError("El cuarto ha sido reflejado segun el eje Y desde el centro del cuarto");
				break;
			default:
				// Horizontal
				GameObject.FindGameObjectWithTag("Endblock").transform.position = new Vector3(old_x, old_y, - old_z);
				Debug.LogError("El cuarto ha sido reflejado segun el eje X desde el centro del cuarto");
				break;
			}
			break;
		}
	}
}
