using UnityEngine;
using System.Collections;

/// <summary>
/// A player controller.
/// 
/// Any player controller must inherit this class.
/// </summary>
public class PlayerController : MonoBehaviour
{
	protected Player player;

	// Use this for initialization
	public virtual void Start ()
	{
		player = gameObject.GetComponent<Game> ().player;
	}
	
	// Update is called once per frame
	public virtual void Update ()
	{

	}


}

