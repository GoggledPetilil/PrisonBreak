using System.Collections;
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

    [SerializeField]
    private InputField input;
    [SerializeField]
    private Button exit;
    public Text resultTextField;
    public Text answerResult;

    // Start is called before the first frame update
    void Start()
    {

        //StartCoroutine(RequestAPI(apiCallBaseUrl + pokemonUrl.ToLower()));
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

        // Type Look Up
        int r = Random.Range(0, jsonObj["results"].Count - 2);
        string _type = jsonObj["results"][r]["name"];
        resultTextField.text = _type.Substring(0, 1).ToUpper() + _type.Substring(1).ToLower();

        // Pokemon Look Up
        /*
        string _name = jsonObj["forms"][0]["name"];
        string _abilities = null;
        for (int i = 0; i < jsonObj["abilities"].Count; i++)
        {
            _abilities += jsonObj["abilities"][i]["ability"]["name"];
            if (i < jsonObj["abilities"].Count - 1)
            {
                _abilities += ", ";
            }
        }
        string _types = null;
        for (int i = 0; i < jsonObj["types"].Count; i++)
        {
            _types += jsonObj["types"][i]["type"]["name"];
            if (i < jsonObj["types"].Count - 1)
            {
                _types += ", ";
            }
        }

        resultTextField.text = "Name: " + _name + "\n";
        resultTextField.text += "Ability: " + _abilities + "\n";
        resultTextField.text += "Type: " + _types + "\n";
        */
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
                    answerResult.text = "Correct.";
                    break;
                }
                else
                {
                    answerResult.text = "Incorrect.";
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
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        p.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = true;
        this.enabled = false;
    }

}
