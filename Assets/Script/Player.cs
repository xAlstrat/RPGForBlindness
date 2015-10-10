using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	private Vector2 position;

	public void setPosition(Vector2 pos){
		position = pos;
	}

	public void setPosition(int i, int j){
		position.x = i;
		position.y = j;
	}

	void Update ()
	{
		
	}
}

