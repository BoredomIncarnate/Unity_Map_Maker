using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEditorInternal;
using UnityEngineInternal;

public class TerrainGenerationModiffied : MonoBehaviour {


    //Public
    public int NumberOfPasses;
    public float Zoom;
    public int MaxHeight;
    public int SeaLevel, SnowCap;


    public GameObject Land,Water,Snow;

    //Private
    private float[,] HeightMap;

	void Start () {
        int SizeOfArray = (int)(Mathf.Pow(2, NumberOfPasses) + 1);
        HeightMap = new float[SizeOfArray, SizeOfArray];

        GenerateMap();

        Texture2D text = new Texture2D(SizeOfArray, SizeOfArray);

        for (int i = 0; i < SizeOfArray; i++)
            for (int j = 0; j < SizeOfArray; j++)
            {
                float value = Smooth(((float)(i))/(float)(Zoom),((float)(j))/(float)(Zoom));
                text.SetPixel(i, j, new Color(value, value, value));
            }

        text.Apply();
        text.EncodeToJPG(75);

        GameObject Voxel;

        for (int i = 0; i < SizeOfArray; i++)
            for (int j = 0; j < SizeOfArray; j++)
            {
                
                int kl = (int)(text.GetPixel(i, j).grayscale * MaxHeight)+1;
                if (kl < SeaLevel)
                {
                    Voxel = Water;
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

    private float Smooth(float x, float y)
    {
        float value = 0;
        float fractX = x - (int)x;
        float fractY = y - (int)y;

        int x1 = ((int)x + HeightMap.GetLength(0)) % HeightMap.GetLength(0);
        int y1 = ((int)y + HeightMap.GetLength(0)) % HeightMap.GetLength(0);

        int x2 = (x1 + HeightMap.GetLength(0) - 1) % HeightMap.GetLength(0);
        int y2 = (y1 + HeightMap.GetLength(0) - 1) % HeightMap.GetLength(0);

        value += fractX * fractY * HeightMap[x1,y1];
        value += fractX * (1 - fractY) * HeightMap[x1,y2];
        value += (1 - fractX) * fractY * HeightMap[x2,y1];
        value += (1 - fractX) * (1 - fractY) * HeightMap[x2,y2];


        return value;
    }

    private void GenerateMap()
    {
        int Length = HeightMap.GetLength(0) - 1;
        int SizeOfPass = 1;


        setPoint(0, 0, Length,   false);
        setPoint(0, Length, Length,   false);
        setPoint(Length, 0, Length,   false);
        setPoint(Length, Length, Length,   false);

        Length /= 2;

        for (int i = 0; i < NumberOfPasses; i++)
        {

            for (int j = 0; j < SizeOfPass; j++)
            {
                for (int k = 0; k < SizeOfPass; k++)
                {
                    setPoint(Length + 2 * Length * j, Length + 2 * Length * k, Length,   true);

                    if (j == 0)
                    {
                        if (k != 0)
                        {
                            setPoint(Length + j * 2 * Length, Length * 2 * (k + 1), Length,   false);
                            setPoint(2 * Length * (j + 1), k * 2 * Length + Length, Length,   false);
                            setPoint(j, Length * (k + 1) + Length * k, Length,   false);
                        }
                        if (k == 0)
                        {
                            setPoint(j, Length * (k + 1), Length,   false);
                        }
                    }

                    if (k == 0)
                    {
                        if (j != 0)
                        {
                            setPoint(Length * 2 * (j + 1), Length + k * 2 * Length, Length,   false);
                            setPoint(j * 2 * Length + Length, 2 * Length * (k + 1), Length,   false);
                            setPoint(Length * (j + 1) + Length * j, k, Length,   false);
                        }
                        if (j == 0)
                        {
                            setPoint(Length * (j + 1), k, Length,   false);
                        }
                    }

                    setPoint(Length + 2 * Length * j, 2 * Length * (k + 1), Length,   false);
                    setPoint(2 * Length * (j + 1), Length + 2 * k * Length, Length,  false);

                }
            }

            SizeOfPass *= 2;
            Length /= 2;
        }
    }

    private void setPoint(int x, int z, int Length, bool isDiamond)
    {
            HeightMap[x, z] =  Random.value;
    }
}
