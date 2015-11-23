using UnityEngine;
using System.Collections.Generic;

public class SoundMap {

	private Dictionary<AbilityStates, string> audioMapSelection;
	private Dictionary<AbilityStates, string> audioMapAttack;

	public SoundMap(){
	
		//faltan sonidos para tierra, naturaleza y arcano
		audioMapSelection = new Dictionary<AbilityStates, string>();
		audioMapAttack = new Dictionary<AbilityStates, string>();

		audioMapSelection.Add (AbilityStates.AGUA, "water_select");
		audioMapSelection.Add (AbilityStates.FUEGO, "fire_select");
		audioMapSelection.Add (AbilityStates.TIERRA, "earth_select");
		audioMapSelection.Add (AbilityStates.VIENTO, "wind_select");
		audioMapSelection.Add (AbilityStates.NATURALEZA,"nature_select");
		audioMapSelection.Add (AbilityStates.ARCANO, "arcane_select");

		audioMapAttack.Add (AbilityStates.AGUA, "water_attack");
		audioMapAttack.Add (AbilityStates.FUEGO, "fire_attack");
		audioMapAttack.Add (AbilityStates.TIERRA, "earth_attack");
		audioMapAttack.Add (AbilityStates.VIENTO, "wind_attack");
		audioMapAttack.Add (AbilityStates.NATURALEZA,"nature_attack");
		audioMapAttack.Add (AbilityStates.ARCANO, "arcane_attack");
	}

	public string getSelectionClip(AbilityStates ability){
		return audioMapSelection[ability];
	}

	public string getAttackClip(AbilityStates ability){
		if(audioMapAttack.ContainsKey(ability))
			return audioMapAttack[ability];
		else
			return "water_attack";
	}

}
