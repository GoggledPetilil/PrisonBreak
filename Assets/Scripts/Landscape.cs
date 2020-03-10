using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Landscape : MonoBehaviour
{

    private void Start()
    {
        Init();
    }

    protected void Init()
    {

        ProceduralManager.OnRegenerate += Generate;
        Generate();

    }

    public virtual void Generate(){}

    public virtual void Clean(){}

}
