using UnityEngine;
using System.Collections;

public class HallNode
{
	public enum Wall
	{
		LEFT, RIGHT, TOP, BOTTOM
	};

	private bool _left = false;
	private bool _right = false;
	private bool _top = false;
	private bool _bottom = false;

	public bool hasWall(HallNode.Wall wall){
		switch (wall) {
		case Wall.LEFT:
			return _left;
		case Wall.RIGHT:
			return _right;
		case Wall.TOP:
			return _top;
		case Wall.BOTTOM:
			return _bottom;
		default:
			return false;
		}
	}

	public void setWall(HallNode.Wall wall){
		switch (wall) {
		case Wall.LEFT:
			_left = true;
			break;
		case Wall.RIGHT:
			_right = true;
			break;
		case Wall.TOP:
			_top = true;
			break;
		case Wall.BOTTOM:
			_bottom = true;
			break;
		}
	}
}

