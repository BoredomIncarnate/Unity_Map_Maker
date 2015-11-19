using UnityEngine;
using System.Collections;

public class DestroyTerrain : MonoBehaviour {

    
	// Update is called once per frame
	void Update () {
        if (gameObject.transform.position.x < GameObject.Find("Player").transform.position.x - GenerateWorld.renderDistance ||
            gameObject.transform.position.x > GameObject.Find("Player").transform.position.x + GenerateWorld.renderDistance ||
            gameObject.transform.position.z < GameObject.Find("Player").transform.position.z - GenerateWorld.renderDistance ||
            gameObject.transform.position.z > GameObject.Find("Player").transform.position.z + GenerateWorld.renderDistance)
            gameObject.tag = "toDestroy";
	}
}
