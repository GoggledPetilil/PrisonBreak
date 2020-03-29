using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour, IInteractable
{

    LoadAndParseAPIResult computer;

    // Start is called before the first frame update
    void Start()
    {
        computer = GetComponent<LoadAndParseAPIResult>();
        if(computer == null)
        {
            Debug.LogError("LoadAndParseAPIResult script missing.");
        }
    }

    public void Action()
    {
        if(computer.enabled == false)
        {
            GameManager.instance.computerCanvas.enabled = true;
            GameManager.instance.TogglePlayerMov(false);
            computer.enabled = true;
            computer.input.text = "";
        }
    }
}
