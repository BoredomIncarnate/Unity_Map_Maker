using UnityEngine;
using System.Collections;

public class WorldGenerator : MonoBehaviour {

    TerrainGenerationModiffied generator;
    
    //Public
    public int SeaLevel;
    public int MaxHeight;
    public int SnowCap;
    public int Zoom;
    public int NumberOfPasses;

    public GameObject Land, Sea, Snow;

    private Texture2D HeightMap;

    //Private

    private int SizeOfArray;

	// Use this for initialization
	void Start () 
    {
        init();
        HeightMap = generator.getHeigtMap();
        renderTerrain();
	}

    public void init()
    {
        generator = new TerrainGenerationModiffied(NumberOfPasses,Zoom,MaxHeight,SeaLevel,SnowCap);
        generator.setPrefabs(Land,Sea,Snow);
        SizeOfArray = (int)Mathf.Pow(2, NumberOfPasses) + 1;
    }

    public void renderTerrain()
    {
        GameObject Voxel;

        for (int i = 0; i < SizeOfArray; i++)
            for (int j = 0; j < SizeOfArray; j++)
            {
                
                int kl = (int)(HeightMap.GetPixel(i, j).grayscale * MaxHeight)+1;
                if (kl < SeaLevel)
                {
                    Voxel = Sea;
                    kl = SeaLevel;
                }
                else
                    Voxel = Land;
                for (int k = 0; k < kl ;k++ )
                {
                    if (k > SnowCap)
                        Voxel = Snow;
                    Instantiate(Voxel, new Vector3(i,k, j), Quaternion.identity);
                }
            }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
