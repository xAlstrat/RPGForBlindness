using UnityEngine;
using System.Collections;

public class PersistenceTestController : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (Input.GetKeyDown (KeyCode.Return)) {
			SceneLoader loader = SceneLoader.GetInstance();
			loader.load(loader.persistentScenes[ Random.Range(0, 3)]);
		}
	}
}

