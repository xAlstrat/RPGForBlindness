using UnityEngine;
using System.Collections;

public class MenuEvent : MonoBehaviour {

	public void goMainMenu(){
		//SceneLoader.GetInstance ().load ("Welcome");
		Application.LoadLevel("Welcome");
	}

	public void goLoadMenu(){
		//SceneLoader.GetInstance ().load ("Load");
		Application.LoadLevel("Load");
	}

	public void startNewGame(){
		ApplicationData.reset ();
		ApplicationData.save ();
		//SceneLoader.GetInstance ().load ("HallState");
		Application.LoadLevel("HallState");
	}

	public void startLoadedGame(){
		//SceneLoader.GetInstance ().load ("HallState");
		Application.LoadLevel("HallState");
	}
}
