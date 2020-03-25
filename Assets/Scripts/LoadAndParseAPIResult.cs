﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;

public class LoadAndParseAPIResult : MonoBehaviour
{

    [SerializeField]
    private string apiCallBaseUrl = "https://pokeapi.co/api/v2/pokemon/";
    private string apiCallTypeUrl = "https://pokeapi.co/api/v2/type/";
    public string pokemonUrl;

    public InputField input;
    [SerializeField]
    private Button exit;
    public Text resultTextField;
    public Text answerResult;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(RequestAPI(apiCallTypeUrl));

    }

    public void UpdateURL()
    {

        pokemonUrl = input.text;
        StartCoroutine(RequestGuessAPI(apiCallBaseUrl + pokemonUrl.ToLower()));

    }

    private void ParseJSON(string jsonStr)
    {
        
        var jsonObj = JSON.Parse(jsonStr);

        int r = Random.Range(0, jsonObj["results"].Count - 2);
        string _type = jsonObj["results"][r]["name"];
        resultTextField.text = _type.Substring(0, 1).ToUpper() + _type.Substring(1).ToLower();
    }

    private void ParseGuessJSON(string jsonStr)
    {
        var jsonObj = JSON.Parse(jsonStr);
        if(jsonObj != null)
        {
            for (int i = 0; i < jsonObj["types"].Count; i++)
            {
                string j = jsonObj["types"][i]["type"]["name"];
                string n = j.Substring(0, 1).ToUpper() + j.Substring(1).ToLower();
                if (n == resultTextField.text)
                {
                    answerResult.text = "CORRECT!";
                    GameObject bd = GameObject.Find("Big Door");
                    bd.GetComponent<Door>().open = true;
                    ExitProgram();
                    break;
                }
                else
                {
                    answerResult.text = "INCORRECT!";
                }
            }
        }
        else
        {
            answerResult.text = "ERROR!\nANSWER DID NOT YIELD ANY RESULTS!";
        }
        
    }

    protected virtual IEnumerator RequestAPI(string WebURL)
    {
        using (UnityWebRequest Request = UnityWebRequest.Get(WebURL))
        {
            yield return Request.SendWebRequest();


            string[] pages = WebURL.Split('/');
            int page = pages.Length;

            if (Request.isNetworkError)
            {
                Debug.Log(pages[page] + "Error" + Request.error);
                yield break;
            }

            //ParseJSON
            ParseJSON(Request.downloadHandler.text);
        }
    }

    protected virtual IEnumerator RequestGuessAPI(string WebURL)
    {
        using (UnityWebRequest Request = UnityWebRequest.Get(WebURL))
        {
            yield return Request.SendWebRequest();


            string[] pages = WebURL.Split('/');
            int page = pages.Length;

            if (Request.isNetworkError)
            {
                Debug.Log(pages[page] + "Error" + Request.error);
                yield break;
            }

            //ParseJSON
            ParseGuessJSON(Request.downloadHandler.text);
        }
    }

    public void ExitProgram()
    {
        Debug.Log("Exiting...");
        GameManager.instance.computerCanvas.enabled = false;
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
        p.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
        this.enabled = false;
    }

}
