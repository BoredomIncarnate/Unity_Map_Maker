using UnityEngine;
using System.Collections;


public class forest {
	private int r;
	private double density;
	private point start;
	private point[] trees;
	private Texture2D _map;

	public forest(int s, double d, point startPoint, Texture2D map) {
		start = startPoint;
		this.r = s;
		this.density = d;
		this._map = map;

	}

	public point[] CreateForest() {
		SetPoints ();
		//_map.GetPixel (0, 0).r = 255;
		return trees;
	}

	public Texture2D map {
		get { 
			return _map;
		}
	}

	private void SetPoints() {
		density = density * r; //tmp way of getting number of trees
        int arrSize = (int)density;
        trees = new point[arrSize];
		for (int i = 0; i < trees.Length; i++) {
			trees[i] = CreatePoint(Random.Range(0, 2 * Mathf.PI));
            _map.SetPixel(trees[i].x, trees[i].y, new Color(.1f, _map.GetPixel(trees[i].x, trees[i].y).g, _map.GetPixel(trees[i].x, trees[i].y).b));
            
		}
	}

	private point CreatePoint(float theta) {
		point p = new point (GetX(theta), GetY(theta), 0);
		return p;
	}

	private int GetX(float theta) {
		int x = (int)((Mathf.Round(Mathf.Cos (theta))) + Random.Range(-r, r) + start.x);
		return x;
	}

	private int GetY(float theta) {
		int y = (int)((Mathf.Round (Mathf.Sin (theta))) + Random.Range (-r, r) + start.y);
		return y;
	}

}
