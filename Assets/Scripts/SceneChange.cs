using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    public string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        Inventory.instance.ClearInventory();
        LevelManager.instance.LevelChange(sceneName);
    }
}
