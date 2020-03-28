using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScape : Landscape
{

    private Terrain t;

    private void Start()
    {
        t = GetComponent<Terrain>();

        if(t == null)
        {
            Debug.LogError("Add Terrainscape script on a terrain.");
        }

        Init();

    }

    public override void Clean()
    {
        Debug.Log("Cleaning...");
        GameObject[] go = GameObject.FindGameObjectsWithTag("Prop");
        for (int i = 0; i < go.Length; i++)
        {
            Destroy(go[i]);
        }
    }

    public override void Generate()
    {
        Clean();
        Debug.Log("Adding heights");
        t.terrainData.heightmapResolution = ProceduralManager.instance.world.Size;
        t.terrainData.SetHeights(0, 0, ProceduralManager.instance.world.heights);

        for(int r = 0; r < ProceduralManager.instance.world.propList.Count; r++)
        {
            Vector3Int rock = ProceduralManager.instance.world.propList[r];
            Vector3 worldPos = new Vector3(
                x: MathUtils.Map(
                    s: rock.x,
                    a1: 0,
                    a2: ProceduralManager.instance.world.Size,
                    b1: t.GetPosition().x,
                    b2: t.GetPosition().x + t.terrainData.size.x),
                y: 0.0f,
                z: MathUtils.Map(
                    s: rock.y,
                    a1: 0,
                    a2: ProceduralManager.instance.world.Size,
                    b1: t.GetPosition().z,
                    b2: t.GetPosition().z + t.terrainData.size.x)
                );

            worldPos.y = t.SampleHeight(worldPos);

            Instantiate(ProceduralManager.instance.world.propPrefab[rock.z], worldPos, Quaternion.identity);
        }

        for (int ra = 0; ra < ProceduralManager.instance.world.raftParts; ra++)
        {
            Vector3Int rock = ProceduralManager.instance.world.propList[ra];
            Vector3 worldPos = new Vector3(
                x: MathUtils.Map(
                    s: rock.x,
                    a1: 0,
                    a2: ProceduralManager.instance.world.Size,
                    b1: t.GetPosition().x,
                    b2: t.GetPosition().x + t.terrainData.size.x),
                y: 0.0f,
                z: MathUtils.Map(
                    s: rock.y,
                    a1: 0,
                    a2: ProceduralManager.instance.world.Size,
                    b1: t.GetPosition().z,
                    b2: t.GetPosition().z + t.terrainData.size.x)
                );

            worldPos.y = t.SampleHeight(worldPos);

            Instantiate(ProceduralManager.instance.world.raftPrefab, worldPos, Quaternion.identity);
        }
    }
}
