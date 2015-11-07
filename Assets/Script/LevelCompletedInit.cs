using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelCompletedInit : MonoBehaviour {

	private void nextLevel(){
		Application.LoadLevel("HallState");
	}

	// Use this for initialization
	void Start () {
		Text text = GameObject.Find("GratzText").GetComponent<Text>();
		text.text = "¡Has completado  el nivel "+(ApplicationData.getLastLevel()-1)+"!";
		Invoke("nextLevel", 5);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
