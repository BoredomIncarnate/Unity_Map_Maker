using UnityEngine;
using System.Collections;

public class tree : MonoBehaviour {

	private GameObject tree_prefab;
	private enum direction { up, left, right};
	private int currHeight = 0;
	private ArrayList _points = new ArrayList ();

	public tree(GameObject prefab) {
		tree_prefab = prefab;
	}

	public void genTree(Vector3 start, int depth, int length, int maxHeight, float theta) {
		if (depth > 0) {
			Vector3 s = buildPart (start, theta, length, depth);
			genTree (s, depth - 1, length - 1, maxHeight, Random.Range (0, Mathf.PI));
			genTree (s, depth - 1, length - 1, maxHeight, Random.Range (0, Mathf.PI));
			genTree (s, depth - 1, length - 1, maxHeight, Random.Range (0, Mathf.PI));
			genTree (s, depth - 1, length - 1, maxHeight, Random.Range (0, Mathf.PI));
		}
	}



	private Vector3 buildPart(Vector3 start, float theta, int length, int scale) {
		Vector3 end = Vector3.zero;
		tree_prefab.transform.localScale = new Vector3((float)scale, (float)scale, (float)scale);
		Instantiate (tree_prefab, start, Quaternion.identity);
		for (int i = 1; i < length; i++) {
			end = new Vector3( (findCos(theta) * i) + start.x, (findSin(theta) * i) + start.y, (findCos(theta) * (findSin(theta)) * i) + start.z);
			_points.Add(end);
		}
		return end;
	}

	public Vector3 startPoint {
		get {
			return _points[0];
		}
	}

	public ArrayList points {
		get {
			return _points;
		}
	}

	public void generate() {
		foreach (Vector3 i in _points) {
			Instantiate (tree_prefab, i , Quaternion.identity);
			break;
		}
	}

	private float findCos(float theta) {
		return Mathf.Cos (theta);
	}	

	private float findSin(float theta) {
		return Mathf.Sin (theta);
	}

	//viewableTree().startPoint -> vector3(0,0,0)
	//viewableTree().

}
