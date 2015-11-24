using UnityEngine;
using System.Collections.Generic;

public class SoundMap {

	private Dictionary<AbilityState, string> audioMapSelection;
	private Dictionary<AbilityState, string> audioMapAttack;

	public SoundMap(){
	
		//faltan sonidos para tierra, naturaleza y arcano
		audioMapSelection = new Dictionary<AbilityState, string>();
		audioMapAttack = new Dictionary<AbilityState, string>();

		audioMapSelection.Add (AbilityState.AGUA, "water_select");
		audioMapSelection.Add (AbilityState.FUEGO, "fire_select");
		audioMapSelection.Add (AbilityState.TIERRA, "earth_select");
		audioMapSelection.Add (AbilityState.VIENTO, "wind_select");
		audioMapSelection.Add (AbilityState.NATURALEZA,"nature_select");
		audioMapSelection.Add (AbilityState.ARCANO, "arcane_select");

		audioMapAttack.Add (AbilityState.AGUA, "water_attack");
		audioMapAttack.Add (AbilityState.FUEGO, "fire_attack");
		audioMapAttack.Add (AbilityState.TIERRA, "earth_attack");
		audioMapAttack.Add (AbilityState.VIENTO, "wind_attack");
		audioMapAttack.Add (AbilityState.NATURALEZA,"nature_attack");
		audioMapAttack.Add (AbilityState.ARCANO, "arcane_attack");
	}

	public string getSelectionClip(AbilityState ability){
		return audioMapSelection[ability];
	}

	public string getAttackClip(AbilityState ability){
		if(audioMapAttack.ContainsKey(ability))
			return audioMapAttack[ability];
		else
			return "water_attack";
	}

}
