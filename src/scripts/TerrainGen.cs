using UnityEngine;
using System.Collections;

public class TerrainGen : Generatable
{

    private int chunckSize, Zoom;

    public TerrainGen(int chunckSize, int Zoom)
    {
        this.chunckSize = chunckSize;
        this.Zoom = Zoom;
    }

    public float[,] Generate()
    {
        return Interpolate(generateRandomValues());
    }

    private float[,] Interpolate(float[,] h)
    {
        float[,] nh = (float[,])h.Clone();
        for (int i = 0; i < h.GetLength(0); i++)
            for (int j = 0; j < h.GetLength(1); j++)
                nh[i,j] = (Smooth((float)(i)/(float)(Zoom), (float)(j)/(float)(Zoom), h));

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

    private float[,] generateRandomValues()
    {
        float[,] h = new float[chunckSize, chunckSize];

        for (int i = 0; i < h.GetLength(0); i++) 
            for(int j = 0; j < h.GetLength(1); j++)
                h[i,j] = Random.value;

        return h;
    }

}
