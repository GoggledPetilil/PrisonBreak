using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    
    public void StartGame()
    {
        LevelManager.instance.LevelChange("Inside");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
