using UnityEngine;
using System.Collections;

public class NormalRenderer : MonoBehaviour {
	public Mesh mesh;
	private Vector3 origin;

	// Use this for initialization
	void Start () {
		mesh = gameObject.GetComponent<MeshFilter> ().mesh;
		origin = new Vector3 (0, 0, 0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Debug.DrawRay(origin, Vector3.up, Color.red, 1, false);
		for (int i=0; i<mesh.vertices.Length; i++) {
			Debug.DrawRay(mesh.vertices[i], mesh.normals[i], Color.red);	
		}
	}
}
