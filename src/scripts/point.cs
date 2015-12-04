using UnityEngine;
using System.Collections;

public class point {
	private int _x;
	private int _y;
	private int _z;

	public point(int a, int b, int c) {
		this._x = a;
		this._y = b;
		this._z = c;
	}

	public int x {
		set { this._x = value; }
		get { return this._x; }
	}

	public int y {
		set { this._y = value; }
		get { return this._y; }
	}

	public int z {
		set { this._z = value; }
		get { return this._z; }
	}

    public override bool Equals(System.Object obj)
    {
        point p = (point)obj;
        return _x == p.x && _y == p.y && _z == p.z;
    }

    public override int GetHashCode()
    {
        return _x + _y + _z;
    }
}
