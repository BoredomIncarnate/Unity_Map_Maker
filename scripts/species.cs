using UnityEngine;
using System.Collections;

public class species {

	private int _length, _depth, _maxHeight, _type;

	public species() {

	}

	public void generate() {
		this._type = (int)Random.Range (0, 2);
		switch (_type) {
			case 0:
				this._length = (int)Random.Range (4, 12);
				this._depth = (int)Random.Range (1, 8);
				break;
			case 1:
				this._length = (int)Random.Range (1, 5);
				this._depth = (int)Random.Range(2, 6);
				break;
		}
	}

	public int length {
		get { return this._length; }
	}

	public int depth {
		get { return this._depth; }
	}

	public int maxHeight {
		get { return this._maxHeight; }
	}


}
