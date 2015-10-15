using UnityEngine;
using System.Collections;

public class GenerateWorld : MonoBehaviour {

    public int MaxHeight, ChunckSize, Zoom, WaterZoom;
    public float waterLevel;
    public GameObject Land, Water;


	// Use this for initialization
	void Start () {
        waterLevel = Mathf.Clamp01(waterLevel);
        float[,] t = createTerrain();
        float[,] w = createWater(t);
        RenderTerrain(t,w);

	}

    private void RenderTerrain(float[,] h, float[,] w)
    {
        for (int i = 0; i < h.GetLength(0); i++)
            for (int j = 0; j < h.GetLength(1); j++)
                for (int k = 0; k < h[i, j] * MaxHeight+1; k++)
                {
                    if (k >= (int)(h[i, j] * MaxHeight) -2 )
                    {
                        if (w[i, j] < waterLevel)
                            Instantiate(Land, new Vector3(i, k, j), Quaternion.identity);
                        else
                            Instantiate(Water, new Vector3(i, k, j), Quaternion.identity);
                    }
                }
    }


    private float[,] createWater(float[,] t)
    {
        Generatable water = new WaterGen(t,WaterZoom);
        return water.Generate();
    }

    private float[,] createTerrain()
    {
        Generatable terrain = new TerrainGen(ChunckSize,Zoom);
        return terrain.Generate();
    }
	
}
