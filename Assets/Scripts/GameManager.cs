﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;
    public Canvas computerCanvas;
    public Canvas bookCanvas;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    public void TogglePlayerMov(bool state)
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = !state;
        p.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = state;
    }
}
