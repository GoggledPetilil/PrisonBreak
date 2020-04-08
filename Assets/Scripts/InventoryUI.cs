using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{

    public static InventoryUI instance;

    public GameObject itemPrefab;
    private Dictionary<string, Pickup> items;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this);
        }

        items = new Dictionary<string, Pickup>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RegisterPickUpItem(Pickup i)
    {
        if (!items.ContainsKey(i.name))
        {
            items.Add(i.name, i);
        }
    }

    public void Add(Item i)
    {
        if (items.ContainsKey(i.name) && !items[i.name].isInInventory())
        {
            GameObject go = Instantiate(itemPrefab, transform);
            go.GetComponent<Image>().sprite = items[i.name].icon;
            go.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = i.name;
            items[i.name].setInventoryObj(go);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
