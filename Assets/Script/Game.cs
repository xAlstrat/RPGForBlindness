using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour
{
	private static Game instance;

	public Player player;
	public MonsterEntity enemy;
	public RoomMaterial roomMaterial;
    public Text playerHP;
	private Room room;

	public static Game GetInstance(){
		return instance;
	}

	void Start ()
	{
		if (instance == null) {
			instance = this;
		}
		initRoom ();
		initPlayer ();
	}

	private void initRoom(){
		room = new Room (new RoomScale (2f, 2f));
	}

	private void initPlayer(){
		Room room = Room.GetInstance ();
		player.setPosition (room.getStartPosition());
        Debug.Log(player.getHP());
        player.setHP(100);
        playerHP.text = "HP: " + player.getHP();
		player.transform.position = room.getWorldPosition ( player.getPosition());
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
	}
}

