using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuInit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Button button = GameObject.Find ("LoadButton").GetComponent<Button>();
		if(!ApplicationData.existSavedGame())
			button.interactable = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
