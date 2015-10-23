using UnityEngine;
using System.Collections;

/// <summary>
/// Room.
/// 
/// Contiene la estructura e informacion de un nivel especifico.
/// </summary>
public class Room
{
	private static Room instance;

	public static Room GetInstance(){
		return instance;
	}

	/// <summary>
	/// The level information.
	/// </summary>
	private LevelData data;

	/// <summary>
	/// The hall structure.
	/// </summary>
	private Hall hall;

	/// <summary>
	/// The scale of the room.
	/// </summary>
	private RoomScale scale;

	public Room(RoomScale scale){
		instance = this;
		HallBuilder hallBuilder = new HallBuilder ();
		data = LevelData.getLevel (1);
		this.hall = hallBuilder.build (data);
		this.scale = scale;
		HallMeshBuilder meshBuilder = new HallMeshBuilder ();
		meshBuilder.setHall (hall);
		meshBuilder.setScale (scale);
		meshBuilder.process ();
		RoomObjectGenerator generator = new RoomObjectGenerator ();
	}

	/// <summary>
	/// Gets the room scale.
	/// </summary>
	/// <returns>The room scale.</returns>
	public float getRoomScale(){
		return scale.getHallWidth ();
	}

	/// <summary>
	/// Gets the player start position.
	/// </summary>
	/// <returns>The start position.</returns>
	public Vector2 getStartPosition(){
		return data.getStartPosition ();
	}

	/// <summary>
	/// Gets the world position of a given position inside the room.
	/// </summary>
	/// <returns>The world position.</returns>
	/// <param name="pos">Position.</param>
	public Vector3 getWorldPosition(Vector2 pos){
		return new Vector3 (
			pos.x * getRoomScale(),
			1f,
			(-hall.getHeight() + pos.y + 1f) * getRoomScale());
	}

	/// <summary>
	/// Gets the world position of a given positoin inside the room.
	/// </summary>
	/// <returns>The world position.</returns>
	/// <param name="pos">Position.</param>
	public Vector3 getWorldPosition(Vector3 pos){
		return new Vector3 (
			pos.x * getRoomScale(),
			pos.y,
			(-hall.getHeight() + pos.y + 1f) * getRoomScale());
	}

	/// <summary>
	/// Returns true if the room has a wall in the given position.
	/// </summary>
	/// <returns>The <see cref="System.Boolean"/>.</returns>
	/// <param name="pos">Position.</param>
	public bool wallAt(Vector2 pos){
		if (!isInside (pos)) {
			return true;
		}
		return data.wallAt ((int)pos.x, (int)(hall.getHeight() - pos.y - 1f));
	}

	/// <summary>
	/// Returns true if the given position is inside the limits of the room.
	/// </summary>
	/// <returns><c>true</c>, if inside was ised, <c>false</c> otherwise.</returns>
	/// <param name="pos">Position.</param>
	private bool isInside(Vector2 pos){
		return 0 <= pos.x && pos.x < hall.getWidth () &&
			0 <= pos.y && pos.y < hall.getHeight ();
	}

}

