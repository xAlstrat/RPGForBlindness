using UnityEngine;
using System.Collections;

public class RoomScale
{
	private float _hallWidth;
	private float _hallHeight;

	public RoomScale(float hallWidth, float hallHeight){
		_hallWidth = hallWidth;
		_hallHeight = hallHeight;
	}

	public float getHallWidth(){
		return _hallWidth;
	}

	public float getHallHeight(){
		return _hallHeight;
	}
}

