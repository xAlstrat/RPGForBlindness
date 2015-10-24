using UnityEngine;
using System.Collections;

public class PlayerMovementRestriction : MonoBehaviour
{
	private LevelData data;
	private AudioSource source;

	private bool toggle = true;
	public AudioClip choque;
	public AudioClip treasure;
	public AudioClip door;
	public AudioClip geometric;

	private float lastPress = 1f;
	private float delay = 1f;
	
	void Start(){
		this.source = GetComponent<AudioSource>();
	}

	public bool canMove(Vector2 pos){
		return Room.GetInstance (). walkableCell(pos);
	}

	public void playCrashSound(Vector2 pos){
		if (Time.time - lastPress > delay) {
			lastPress = Time.time;
			GameObject go = Room.GetInstance ().getEntityAt (pos);
			if (go == null) {
				source.PlayOneShot(choque);
			} else {
				go.GetComponent<AudioSource> ().Play ();
			}
		}
	}


}

