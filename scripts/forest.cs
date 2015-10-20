using UnityEngine;
using System.Collections;


public class forest {
	private int r;
	private double density;
	private point start;
	private point[] trees;

	public forest(int s, double d, point startPoint) {
		start = startPoint;
		this.r = s;
		this.density = d;
	}

	public point[] CreateForest() {
		SetPoints ();
		return trees;
	}

	private void SetPoints() {
		density = density * r; //tmp way of getting number of trees
		int arrSize = int.Parse (density.ToString ());
		trees = new point[arrSize];
		for (int i = 0; i < trees.Length; i++) {
			trees[i] = CreatePoint(Random.Range(0, 2 * Mathf.PI));
		}
	}

	private point CreatePoint(float theta) {
		point p = new point (GetX(theta), GetY(theta), 0);
		return p;
	}

	private int GetX(float theta) {
		int x = (int)((Mathf.Round(Mathf.Cos (theta))) + Random.Range(-r, r));
		return x;
	}

	private int GetY(float theta) {
		int y = (int)((Mathf.Round (Mathf.Sin (theta))) + Random.Range (-r, r));
		return y;
	}

}
