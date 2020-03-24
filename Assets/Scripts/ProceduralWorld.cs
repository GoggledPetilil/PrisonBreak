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
        Island,
        PerfectIsland
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
                        float perlinlX = (x / size * detail) + ProceduralManager.instance.GetPerlinSeed();
                        float perlinlZ = (z / size * detail) + ProceduralManager.instance.GetPerlinSeed();

                        for (int i = 1; i < 5; i++)
                        {
                            height += (Mathf.PerlinNoise(perlinlX, perlinlZ) - minHeight / (2 * i)) * maxHeight;
                        }
                        break;
                    case GenType.Island:
                        float distance = Vector2.Distance(new Vector2(size / 2, size / 2), new Vector2(x, z));
                        float islandX = (x / detail + ProceduralManager.instance.GetPerlinSeed());
                        float islandZ = (z / detail + ProceduralManager.instance.GetPerlinSeed());

                        height = (Mathf.PerlinNoise(islandX, islandZ) * (Mathf.Cos(distance / detail) - minHeight) * maxHeight);

                        if (height > maxHeight)
                            height += UnityEngine.Random.Range(minHeight, maxHeight) / distance;
                        break;
                    case GenType.PerfectIsland:
                        float pDistance = Vector2.Distance(new Vector2(size / 2, size / 2), new Vector2(x, z));
                        float pIslandX = (x / detail + ProceduralManager.instance.GetPerlinSeed());
                        float pIslandZ = (z / detail + ProceduralManager.instance.GetPerlinSeed());

                        height = (Mathf.PerlinNoise(pIslandX, pIslandZ) * (Mathf.Cos(pDistance / detail * Mathf.PI) - minHeight) * maxHeight);

                        if (height > maxHeight)
                            height += UnityEngine.Random.Range(minHeight, maxHeight) / pDistance;
                        break;
                }

                heights[x, z] = height / 1000;
                float rockRand = UnityEngine.Random.value;
                if(rockRand < (rockProbability / 1000) * (maxHeight / height))
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
