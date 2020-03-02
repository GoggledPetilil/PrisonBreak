using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item
{

    public string name;
    public float weight;

    public Item(string name, float weight)
    {

        this.name = name;
        this.weight = weight;

    }

}
