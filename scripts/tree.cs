using UnityEngine;
using System.Collections;

public class tree : MonoBehaviour {

	public Material material;
	public Mesh     mesh;
	private int     maxHeight;
	private float   scale         = 0.7f;
	private int     currentHeight;
	private bool    hasTrunk      = false;


	// Use this for initialization
	void Start () {
		maxHeight = 6;
		gameObject.AddComponent<MeshFilter>().mesh = mesh;
		gameObject.AddComponent<MeshRenderer>().material = material;
		if (currentHeight < maxHeight) {
			GenerateTrunk();
			if(currentHeight >= 3) {
				GenerateLeaves();
			}
		}

	}

	private void GenerateTrunk() {
		new GameObject("trunk").AddComponent<tree>().Init(this, Vector3.up);
	}

	private void GenerateLeaves() {
		new GameObject("leaf").AddComponent<tree>().Init(this, Vector3.left);
		new GameObject("leaf").AddComponent<tree>().Init(this, Vector3.right);
		new GameObject("leaf").AddComponent<tree>().Init(this, Vector3.forward);
		new GameObject("leaf").AddComponent<tree>().Init(this, Vector3.back);
	}
	
	private void Init(tree parent, Vector3 dir) {
		mesh = parent.mesh;
		material = parent.material;
		maxHeight = parent.maxHeight;
		currentHeight = parent.currentHeight + 1;
		transform.parent = parent.transform;
		transform.localPosition = dir * 1;
		Debug.Log("Depth -> " + currentHeight);
		Debug.Log("Parent -> " + parent.GetType ());
	}

}
