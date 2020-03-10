using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ProceduralWorld
{

    public enum GenType
    {
        RandomBased,
        PerlinBased,
        PerlinLayeredBased,
        SineBased,
        TestBased
    }

    [SerializeField]
    public List<GameObject> rockPrefab;
    [SerializeField]
    private float minHeight = 0f;
    [SerializeField]
    private float maxHeight = 1;
    [SerializeField]
    private int size = 10;
    [SerializeField]
    private float detail = 10;
    [SerializeField]
    private float rockProbability;
    [SerializeField]
    private int seed = 0;
    [SerializeField]
    private GenType type;
    public float[,] heights;
    public List<Vector3Int> rocks;

    
    //public float MinHeight
    //{
    //    get { return minHeight; }
    //    set
    //    {
    //        minHeight = value;
    //        Init();
    //    }
    //}
    //public float MaxHeight
    //{
    //    get { return maxHeight; }
    //    set
    //    {
    //        maxHeight = value;
    //        Init();
    //    }
    //}
    public int Size
    {
        get { return size; }
        set
        {
            size = value;
            Regenerate();
        }
    }
    public float Detail
    {
        get { return detail; }
        set
        {
            detail = value;
            Regenerate();
        }
    }
    //public int Seed
    //{
    //    get { return seed; }
    //    set
    //    {
    //        seed = value;
    //        Init();
    //    }
    //}
    //public GenType Gen
    //{
    //    get { return type; }
    //    set
    //    {
    //        type = value;
    //        Init();
    //    }
    //}


    public ProceduralWorld(float minHeight, float maxHeight, int size, float detail, int seed, GenType type)
    {

        Debug.Log("Constructor of the World called.");

        this.minHeight = minHeight;
        this.maxHeight = maxHeight;
        this.size = size;
        this.detail = detail;
        this.seed = seed;
        this.type = type;
    }

    public void Init()
    {
        ProceduralManager.OnRegenerate += Regenerate;
        Regenerate();
    }

    public void Regenerate() 
    {
        heights = new float[size, size];
        ProceduralManager.instance.SetSeed(seed);
        Generate();
    }

    public void Generate()
    {

        for (int x = 0; x < heights.GetLength(dimension: 0); x++)
        {
            for (int z = 0; z < heights.GetLength(dimension: 1); z++)
            {
                float height = 0;

                switch (type)
                {
                    case GenType.RandomBased:
                        height = UnityEngine.Random.Range(minHeight, maxHeight);
                        break;
                    case GenType.PerlinBased:
                        // float perlinX = (x / detail) + ProceduralManager.instance.GetPerlinSeed();
                        //float perlinY = (z / detail) + ProceduralManager.instance.GetPerlinSeed();
                        float perlinX = ProceduralManager.instance.GetPerlinSeed() + x / (float)size * detail;
                        float perlinY = ProceduralManager.instance.GetPerlinSeed() + z / (float)size * detail;
                        height = (Mathf.PerlinNoise(perlinX, perlinY) - minHeight) * maxHeight;
                        break;
                    case GenType.PerlinLayeredBased:
                        float perlinlX = (x / detail) + ProceduralManager.instance.GetPerlinSeed();
                        float perlinlY = (z / detail) + ProceduralManager.instance.GetPerlinSeed();

                        float height1 = (Mathf.PerlinNoise(perlinlX, perlinlY) - minHeight) * maxHeight;
                        float height2 = (Mathf.PerlinNoise(perlinlX, perlinlY) / 2 - minHeight + 1) * maxHeight;
                        float height3 = (Mathf.PerlinNoise(perlinlX, perlinlY) / 4 - minHeight + 2) * maxHeight;
                        float height4 = (Mathf.PerlinNoise(perlinlX, perlinlY) / 8 - minHeight + 3) * maxHeight;
                        float height5 = (Mathf.PerlinNoise(perlinlX, perlinlY) / 16 - minHeight + 4) * maxHeight;
                        height = height1 + height2 + height3 + height4 + height5;
                        break;
                    case GenType.SineBased:
                        float sineX = (x / detail);
                        float sineY = (z / detail);

                        height = (Mathf.Sin(sineX + sineY) - minHeight) * maxHeight;
                        break;
                }

                heights[x, z] = height;
                float rockRand = UnityEngine.Random.value;
                if(rockRand < rockProbability * (maxHeight / height))
                {
                    int t = UnityEngine.Random.Range(0, rockPrefab.Count);
                    Vector3Int rock = new Vector3Int(x, z, t);
                    rocks.Add(rock);
                }
            }
        }
        Debug.Log("World generated.");
    }

}
