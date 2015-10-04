using UnityEngine;
using System.Collections;

public class Init : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		HallBuilder hallBuilder = new HallBuilder ();
		Hall hall = hallBuilder.build (LevelData.getLevel (1));
		HallMeshBuilder meshBuilder = gameObject.GetComponent<HallMeshBuilder> ();
		meshBuilder.setHall (hall);
		meshBuilder.process ();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

