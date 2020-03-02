using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Inventory : MonoBehaviour
{

    public static Inventory instance;

    private List<Item> items;
    public float maxWeight = 10f;
    public float totalWeight;
    private GameObject inventoryObj;

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
            //InventoryUI.instance.Add(item);
            items.Add(item);
            totalWeight += item.weight;
            return true;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Inventory"))
        {

            foreach(Item i in items)
            {
                Debug.Log(i.name);
            }

        }

    }

    public bool HasKey(int id)
    {

        for(int i = 0; i < items.Count; i++)
        {
            if(items[i] is AccessItem)
            {
                AccessItem it = (AccessItem)items[i];
                if(it.door == id)
                {
                    return true;
                }
            }
        }

        return false;

    }

}
