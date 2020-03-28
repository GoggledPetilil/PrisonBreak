using System.Collections;
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

    public void TogglePlayerMov()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = !Cursor.visible;
        p.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = 
            !p.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled;
    }

    public void Win()
    {
        TogglePlayerMov();
        GameObject i = GameObject.Find("Black");
        while(i.GetComponent<Image>().color != new Color(0, 0, 0, 255))
        {

        }
    }
}
