using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PostCharachterData : MonoBehaviour
{

    public InputField hp;
    public InputField playerName;
    public Button sendPlayerDataButton;
    public string uri;

    private void Start()
    {
        sendPlayerDataButton.onClick.AddListener(sendIt);
    }

    public void sendIt()
    {
        StartCoroutine(postPlayerData());
    }
    
    IEnumerator postPlayerData()
    {
        
        WWWForm form = new WWWForm();
        form.AddField("playername", playerName.text);
        form.AddField("health_points", hp.text);
        
        using (UnityWebRequest postWebRequest = UnityWebRequest.Post(uri, form))
        {
            yield return postWebRequest.SendWebRequest();

            if (postWebRequest.result == UnityWebRequest.Result.Success)
            {
                string results = postWebRequest.downloadHandler.text;
                Debug.Log(results);
            }
            else
            {
                Debug.LogError("SOME-TING WENT NOT GOOD");
            }
            postWebRequest.Dispose();
        }
    }
}
