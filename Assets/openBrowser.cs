using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Include this namespace for scene management
using System.Collections;

public class ButtonSetup : MonoBehaviour
{
    public Button button;
    private bool dataReceived = false;

    public string userId = "";


    void Start()
    {
        if (button == null)
            button = GetComponent<Button>();

        button.onClick.AddListener(OpenAddress);
    }

    void OpenAddress()
    {
        string url = "http://127.0.0.1:5000?source=unity";
        Application.OpenURL(url);
        StartCoroutine(CheckForUserData());
    }

    IEnumerator CheckForUserData()
    {
        while (!dataReceived)
        {
            UnityWebRequest www = UnityWebRequest.Get("http://127.0.0.1:5000/unity_data");
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError) {
                Debug.Log(www.error);
            }
            else {
                if (www.responseCode == 200) {
                    ProcessUserData(www.downloadHandler.text);
                    dataReceived = true;
                    Debug.Log("User data received successfully.");

                    // Change scene after receiving user data successfully
                    SceneManager.LoadScene("CharactersScene");
                }
            }
            yield return new WaitForSeconds(5f);  // Check every 5 seconds
        }
    }   

    void ProcessUserData(string jsonData)
    {
        Debug.Log("Processing user data: " + jsonData);

        userId = jsonData;
        GameManager.Instance.accountApiData = userId;
        
        
    }
}

// Define a class to match the JSON structure you expect to receive from Flask
[System.Serializable]
public class UserInfo
{
    public string userId;
}
