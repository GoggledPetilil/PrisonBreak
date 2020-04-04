using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jerry : MonoBehaviour
{
    public int spawnChance;
    private void Awake()
    {
        if(spawnChance > 100) 
        { 
            spawnChance = 100; 
        }
        else if(spawnChance < 0)
        {
            spawnChance = 0;
        }
        int r = Random.Range(0, 100 - spawnChance);
        if(r < 100 - spawnChance) { this.gameObject.SetActive(false); }

    }

    private void OnBecameVisible()
    {
        StartCoroutine(Disappear());
    }

    IEnumerator Disappear()
    {
        yield return new WaitForSeconds(1);
        this.gameObject.SetActive(false);
    }
}
