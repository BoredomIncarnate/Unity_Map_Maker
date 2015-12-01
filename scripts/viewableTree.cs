using UnityEngine;
using System.Collections;

public class viewableTree {

	private Vector3 _startPoint = new Vector3 (0, 0, 0);
	private ArrayList _points = new ArrayList();

	public viewableTree(ArrayList points) {
		this._points = points;
		this._startPoint = (Vector3)points[0];
	}

	public ArrayList points {
		get {
			return this._points;
		}
	}

	public Vector3 startPoint {
		get {
			return this._startPoint;
		}
	}
	
}
