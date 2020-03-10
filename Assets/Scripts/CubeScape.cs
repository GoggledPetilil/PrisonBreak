using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScape : Landscape
{

    public GameObject preFab;
    public bool floor;


    // Start is called before the first frame update
    void Start()
    {

        ProceduralManager.OnRegenerate += Generate;
        Generate();

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Clean();
        }

    }

    public override void Clean()
    {
        for(int i = 0; i < transform.childCount; i++)
        {

            Destroy(transform.GetChild(i).gameObject);

        }
    }

    public override void Generate()
    {
        Clean();
        for (int x = 0; x < ProceduralManager.instance.world.Size; x++)
        {
            for (int z = 0; z < ProceduralManager.instance.world.Size; z++)
            {

                float height = ProceduralManager.instance.world.heights[x, z];
                Vector3 pos = new Vector3(x: x, y: height, z: z);
                if (floor)
                {
                    pos = new Vector3(x: x, y: Mathf.Floor(height), z: z);
                }
                Instantiate(preFab, this.gameObject.transform.position + pos, Quaternion.identity, this.gameObject.transform);
            }
        }
    }
}
