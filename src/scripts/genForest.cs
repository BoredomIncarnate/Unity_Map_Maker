﻿using UnityEngine;
using System.Collections;



public class genForest : ScriptableObject {

	public GameObject f_prefab;
	public int radius;
	public int depth;
	public double density;
	public ArrayList trees = new ArrayList();

	void Start () {
		forest f = new forest (radius, density, new point(0, 0, 0));
		point[] arrForest = f.CreateForest ();
		species aSpec = new species ();
		aSpec.generate ();
		foreach (point tree in arrForest) {
			Debug.Log (string.Format("Tree x: {0}, y: {1}", tree.x, tree.y));
			aSpec.generate();
			tree t = new tree(f_prefab);
			t.genTree(new Vector3(tree.x, 0, tree.y), aSpec.depth, aSpec.length, aSpec.maxHeight, Mathf.PI / 2);
			viewableTree v = new viewableTree(t.points);
			trees.Add(v);
		}
		foreach (viewableTree v in trees) {
			Debug.Log (string.Format("Viewable Tree start point: {0}", v.startPoint));
		}
	}
}
