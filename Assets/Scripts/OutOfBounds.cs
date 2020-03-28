using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Teleporting...");
        other.gameObject.transform.position = new Vector3(120, 10, 120);
    }
}
