using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{

    public int id;
    public bool open;
    private float initialPosition;
    private float initialRotation;

    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position.y;
        initialRotation = transform.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (open && transform.position.y > initialPosition - (transform.localScale.y * 0.95f))
        {
            transform.position = Vector3.MoveTowards(
                current: transform.position, 
                target: new Vector3(transform.position.x, initialPosition - (transform.localScale.y * 0.95f), transform.position.z), 
                maxDistanceDelta: 0.2f);
        }
        else if (!open && transform.position.y < initialPosition)
        {
            transform.position = Vector3.MoveTowards(
                current: transform.position,
                target: new Vector3(transform.position.x, initialPosition, transform.position.z),
                maxDistanceDelta: 0.2f);
        }
    }


    void Open()
    {
        if(id == -1 || Inventory.instance.HasKey(id))
        {
            open = !open;
        }
    }

    public void Action()
    {
        Open();
    }
}
