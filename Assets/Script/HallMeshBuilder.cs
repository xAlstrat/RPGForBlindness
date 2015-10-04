using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HallMeshBuilder : MonoBehaviour
{
	public class WallData{
		public int beforeLenght = 0;
		public int afterLenght = 0;
		public Vector2 position;
		public HallNode.Wall wall;

		public WallData(int i, int j, HallNode.Wall wall){
			this.position = new Vector2(i , j);
			this.wall = wall;
		}
	}

	private Hall hall;



	public void setHall(Hall hall){
		this.hall = hall;
		hallWalls = new Dictionary<string, Dictionary<HallNode.Wall, WallData>> ();
	}

	private Dictionary<string, Dictionary<HallNode.Wall, WallData>> hallWalls;
	private List<WallData> walls = new List<WallData>();
	

	public void process(){
		for (int i=0; i<hall.width(); i++) {
			for (int j=0; j<hall.height(); j++) {
				generateWallData(i, j, new Vector2(1, 0), HallNode.Wall.TOP);
				generateWallData(i, j, new Vector2(1, 0), HallNode.Wall.BOTTOM);
				generateWallData(i, j, new Vector2(0, 1), HallNode.Wall.LEFT);
				generateWallData(i, j, new Vector2(0, 1), HallNode.Wall.RIGHT);
			}
		}
		generateHallMesh ();
	}

	private void generateWallData(int i, int j, Vector2 offset, HallNode.Wall wall){
		if(hall.hasWallAt(i, j, wall)){
			string key = i+","+j;
			string keyPos1 = (i+offset.x)+","+(j+offset.y);
			string keyPos2 = (i-offset.x)+","+(j-offset.y);
			if(hallWalls.ContainsKey(keyPos1) && hallWalls[keyPos1].ContainsKey(wall)){
				hallWalls[keyPos1][wall].beforeLenght++;
				if(hallWalls.ContainsKey(key)){
					hallWalls[key].Add(wall, hallWalls[keyPos1][wall]);
				}
				else{
					hallWalls.Add(key, new Dictionary<HallNode.Wall, WallData>());
					hallWalls[key].Add(wall, hallWalls[keyPos1][wall]);
					//walls.Add(hallWalls[key][wall]);
				}
					
			}
			else if(hallWalls.ContainsKey(keyPos2) && hallWalls[keyPos2].ContainsKey(wall)){
				hallWalls[keyPos2][wall].afterLenght++;
				if(hallWalls.ContainsKey(key)){
					hallWalls[key].Add(wall, hallWalls[keyPos2][wall]);
				}
				else{
					hallWalls.Add(key, new Dictionary<HallNode.Wall, WallData>());
					hallWalls[key].Add(wall, hallWalls[keyPos2][wall]);
					//walls.Add(hallWalls[key][wall]);
				}
			}
			else{
				WallData wallData = new WallData(i, j, wall);
				if(hallWalls.ContainsKey(key)){
					hallWalls[key].Add(wall, wallData);
				}
				else{
					Dictionary<HallNode.Wall, WallData> dic = new Dictionary<HallNode.Wall, WallData>();
					dic.Add(wall, wallData);
					hallWalls.Add(key, dic);
				}

				walls.Add(wallData);
			}
		}
	}

	private void generateHallMesh(){
		foreach (WallData data in walls) {
			generateWallMesh(data);
		}
	}

	private void generateWallMesh(WallData data){
		GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
		cube.name = "Wall (" + data.position.x + "," + data.position.y + ") " + data.wall.ToString ();
		int offset = data.beforeLenght + data.afterLenght + 1;

		cube.transform.position = calculatePosition(data);
		cube.transform.localScale = new Vector3 (offset, 1f, 0.1f);
		cube.transform.Rotate (calculateRotation(data));
	}

	private Vector3 calculatePosition(WallData data){
		float offset = ((data.afterLenght + data.beforeLenght + 1f) / 2) - data.beforeLenght - 0.5f;
		switch (data.wall) {
		case HallNode.Wall.TOP:
			return new Vector3 (data.position.x + offset, 0f, -data.position.y+0.5f);
		case HallNode.Wall.BOTTOM:
			return new Vector3 (data.position.x + offset, 0f, -data.position.y-0.5f);
		case HallNode.Wall.LEFT:
			return new Vector3 (data.position.x-0.5f, 0f, -data.position.y - offset);
		default:
			return new Vector3 (data.position.x+0.5f, 0f, -data.position.y - offset);
		}
	}

	private Vector3 calculateRotation(WallData data){
		return (data.wall == HallNode.Wall.LEFT || data.wall == HallNode.Wall.RIGHT) ?
			new Vector3 (0f, 90f, 0f) : Vector3.zero;
	}
}

