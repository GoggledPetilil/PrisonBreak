using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public static Inventory instance;

    private List<Item> items;
    public float maxWeight = 10f;
    public float totalWeight;

    // Start is called before the first frame update
    void Start()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }

        items = new List<Item>();

    }

    public bool AddItem(Item item)
    {
        if(totalWeight + item.weight > maxWeight)
        {
            Debug.Log(item.name + " was not added.");
            return false;
        }
        else
        {
            Debug.Log(item.name + " was added.");
            items.Add(item);
            totalWeight += item.weight;
            return true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
