﻿using UnityEngine;
using System.Collections;

	public class SoundManager : MonoBehaviour 
	{
		public AudioSource efxSource;                   //Drag a reference to the audio source which will play the sound effects.
		public AudioSource musicSource;                 //Drag a reference to the audio source which will play the music.
		public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.             
		public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
		public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.
		

		void Awake ()
		{
			//Check if there is already an instance of SoundManager
			if (instance == null)
				//if not, set it to this.
				instance = this;
			//If instance already exists:
			else if (instance != this)
				//Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
				Destroy (gameObject);

			efxSource.playOnAwake = false;

			//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
			//DontDestroyOnLoad (gameObject);
		}
		
		
		//Used to play single sound clips.
		public void PlaySingle(AudioClip clip)
		{
			//Set the clip of our efxSource audio source to the clip passed in as a parameter.
			efxSource.clip = clip;
			efxSource.panStereo = 0.0f;
			efxSource.volume = 1.0f;
			//Play the clip.
			efxSource.Play ();
		}

		public void PlaySingleWithVolume(AudioClip clip, float volume)
		{
			//Set the clip of our efxSource audio source to the clip passed in as a parameter.
			efxSource.clip = clip;
			efxSource.panStereo = 0.0f;
			efxSource.volume = volume;
			//Play the clip.
			efxSource.Play ();
		}

		public void playDirectionalSingle(AudioClip clip, float pan)
		{
			efxSource.clip = clip;
			efxSource.panStereo = pan;
			efxSource.volume = 1.0f;
			//Play the clip.
			efxSource.Play ();
		}

		//Used to play single sound clips based on the string name of the clip.
		public void PlaySingle(string clipName){
			PlaySingle((AudioClip)Resources.Load("Sounds/" + clipName));			
		}

		public void PlaySingleWithVolume(string clipName, float volume){
			PlaySingleWithVolume((AudioClip)Resources.Load("Sounds/" + clipName), volume);			
		}

		public void PlayDirectionalSingle(string clipName, float pan){			
			playDirectionalSingle((AudioClip)Resources.Load("Sounds/" + clipName), pan);
		}

		public void StopSingle(){
			efxSource.Stop();
		}

		public bool isEfxPlaying(){
			return efxSource.isPlaying;
		}

		//Used to play single sound clips.
		public void PlayMusic(AudioClip clip)
		{
			//Set the clip of our efxSource audio source to the clip passed in as a parameter.
			musicSource.clip = clip;
				
				//Play the clip.
			musicSource.Play ();
		}

		//Used to play single sound clips.
		public void PlayMusic(string clipName)
		{
			PlayMusic((AudioClip)Resources.Load("Music/" + clipName));
		}

		public void StopMusic(){
			musicSource.Stop();
		}
		
		
		//RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
		public void RandomizeSfx (params AudioClip[] clips)
		{
			//Generate a random number between 0 and the length of our array of clips passed in.
			int randomIndex = Random.Range(0, clips.Length);
			
			//Choose a random pitch to play back our clip at between our high and low pitch ranges.
			float randomPitch = Random.Range(lowPitchRange, highPitchRange);
			
			//Set the pitch of the audio source to the randomly chosen pitch.
			efxSource.pitch = randomPitch;
			
			//Set the clip to the clip at our randomly chosen index.
			efxSource.clip = clips[randomIndex];
			
			//Play the clip.
			efxSource.Play();
		}
	}
