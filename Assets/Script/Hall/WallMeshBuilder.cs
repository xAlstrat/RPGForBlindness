using UnityEngine;
using System.Collections;

public class WallMeshBuilder
{

	public WallMeshBuilder(){

	}

	public GameObject generateWall(int width, int height){
		GameObject wall = new GameObject ();
		GameObject wallChild1 = new GameObject ("WallChild");
		GameObject wallChild2 = new GameObject ("WallChild");

		Transform parent = wall.transform;
		wallChild1.transform.parent = parent;
		wallChild2.transform.parent = parent;

		MeshRenderer renderer = wallChild1.AddComponent<MeshRenderer> () as MeshRenderer;
		MeshFilter filter = wallChild1.AddComponent<MeshFilter> () as MeshFilter;

		Mesh mesh = filter.mesh;
		mesh.vertices = generateVertices (width, height);
		mesh.normals = generateNormals (width, height);
		mesh.triangles = generateTriangles (width, height);
		mesh.uv = generateUV (mesh.vertices);

		renderer.material = Game.GetInstance ().roomMaterial.wallMaterial;

		MeshRenderer renderer2 = wallChild2.AddComponent<MeshRenderer> () as MeshRenderer;
		MeshFilter filter2 = wallChild2.AddComponent<MeshFilter> () as MeshFilter;
		renderer2.material = renderer.material;
		filter2.mesh = filter.mesh;

		wallChild2.transform.Rotate (new Vector3 (0f, 180f, 0f));

		return wall;
	}

	private Vector3[] generateVertices(int width, int height){
		Vector3[] vs = new Vector3[(width+1)*(height+1)];
		float xoffset = width / 2f;
		float yoffset = height / 2f;
		int[] triangles = new int[width * height * 2];
		for (int j=0; j<=height; j++) {
			for (int i=0; i<=width; i++) {
				int index = j*(width+1) + i;
				vs[index] = new Vector3(i - xoffset, j - yoffset, 0f);
			}
		}
		return vs;
	}

	private Vector3[] generateNormals(int width, int height){
		Vector3[] normals = new Vector3[(width+1)*(height+1)];
		int[] triangles = new int[width * height * 2];
		for (int j=0; j<=height; j++) {
			for (int i=0; i<=width; i++) {
				int index = j*(width+1) + i;
				normals[index] = new Vector3(0f, 0f, -1f);
			}
		}
		return normals;
	}

	private int[] generateTriangles(int width, int height){
		int _widthpo = width + 1;
		int _heightpo = height + 1;
		int[] triangles = new int[6 * width * height];
		int count = 0;
		for (int j=0; j<height; j++) {
			for(int i=0; i<width; i++){
				int index = j*_widthpo+i;
				triangles[count++] = index;
				triangles[count++] = index+_widthpo;
				triangles[count++] = index+_widthpo+1;
				triangles[count++] = index;
				triangles[count++] = index+_widthpo+1;
				triangles[count++] = index+1;
			}	
		}
		return triangles;
	}

	private Vector2[] generateUV(Vector3[] vertices){
		Vector2[] uvs = new Vector2[vertices.Length];
		for (int i=0; i < uvs.Length; i++) {
			uvs[i] = new Vector2(vertices[i].x, vertices[i].y);
		}
		return uvs;
	}
}

