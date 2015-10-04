using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hall
{
	private HallNode[,] _nodes;
	private int _width;
	private int _height;

	public Hall(int width, int height){
		_nodes = new HallNode[width,height];
		_width = width;
		_height = height;
	}

	public void setHallNode(int i, int j, HallNode node){
		_nodes [i, j] = node;
	}

	public void setWallAt(int i, int j, HallNode.Wall wall){
		_nodes [i, j].setWall (wall);
	}

	public bool hasWallAt(int i, int j, HallNode.Wall wall){
		return _nodes [i, j].hasWall (wall);
	}

	public int width(){
		return _width;
	}

	public int height(){
		return _height;
	}
}

