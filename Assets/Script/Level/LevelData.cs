using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class LevelData
{

	private string[] hallData;
	private Vector2 startPosition;
	private Dictionary<string, Orientation> orientations;

	public LevelData(){
		orientations = new Dictionary<string, Orientation> ();
	}

	public Vector2 getStartPosition(){
		return startPosition;
	}

	public int getRoomWidth(){
		return hallData [0].Length;
	}

	public int getRoomHeight(){
		return hallData.Length;
	}

	public bool wallAt(int i, int j){
		return hallData[j][i].Equals('#');
	}

	public bool walkableCell(int i, int j){
		return hallData[j][i].Equals(' ');
	}

	public void addOrientation(int i, int j, Orientation o){
		orientations.Add (i + "," + j, o);
	}

	public Orientation getOrientationAt(int i, int j){
		return orientations [i + "," + j];
	}

	public Entity getEntityAt(int i, int j){
		switch (hallData[j][i]){
		case '#':
			return Entity.WALL;
		case ' ':
			return Entity.FLOOR;
		case 'T':
			return Entity.TREASURE;
		case 'X':
			return Entity.TRAP;
		case 'D':
			return Entity.DOOR;
		case 'M':
			return Entity.MONSTER;
		default:
			return Entity.WALL;
		}
	}

	public static LevelData getLevel(int n){
		LevelData levelData = new LevelData ();
		switch (n) {
		case 1:
			levelData.hallData = new string[]{
				"       #D##",
				" ##### # ##",
				" #####    D",
				" ##########",
				" ## M  ####",
				" ## ## ##X#",
				" ##T## ## #",
				" #####    #",
				" ##### ####",
				"       ####",
				"### #######",
				"### #######",
				"### #######"
			};
			levelData.startPosition = new Vector2(3, 12);
			/*Orientations*/
			//South
			levelData.addOrientation(8, 0, Orientation.SOUTH);
			levelData.addOrientation(9, 5, Orientation.SOUTH);
			//North
			levelData.addOrientation(3, 6, Orientation.NORTH);
			//West
			levelData.addOrientation(10, 2, Orientation.WEST);
			//East
			levelData.addOrientation(4, 4, Orientation.EAST);
			break;
		default:
			break;
		}
		return levelData;
	}

    public void removeEntity(int i, int j)
    {
        StringBuilder sb = new StringBuilder(hallData[j]);
        sb[i] = ' ';
        hallData[j]= sb.ToString();
    }
}

