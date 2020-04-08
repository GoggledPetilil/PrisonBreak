using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pickup : MonoBehaviour, IInteractable
{

    public string name;
    public float weight;
    public Sprite icon;
    private GameObject inventoryObj;

    // Start is called before the first frame update
    void Start()
    {
        //InventoryUI.instance.RegisterPickUpItem(this);
    }

    public void Action()
    {
        if (Inventory.instance.AddItem(CreateItem()))
        {
            gameObject.SetActive(false);
        }
    }

    public bool isInInventory()
    {
        return inventoryObj != null;
    }

    public void setInventoryObj(GameObject go)
    {
        inventoryObj = go;
    }

    protected abstract Item CreateItem();
}
