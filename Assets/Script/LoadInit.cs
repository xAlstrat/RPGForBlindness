using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadInit : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		Text text =  GameObject.Find ("LevelText").GetComponent<Text>();
		int level = ApplicationData.getLastLevel ();
		text.text = "Nivel " + level;
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

