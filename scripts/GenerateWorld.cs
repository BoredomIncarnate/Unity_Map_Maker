using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class GenerateWorld : MonoBehaviour {

    public int MaxHeight, ChunckSize, Zoom, WaterZoom,depth;
    public static int renderDistance = 40;
    public float waterLevel, forestDensity;
    public GameObject Land, Water, TreeTrunk;
    private float[,] TerrainMap, WaterMap;
    private point[] treeLocations;
    private float prx = float.MaxValue, prz = float.MaxValue;
    private GameObject player;
    private Texture2D texture;
    Dictionary<point, tree> treeMap = new Dictionary<point,tree>();

    void Start()
    {
        player = GameObject.Find("Player");
        waterLevel = Mathf.Clamp01(waterLevel);
        TerrainMap = createTerrain();
        WaterMap = createWater(TerrainMap);
        generateTexureMap();
        forest f = new forest(ChunckSize / 2, forestDensity, new point(ChunckSize / 2,ChunckSize / 2,0), texture);
        f.CreateForest();
        texture = f.map;
        
    }

    public void generateTexureMap()
    {
        texture = new Texture2D(ChunckSize,ChunckSize);
        for(int i = 0;i < ChunckSize; i++)
            for(int j = 0;j < ChunckSize; j++)
                texture.SetPixel(i,j,new Color(0,TerrainMap[i,j],WaterMap[i,j]));
    }

    void Update()
    {
        if (!checkX() || !checkY())
        {
            RenderTerrain(TerrainMap, WaterMap,player.transform.position);
            prx = player.transform.position.x;
            prz = player.transform.position.z;
        }

    }

    bool checkX()
    {
        return prx < player.transform.position.x + 5 && 
            prx > player.transform.position.x - 5;
    }

    bool checkY()
    {
        return prz < player.transform.position.z + 5 &&
            prz > player.transform.position.z - 5;
    }	

    private void RenderTerrain(float[,] h, float[,] w,  Vector3 player)
    { 
        for (int i = (int)(player.x - renderDistance/2); i < (int)(player.x + renderDistance/2); i++)
            for (int j = (int)(player.z - renderDistance / 2); j < (int)(player.z + renderDistance / 2); j++)
            {
                try
                {
                    for (int k = 0; k < h[i, j] * MaxHeight + 1; k++)
                    {
                        if (k >= (int)(h[i, j] * MaxHeight) - 2)
                        {
                            if (w[i, j] < waterLevel && !Physics.CheckSphere(new Vector3(i, k, j), .25f))
                                Instantiate(Land, new Vector3(i, k, j), Quaternion.identity);
                            else if (!Physics.CheckSphere(new Vector3(i, k, j), (float)(0.25)))
                                Instantiate(Water, new Vector3(i, k, j), Quaternion.identity);


                        }
                        if (texture.GetPixel(i, j).r >= .1f && 
                            !Physics.CheckSphere(new Vector3(i, h[i, j] * MaxHeight + 2, j), .25f) &&
                            w[i,j] < waterLevel)
                        {
                            if (!treeMap.ContainsKey(new point(i, j,(int) (h[i, j] * MaxHeight + 1))))
                            {

                                tree Tree = new tree(TreeTrunk);
                                Tree.genTree(new Vector3(i, h[i, j] * MaxHeight + 1, j), 5, 5, 10, Mathf.PI / 2);
                                treeMap.Add(new point(i, j, (int)(h[i, j] * MaxHeight + 1)), Tree);
                            }
                                tree Tree1;
                                treeMap.TryGetValue(new point(i, j,(int) (h[i, j] * MaxHeight + 1)), out Tree1);
                                Tree1.generate();
                        }
                    }
                }
                catch { }                
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