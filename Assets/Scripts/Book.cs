using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour, IInteractable
{
    public void Action()
    {
        if(GameManager.instance.bookCanvas.enabled == false)
        {
            GameManager.instance.bookCanvas.enabled = true;
            GameManager.instance.text.text = this.GetComponent<Text>().text;
            GameManager.instance.TogglePlayerMov();
        }
    }

    public void CloseBook()
    {
        GameManager.instance.bookCanvas.enabled = false;
        GameManager.instance.TogglePlayerMov();
    }
}
