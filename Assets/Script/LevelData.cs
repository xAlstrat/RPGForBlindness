using UnityEngine;
using System.Collections;

public class LevelData
{
	private string[] hallData;
	private Vector2 startPosition;

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

	public static LevelData getLevel(int n){
		LevelData levelData = new LevelData ();
		switch (n) {
		case 1:
			levelData.hallData = new string[]{
				".......#.##",
				".#####.#.##",
				".#####.....",
				".##########",
				".##....####",
				".##.##.##.#",
				".##.##.##.#",
				".#####....#",
				".#####.####",
				".......####",
				"###.#######",
				"###.#######",
				"###.#######"
			};
			levelData.startPosition = new Vector2(3, 0);
			break;
		case 2:
			levelData.hallData = new string[]{
				"..",
				".#"
			};
			break;
		default:
			break;
		}
		return levelData;
	}
}

