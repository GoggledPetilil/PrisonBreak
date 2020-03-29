using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public static LevelManager instance;
    private Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
            ani = GetComponent<Animator>();
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        ani.Play("Screen_FadeIN");
    }

    public void LevelChange(string sceneName)
    {
        Debug.Log("Loading...");
        ani.Play("Screen_FadeOUT");
        StartCoroutine(FadeOutChange(sceneName));
    }

    IEnumerator FadeOutChange(string sceneName)
    {
        GameManager.instance.TogglePlayerMov(false);
        AnimatorStateInfo currInfo = ani.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(currInfo.normalizedTime);
        SceneManager.LoadScene(sceneName);
    }
}
