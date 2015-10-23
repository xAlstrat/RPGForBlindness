using UnityEngine;
using System.Collections;

/// <summary>
/// This class allows us to move through scenes preserving some gameobjects.
/// </summary>
public class SceneLoader : MonoBehaviour
{
	private static SceneLoader instance;

	public static SceneLoader GetInstance(){
		return instance;
	}

	public GameObject[] persistentGameobjects;
	public string[] persistentScenes;
	public Vector2[] showObjectInScene;
	
	protected void Awake(){
		instance = this;
		DontDestroyOnLoad (transform.gameObject);
		foreach (GameObject go in persistentGameobjects) {
			DontDestroyOnLoad (go);
		}
	}

	/// <summary>
	/// Load the specified scene.
	/// </summary>
	/// <param name="scene">Scene.</param>
	public void load (string scene){
		for(int i=0; i < persistentScenes.Length; i++){
			if(persistentScenes[i].Equals(scene)){
				hideAndShow(scene);
				Application.LoadLevel(scene);
				return;
			}
		}
		destroyGameObjects ();
	}

	/// <summary>
	/// Destroies all the persistent gameobjects.
	/// </summary>
	private void destroyGameObjects(){
		foreach (GameObject go in persistentGameobjects) {
			Destroy(go);
		}
		Destroy (this.gameObject);
	}

	private void hideAndShow(string scene){
		foreach (Vector2 v in showObjectInScene) {
			if(persistentScenes[(int)v.y].Equals(scene))
				persistentGameobjects[(int)v.x].SetActive(true);
			else
				persistentGameobjects[(int)v.x].SetActive(false);
		}
	}
}

