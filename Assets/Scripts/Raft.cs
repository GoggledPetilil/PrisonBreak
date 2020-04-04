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
        GameObject[] raft = GameObject.FindGameObjectsWithTag("Raft");
        if (raft.Length < 1) { LevelManager.instance.LevelChange("Win"); }
    }

    private void OnEnable()
    {
        this.gameObject.tag = "Raft";
    }
}
