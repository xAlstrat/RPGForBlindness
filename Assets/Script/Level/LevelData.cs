using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class LevelData
{

	private string[] hallData;
	private Vector2 startPosition;
	private Dictionary<string, Orientation> orientations;
	private Dictionary<string, string> sounds;
    private ArrayList monsters;

	public LevelData(){
		orientations = new Dictionary<string, Orientation> ();
        monsters = new ArrayList();
		sounds = new Dictionary<string, string> ();
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

	public void addSound(int i, int j, string sound){
		sounds.Add (i + "," + j, sound);
	}

	public string getSound(int i, int j){
		return sounds [i + "," + j];
	}

	public Orientation getOrientationAt(int i, int j){
		return orientations [i + "," + j];
	}

    public void addMonster(int i, int j)
    {
        monsters.Add(new Vector2(i, j));
    }

    public void removeMonster(Vector2 pos)
    {
        foreach (Vector2 v in monsters)
        {
            if (v.Equals(pos))
            {
                monsters.Remove(v);
                break;
            }
        }
    }

    public void removeMonster(int i, int j)
    {
        removeMonster(new Vector2(i, j)); 
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
		case 'W':
			return Entity.GEOMETRIC;
		case 'h':
			return Entity.HELP;
		default:
			return Entity.WALL;
		}
	}

	public static LevelData getLevel(int n){
		LevelData levelData = new LevelData ();
		switch (n) {
		case 1:
			levelData.hallData = new string[]{
				"############D##",
				"#h     r   h ##",
				"# #############",
				"# #############",
				"#u#############",
				"#-T############",
				"# #############",
				"#h#############",
				"# #############",
			};
			levelData.startPosition = new Vector2(1, 8);
			/*Orientations*/
			//Doors
			levelData.addOrientation(12, 0, Orientation.SOUTH);
			//Treasures
			levelData.addOrientation(2, 5, Orientation.WEST);
			//helpsounds
			levelData.addSound(1, 7, "bienvenido");
            levelData.addSound(1, 1, "ayuda_tesoro");
            levelData.addSound(11, 1, "fin_nivel");
            break;
		case 2:
			levelData.hallData = new string[]{
				"############D##",
				"#X -         ##",
				"### ###########",
				"### ###########",
				"###u###########",
				"##T-  #########",
				"# ###M#########",
				"#- h  #########",
				"# #############",
			};
			levelData.startPosition = new Vector2(1, 8);
			/*Orientations*/
			//Doors
			levelData.addOrientation(12, 0, Orientation.SOUTH);
			//Treasures
			levelData.addOrientation(2, 5, Orientation.EAST);
			//Traps
			levelData.addOrientation(1, 1, Orientation.EAST);
			//Monsters
			levelData.addOrientation(5, 6, Orientation.SOUTH);
            levelData.addMonster(5, 6);
            levelData.addSound(3, 7, "advertencia_monster");   
			break;
		case 3:
			levelData.hallData = new string[]{
				"############D##",
				"############ ##",
				"############ ##",
				"# M-     T## ##",
				"#u# ######## ##",
				"# #   ###   W##",
				"# ###M###u#####",
				"#- r -    #####",
				"# #############",
			};
			levelData.startPosition = new Vector2(1, 8);
			/*Orientations*/
			//Doors
			levelData.addOrientation(12, 0, Orientation.SOUTH);
			//Treasures
			levelData.addOrientation(9, 3, Orientation.WEST);
			//Monsters
			levelData.addOrientation(5, 6, Orientation.SOUTH);
            levelData.addMonster(5, 6);
            levelData.addOrientation(2, 3, Orientation.WEST);
            levelData.addMonster(2, 3);

            //Warps
            levelData.addOrientation(12, 5, Orientation.WEST);
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
    
    public float getMinDistanceMonster(Vector2 pos)
    {
        float minDistance = 10000000.0f;
        foreach(Vector2 v in monsters)
        {
            float aux = Vector2.Distance(v, pos);
            if (aux<minDistance)
            {
                minDistance = aux;
            }
        }

        return minDistance;
    } 
}

