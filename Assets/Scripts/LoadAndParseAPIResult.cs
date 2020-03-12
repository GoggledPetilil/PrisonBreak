using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using SimpleJSON;

public class LoadAndParseAPIResult : MonoBehaviour
{

    public string apiCallBaseUrl = "https://pokeapi.co/api/v2/pokemon/";
    public string pokemonUrl;

    public InputField input;
    public Text resultTextField;
    public Texture image;

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(RequestAPI(apiCallBaseUrl + pokemonUrl.ToLower()));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateURL()
    {

        pokemonUrl = input.text;
        StartCoroutine(RequestAPI(apiCallBaseUrl + pokemonUrl.ToLower()));

    }

    private void ParseJSON(string jsonStr)
    {
        
        var jsonObj = JSON.Parse(jsonStr);

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

        //image.

        //image = UnityWebRequestTexture.GetTexture("https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/549.png");

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
}
