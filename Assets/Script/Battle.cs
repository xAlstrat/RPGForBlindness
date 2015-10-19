using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Timers;

public class Battle : MonoBehaviour {

	private int countR = 0;
	private int countL = 0;
	private int countU = 0;
	
	private int playerHP = 5;
	private int timeout = 3000;  //milliseconds
	private System.Timers.Timer myTimer;

	public Text winText;
	public Text hpText;
	public Text flechasText;

	public AudioClip explosion;
	public AudioClip agua;
	public AudioClip trueno;

	private AudioSource source;
	private float lowPitchRange = .75F;
	private float highPitchRange = 1.5F;
	private float velToVol = .2F;
	private float velocityClipSplit = 10F;

	public void myEvent(object source, ElapsedEventArgs e) {
		playerHP--;
	}
	
	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource>();
		flechasText.fontSize = 30;
		flechasText.color = Color.cyan;
		winText.fontSize = 40;
		winText.color = Color.cyan;
		
		myTimer = new System.Timers.Timer();
		myTimer.Elapsed += new ElapsedEventHandler(myEvent);
		myTimer.Interval = timeout;    
		myTimer.Enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
		//winText.text = "WIN";
		//Secuencia para ganar R, R, L, U
		
		hpText.text = "HP: " + playerHP.ToString();

		if (rightEvent ()) {
			//winText.text = "WIN";
			if(countR == 0 || countR == 1){
				flechasText.text += "Der ";
				makeExplosionSound();
				countR++;
			}
			else{
				//TODO imprimir en pantalla
				initCounts();
			}
		} else if (leftEvent ()) {
			if(countR == 2){
				makeAguaSound();
				flechasText.text += "Izq";
				countL++;
			}
			else{
				//TODO imprimir en pantalla
				initCounts();
			}
		} else if (aheadEvent ()) {
			if(countL == 1 && countR == 2){
				makeTruenoSound();
				flechasText.text += "Arriba";
				myTimer.Stop();
				winText.text = "WIN";
				initCounts();
			}
			else{
				//TODO imprimir en pantalla
				initCounts();
			}
		}
		
		if (playerHP <= 0) {
			myTimer.Stop();
			hpText.text = "";
			winText.text = "You lose";
		}
	}

	private void makeExplosionSound ()
	{
		source.PlayOneShot(explosion, 1);
	}

	private void makeAguaSound ()
	{
		source.PlayOneShot(agua);
	}

	private void makeTruenoSound ()
	{
		source.PlayOneShot(trueno, 1);
	}

	private void initCounts(){
		countR = 0;
		countL = 0;
		countU = 0;
		flechasText.text = "";
	}

	protected bool leftEvent(){
		return Input.GetKeyUp (KeyCode.LeftArrow);
	}
	
	protected bool rightEvent(){
		return Input.GetKeyUp (KeyCode.RightArrow);
	}
	
	protected bool aheadEvent(){
		return Input.GetKeyUp (KeyCode.UpArrow);
	}
}
