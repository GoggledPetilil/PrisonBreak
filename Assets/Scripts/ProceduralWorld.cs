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
        Island
    }

    [SerializeField]
    private GenType type;
    [SerializeField]
    private float minHeight = 0f;
    [SerializeField]
    private float maxHeight = 1;
    [SerializeField]
    private int size = 10;
    [SerializeField]
    private float detail = 10;
    [SerializeField]
    private int seed = 0;
    public float[,] heights;
    [SerializeField]
    private float rockProbability;
    [SerializeField]
    public List<GameObject> propPrefab;
    public List<Vector3Int> propList;
    
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
        propList.Clear();
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
                        float perlinX = ProceduralManager.instance.GetPerlinSeed() + x / (float)size * detail;
                        float perlinZ = ProceduralManager.instance.GetPerlinSeed() + z / (float)size * detail;
                        height = (Mathf.PerlinNoise(perlinX, perlinZ) - minHeight) * maxHeight;
                        break;
                    case GenType.PerlinLayeredBased:
                        float perlinlX = (x / detail) + ProceduralManager.instance.GetPerlinSeed();
                        float perlinlZ = (z / detail) + ProceduralManager.instance.GetPerlinSeed();

                        float height1 = (Mathf.PerlinNoise(perlinlX, perlinlZ) - minHeight) * maxHeight;
                        float height2 = (Mathf.PerlinNoise(perlinlX, perlinlZ) / 2 - minHeight + 1) * maxHeight;
                        float height3 = (Mathf.PerlinNoise(perlinlX, perlinlZ) / 4 - minHeight + 2) * maxHeight;
                        float height4 = (Mathf.PerlinNoise(perlinlX, perlinlZ) / 8 - minHeight + 3) * maxHeight;
                        float height5 = (Mathf.PerlinNoise(perlinlX, perlinlZ) / 16 - minHeight + 4) * maxHeight;
                        height = height1 + height2 + height3 + height4 + height5;
                        break;
                    case GenType.SineBased:
                        float sineX = (x / detail);
                        float sineZ = (z / detail);

                        height = (Mathf.Sin(sineX + sineZ) - minHeight) * maxHeight;
                        break;
                    case GenType.Island:
                        float distance = Vector2.Distance(new Vector2(x / 2, z / 2), new Vector2(x, z));
                        //float distance = Vector2.Distance(new Vector2(0, 0), new Vector2(1, 1));
                        float pSeed = ProceduralManager.instance.GetPerlinSeed();
                        float islandX = (x / detail + pSeed);
                        float islandZ = (z / detail + pSeed);

                        //height = Mathf.Cos(distance * (Mathf.PI / 2));
                        height = (Mathf.PerlinNoise(islandX, islandZ) * (Mathf.Cos(distance / detail) - minHeight) * maxHeight);

                        if (height > maxHeight)
                            height += UnityEngine.Random.Range(minHeight, maxHeight) / distance;
                        break;
                }

                heights[x, z] = height / 1000;
                float rockRand = UnityEngine.Random.value;
                if(rockRand < rockProbability * (maxHeight / height))
                {
                    int t = UnityEngine.Random.Range(0, propPrefab.Count);
                    Vector3Int rock = new Vector3Int(x, z, t);
                    propList.Add(rock);
                }
            }
        }
        Debug.Log("World generated.");
    }

}
