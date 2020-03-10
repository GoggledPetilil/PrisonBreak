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

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(GetRequest(apiCallBaseUrl + "pikachu"));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateURL()
    {

        pokemonUrl = input.text;
        parseJSON(apiCallBaseUrl + pokemonUrl);

    }

    private void parseJSON(string jsonStr)
    {
        
        var jsonObj = JSON.Parse(jsonStr);

        string _name = jsonObj["forms"][0]["name"].ToString();
        string _abilities = jsonObj["abilities"][0]["name"].ToString();

        resultTextField.text = "Name: " + _name + "\n";
        resultTextField.text += "Ability " + _abilities + "\n";
        



    }

    private IEnumerator GetRequest(string url)
    {

        var req = UnityWebRequest.Get(url + input.text);
        yield return req.SendWebRequest();

        if (req.isNetworkError)
        {
            Debug.Log(": Error: " + req.error);
        }
        else
        {
            Debug.Log(":\nReceived: " + req.downloadHandler.text);
        }

    }

}
