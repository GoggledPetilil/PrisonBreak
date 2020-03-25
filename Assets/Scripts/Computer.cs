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
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            p.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = false;
            computer.enabled = true;
            computer.input.text = "";
        }
    }
}
