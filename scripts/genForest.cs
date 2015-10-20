using UnityEngine;
using System.Collections;



public class genForest : ScriptableObject {

	public GameObject f_prefab;
	public int radius;
	public int depth;
	public double density;
	// Use this for initialization


	void Start () {
		Debug.Log ("I'm alive");
		forest f = new forest (radius, density, new point(0, 0, 0));
		point[] arrForest = f.CreateForest ();
		foreach (point tree in arrForest) {
			Debug.Log (string.Format("Tree x: {0}, y: {1}", tree.x, tree.y));
			tree t = new tree(f_prefab);
			t.genTree(new Vector3(tree.x, 0, tree.y), depth, 5, 20, Mathf.PI / 2);
		}
	}
}
