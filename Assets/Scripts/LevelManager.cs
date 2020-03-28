using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;
    [SerializeField]
    private Image black;
    private Animator ani = instance.GetComponent<Animator>();

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

    void LevelChange(string sceneName)
    {
        
        SceneManager.LoadScene(sceneName);
    }


    private void OnLevelWasLoaded(int level)
    {
        ani.Play("Screen_FadeIN");
    }
}
