using UnityEngine;
using System.Collections;

public class tree : MonoBehaviour {

	private GameObject tree_prefab;
	private enum direction { up, left, right};
	private int currHeight = 0;

	public tree(GameObject prefab) {
		tree_prefab = prefab;
	}

	public void genTree(Vector3 start, int depth, int length, int maxHeight, float theta) {
		if (depth > 0) {
			Debug.Log ("Depth = " + depth);
			Vector3 s = buildPart(start,theta, length);
			genTree(s, depth - 1, length -1, maxHeight, Random.Range(0, Mathf.PI));
			genTree(s, depth - 1, length -1, maxHeight, Random.Range(0, Mathf.PI));
			genTree(s, depth - 1, length -1, maxHeight, Random.Range(0, Mathf.PI));
		}
	}
	private Vector3 buildPart(Vector3 start, float theta, int length) {
		Vector3 end = Vector3.zero;
		Debug.Log ("Start X = " + start.x);
		Debug.Log ("Start Y = " + start.y);
		Instantiate (tree_prefab, start, Quaternion.identity);
		for (int i = 1; i < length; i++) {
			end = new Vector3( (findCos(theta) * i) + start.x, (findSin(theta) * i) + start.y, start.z);
			Instantiate (tree_prefab, end , Quaternion.identity);
		}
		Debug.Log ("End X = " + end.x);
		Debug.Log ("End Y = " + end.y);
		return end;
	}

	private float findCos(float theta) {
		return Mathf.Cos (theta);
	}	

	private float findSin(float theta) {
		return Mathf.Sin (theta);
	}

}
