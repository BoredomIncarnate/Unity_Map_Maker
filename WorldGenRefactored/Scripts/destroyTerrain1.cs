using UnityEngine;
using System.Collections;

public class destroyTerrain1 : MonoBehaviour {

	// Update is called once per frame
	void Update () {
	    foreach(GameObject c in GameObject.FindGameObjectsWithTag("toDestroy"))
            DestroyObject(c);
	}
}
