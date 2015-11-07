using UnityEngine;
using System.Collections;

public abstract class ConditionalSignalEntity : SignalEntity
{
	public string falseSource;
	protected abstract bool condition();

	protected void Awake(){
		base.Awake ();
		falseSource = "incorrect";
	}

	public override void handleCollision ()
	{
		if (condition())
			base.handleCollision ();
		else
			SoundManager.instance.PlaySingle (falseSource);
	}

}

