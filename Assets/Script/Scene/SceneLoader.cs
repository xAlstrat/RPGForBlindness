using UnityEngine;
using System.Collections;

/// <summary>
/// This class allows us to move through scenes preserving some gameobjects.
/// </summary>
public class SceneLoader : MonoBehaviour
{
	private static SceneLoader instance;
	private SceneFadeInOut fade;
	private string sceneToLoad;
	private bool loadingScene = false;
	private bool loadPersistent = false;

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
		fade = gameObject.GetComponent<SceneFadeInOut> ();
	}

	void FixedUpdate(){
		if (loadingScene && fade.isReady ()) {
			if(loadPersistent){
				hideAndShow(sceneToLoad);
				loadingScene = false;
				fade.setStarting();
				Application.LoadLevel(sceneToLoad);

			}
			else{
				destroyGameObjects ();
				Application.LoadLevel(sceneToLoad);
			}

		}
	}

	/// <summary>
	/// Loads the specified scene keeping persistent gameobjects.
	/// </summary>
	/// <param name="scene">Scene.</param>
	public void load (string scene){
		sceneToLoad = scene;
		loadingScene = true;
		loadPersistent = false;
		fade.setEnding ();
		fade.enable ();
		for(int i=0; i < persistentScenes.Length; i++){
			if(persistentScenes[i].Equals(scene)){
				loadPersistent = true;
				return;
			}
		}
	}

	/// <summary>
	/// Loads the specified scene deleting all persistent gameobjects.
	/// </summary>
	/// <param name="scene">Scene.</param>
	public void cleanLoad(string scene){
		sceneToLoad = scene;
		loadingScene = true;
		loadPersistent = false;
		fade.setEnding ();
		fade.enable ();
	}

	/// <summary>
	/// Destroies all the persistent gameobjects with this instance.
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

