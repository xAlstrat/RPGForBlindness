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
		return !hallData [j] [i].Equals ('#');
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
		case '-':
			return Entity.SIGNAL_MULTI;
		case 'u':
			return Entity.SIGNAL_UP;
		case 'd':
			return Entity.SIGNAL_DOWN;
		case 'l':
			return Entity.SIGNAL_LEFT;
		case 'r':
			return Entity.SIGNAL_RIGHT;
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
				" #####  - D",
				" ##########",
				" ## M  ####",
				" ## ## ##X#",
				" ##T## ## #",
				" #####-   #",
				" ##### ####",
				" l - l ####",
				"### #######",
				"### #######",
				"###s#######"
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
			//levelData.addOrientation(3, 11, Orientation.SOUTH);

			break;
		case 2:
			levelData.hallData = new string[]{
				"   T###X###########",
				" ###### ###    ####",
				"M   ###-    ##-   #",
				"### ### ###### ## #",
				"#  -   - ##### ## #",
				"# ### ## #     ## #",
				"#-  # ##   ###### #",
				"# ###  T#########D#"
			};
			levelData.startPosition = new Vector2(1, 7);
			/*Orientations*/
			//South
			levelData.addOrientation(7, 0, Orientation.SOUTH);
			//North
			levelData.addOrientation(17, 7, Orientation.NORTH);
			//West
			levelData.addOrientation(3, 0, Orientation.WEST);
			levelData.addOrientation(7, 7, Orientation.WEST);
			//East
			levelData.addOrientation(0, 2, Orientation.EAST);
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

