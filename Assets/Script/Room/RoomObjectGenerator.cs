using UnityEngine;
using System.Collections;

public class RoomObjectGenerator
{
	private LevelData data;
	private Transform parent;

	public RoomObjectGenerator(LevelData data){
		this.data = data;
		parent = GameObject.Find ("RoomObject").transform;
	}

	public void process(){
		int width = data.getRoomWidth ();
		int heght = data.getRoomHeight ();

		for (int i=0; i<width; i++) {
			for (int j=0; j<heght; j++) {
				instantiateRoomObject(i, j, data);
			}
		}
	}

	private void instantiateRoomObject(int i, int j, LevelData data){
		switch (data.getEntityAt (i, j)) {
		case Entity.TREASURE:
			instantiateTreasure(i , j);
			break;
		case Entity.MONSTER:
			instantiateMonster(i, j);
			break;
		case Entity.TRAP:
			instantiateTrap(i, j);
			break;
		case Entity.GEOMETRIC:
			instantiateGeometricRoom(i, j);
			break;
		case Entity.DOOR:
			instantiateDoor(i, j);
			break;
		default:
			break;
		}
	}

	private void instantiateTreasure(int i, int j){
		GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
		go.GetComponent<MeshRenderer>().material = Resources.Load("YellowMat") as Material;
		updateObjectTransform (go, i, j);

	}

	private void instantiateDoor(int i, int j){
		GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
		go.GetComponent<MeshRenderer>().material = Resources.Load("BrownMat") as Material;
		go.transform.parent = parent;
		go.transform.localScale = new Vector3 (0.8f, 2.3f, 0.1f);
		
		Orientation o = data.getOrientationAt (i, j);
		go.transform.Rotate (o.getRotation());
		go.transform.position = Room.GetInstance ().getWorldPosition (new Vector3(i, 0.15f, j));
	}

	private void instantiateMonster(int i, int j){
		GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
		go.GetComponent<MeshRenderer>().material = Resources.Load("BrownMat") as Material;
		updateObjectTransform (go, i, j);
	}

	private void instantiateTrap(int i, int j){
		GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
		go.GetComponent<MeshRenderer>().material = Resources.Load("BlackMat") as Material;
		updateObjectTransform (go, i, j);
	}

	private void instantiateGeometricRoom(int i, int j){
		GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
		go.GetComponent<MeshRenderer>().material = Resources.Load("BlueMat") as Material;
		updateObjectTransform (go, i, j);
	}

	private void updateObjectTransform(GameObject go, int i, int j){
		go.transform.parent = parent;
		go.transform.localScale = new Vector3 (0.8f, 0.3f, 0.5f);
		
		Orientation o = data.getOrientationAt (i, j);
		go.transform.Rotate (o.getRotation());
		go.transform.position = Room.GetInstance ().getWorldPosition (new Vector3(i, 0.15f, j));
	}
}

