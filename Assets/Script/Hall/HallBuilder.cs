using UnityEngine;
using System.Collections;

public class HallBuilder {
	private int hallWidth;
	private int hallHeight;

	public HallBuilder(){

	}

	public Hall build(LevelData data){
		hallWidth = data.getRoomWidth ();
		hallHeight = data.getRoomHeight ();
		Hall hall = new Hall (hallWidth, hallHeight);

		for (int i=0; i<hallWidth; i++) {
			for (int j=0; j<hallHeight; j++) {
				hall.setHallNode(i, j, generateHallNode(data, i, j));
			}
		}
		return hall;
	}

	private HallNode generateHallNode(LevelData data, int i, int j){
		HallNode node = new HallNode ();
		if (i - 1 < 0 || (data.wallAt (i - 1, j) && !data.wallAt (i, j)))
			node.setWall (HallNode.Wall.LEFT);
		if (i + 1 >= hallWidth || (data.wallAt (i + 1, j) && !data.wallAt (i, j)))
			node.setWall (HallNode.Wall.RIGHT);
		if (j - 1 < 0 || (data.wallAt (i, j - 1) && !data.wallAt (i, j)))
			node.setWall (HallNode.Wall.TOP);
		if (j + 1 >= hallHeight || (data.wallAt (i, j + 1) && !data.wallAt (i, j)))
			node.setWall (HallNode.Wall.BOTTOM);
		return node;
	}
}
