using UnityEngine;
using System.Collections;

public class TreasureEntity : SignalEntity
{
	protected void Awake(){
		source  = "item_pickup";
	}

	public override void handleCollision(){
		base.handleCollision ();
		Game.GetInstance ().player.wait (1.5f);
		generateTreasureRandomLootBoost ();

	}

	public override bool destroyable(){
		return true;
	}

	public override void ask ()
	{
		SoundManager.instance.PlaySingle ("tomar-tesoro");
	}

	public void generateTreasureRandomLootBoost() {
		AbilityState[] listAbilityPool = new AbilityState[]{
			AbilityState.AGUA,
			AbilityState.TIERRA,
			AbilityState.FUEGO,
			AbilityState.VIENTO,
			AbilityState.NATURALEZA,
			AbilityState.ARCANO
		};
		int randomBoost = Random.Range(0,6); // A number between 0 and 5
		Game.GetInstance ().player.addBoost (listAbilityPool[randomBoost], 4);
	}
}

