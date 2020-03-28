using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raft : Pickup
{
    public int points;

    protected override Item CreateItem()
    {
        return new AccessItem(name, weight, points);
    }

    private void OnDisable()
    {
        this.gameObject.tag = "Untagged";

        Debug.Log("Checking remaining Raft Parts.");
        GameObject[] raft = GameObject.FindGameObjectsWithTag("Raft");
        if (raft.Length < 1) { GameManager.instance.Win(); }
    }

    private void OnEnable()
    {
        this.gameObject.tag = "Untagged";
    }
}
