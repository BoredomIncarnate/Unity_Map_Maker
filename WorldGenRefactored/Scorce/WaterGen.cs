using UnityEngine;
using System.Collections;

public class WaterGen : Generatable {

    float[,] Terrain;
    float Zoom;

    public WaterGen(float[,] Terrain, float WaterFrequency)
    {
        this.Terrain = Terrain;
        Zoom = WaterFrequency;
    }
    
    public float[,] Generate()
    {
        return Interpolate(GenerateRandomValues());
    }

    private float[,] Interpolate(float[,] w)
    {
        float[,] nh = (float[,])w.Clone();
        for (int i = 0; i < w.GetLength(0); i++)
            for (int j = 0; j < w.GetLength(1); j++)
                nh[i, j] = (Smooth((float)(i) / (float)(Zoom), (float)(j) / (float)(Zoom), w));

        return nh;   
    }

    private float Smooth(float x, float y, float[,] HeightMap)
    {
        float value = 0;
        float fractX = x - (int)x;
        float fractY = y - (int)y;

        int x1 = ((int)x + HeightMap.GetLength(0)) % HeightMap.GetLength(0);
        int y1 = ((int)y + HeightMap.GetLength(0)) % HeightMap.GetLength(0);

        int x2 = (x1 + HeightMap.GetLength(0) - 1) % HeightMap.GetLength(0);
        int y2 = (y1 + HeightMap.GetLength(0) - 1) % HeightMap.GetLength(0);

        value += fractX * fractY * HeightMap[x1, y1];
        value += fractX * (1 - fractY) * HeightMap[x1, y2];
        value += (1 - fractX) * fractY * HeightMap[x2, y1];
        value += (1 - fractX) * (1 - fractY) * HeightMap[x2, y2];

        return value;
    }


    private float[,] GenerateRandomValues()
    { 
        float[,] w = new float[Terrain.GetLength(0),Terrain.GetLength(1)];
        
        for (int i = 0; i < w.GetLength(0); i++)
            for (int j = 0; j < w.GetLength(1); j++)
                w[i, j] = Random.value;

        return w;
    }

}
