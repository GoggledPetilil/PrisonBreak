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

    public InputField input;
    [SerializeField]
    private Button exit;
    public Text resultTextField;
    public Text answerResult;

    private AudioSource aud;
    [SerializeField]
    private AudioClip[] audClips;

    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
        if(aud == null) { Debug.Log("Please add Audio Source Component to this script."); }
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
                    PlayAudio(0);
                    answerResult.text = "CORRECT!";
                    GameObject bd = GameObject.Find("Big Door");
                    bd.GetComponent<Door>().open = true;
                    ExitProgram();
                    break;
                }
                else
                {
                    PlayAudio(1);
                    answerResult.text = "INCORRECT!";
                }
            }
        }
        else
        {
            PlayAudio(1);
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
        GameManager.instance.computerCanvas.enabled = false;
        GameManager.instance.TogglePlayerMov(true);
        this.enabled = false;
    }

    private void PlayAudio(int i)
    {
        aud.clip = audClips[i];
        aud.Play();
    }
}
