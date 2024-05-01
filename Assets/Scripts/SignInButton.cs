using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 
using System.Collections;

public class SignInButton : MonoBehaviour
{
    private Button button;
    
    void Start()
    {
        if (button == null)
            button = GetComponent<Button>();

        button.onClick.AddListener(OpenAddress);
    }
        
    void OpenAddress()
    {
        string url = "http://127.0.0.1:5000/register";
        Application.OpenURL(url);
    }
    
}
