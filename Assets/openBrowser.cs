using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI; // Ensure this namespace is included for UI components like Button'
using System.Collections;

public class ButtonSetup : MonoBehaviour
{
    public Button button;
    private bool dataReceived = false; // Ensure this is initialized correctly

    void Start()
    {
        // Find the button component if not assigned in the Inspector
        if (button == null)
            button = GetComponent<Button>();

        // Add listener to the button's onClick event
        button.onClick.AddListener(OpenAddress);
    }

    void OpenAddress()
    {
        string url = "http://127.0.0.1:5000?source=unity";
        Application.OpenURL(url);
        StartCoroutine(CheckForUserData()); // Start the coroutine to check for user data after opening the URL
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
                    dataReceived = true; // Mark as received to stop the coroutine
                    Debug.Log("User data received successfully.");
                }
            }
            yield return new WaitForSeconds(5f);  // Check every 5 seconds
        }
    }   

    void ProcessUserData(string jsonData)
    {
        // Process JSON data received from Flask
        Debug.Log("Processing user data: " + jsonData);
        // Here you could parse the jsonData to handle it as needed, for instance:
        // UserInfo userInfo = JsonUtility.FromJson<UserInfo>(jsonData);
        // Debug.Log("User ID: " + userInfo.userId);
    }
}

// Define a class to match the JSON structure you expect to receive from Flask
[System.Serializable]
public class UserInfo
{
    public string userId;
}
