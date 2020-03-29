using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    private Animator ani;

    // Start is called before the first frame update
    void Start()
    {
        ani = GetComponent<Animator>();
    }

    public void FadeIn()
    {
        ani.Play("Screen_FadeIN");
        StartCoroutine(WaitForAnimation());
    }

    public void FadeOut()
    {
        ani.Play("Screen_FadeOUT");
        StartCoroutine(WaitForAnimation());
    }
    
    IEnumerator WaitForAnimation()
    {
        AnimatorStateInfo currInfo = ani.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(currInfo.normalizedTime);
        Debug.Log("Stuff Happens.");
    }

    public IEnumerator LevelChange()
    {
        ani.Play("Screen_FadeOUT");
        AnimatorStateInfo currInfo = ani.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(currInfo.normalizedTime);
        Debug.Log("Scene Load.");
    }
}
