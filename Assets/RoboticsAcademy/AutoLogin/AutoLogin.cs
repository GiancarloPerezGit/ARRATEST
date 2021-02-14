using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class AutoLogin : MonoBehaviour
{
    // url for executing things on database.
    string baseURL = "http://localhost/test_php_files/";

    // current token for profiles.
    public string currentToken { get; private set; } = "None";

    // if signed in.
    bool signedIn = false;

    // buttons for auto login.
    [SerializeField] Button startButton;
    [SerializeField] Button signoutButton;

    /// <summary>
    /// Buttons disabled until signed in.
    /// </summary>
    void Update()
    {
        startButton.interactable = signedIn;
        signoutButton.interactable = signedIn;
    }

    // Start method just starts polling for a currently signed in user.
    private void Start()
    {
        SignInPolling();
    }

    /// <summary>
    /// Sign into device.
    /// </summary>
    public void SignIn()
    {
        StartCoroutine(GetToken());
    }

    /// <summary>
    /// Sign into device using continuous polling.
    /// </summary>
    public void SignInPolling()
    {
        StartCoroutine(PollForToken());
    }

    /// <summary>
    /// Sign out of device.
    /// </summary>
    public void SignOut()
    {
        StartCoroutine(DeleteToken());
    }

    /// <summary>
    /// Continuously poll for a token until one signs in.
    /// </summary>
    IEnumerator PollForToken()
    {
        // poll for token continuously until signed in.
        while (!signedIn)
        {
            StartCoroutine(GetToken());
            yield return null;
        }
    }

    /// <summary>
    /// Recieves token from web server using PHP script.
    /// </summary>
    IEnumerator GetToken()
    {
        // send web request to execute php script.
        UnityWebRequest www = UnityWebRequest.Get(baseURL + "get_tokens.php");
        yield return www.SendWebRequest();

        // make sure no errors occur.
        if (www.isNetworkError || www.isHttpError) Debug.Log(www.error);
        else
        {
            if (!www.downloadHandler.text.Equals("None"))
            {
                // Just get text from page and set to token.
                currentToken = www.downloadHandler.text;

                // You are now signed in
                signedIn = true;
            }
            // debug for testing.
            Debug.Log(www.downloadHandler.text);
        }
    }

    /// <summary>
    /// Deletes current token from web server using PHP script.
    /// </summary>
    IEnumerator DeleteToken()
    {
        // Make sure token actually exists.
        if (!currentToken.Equals("None"))
        {
            // send web request to execute php script.
            UnityWebRequest www = UnityWebRequest.Get(baseURL + "sign_out.php?token=" + currentToken);
            yield return www.SendWebRequest();

            // make sure no errors occur.
            if (www.isNetworkError || www.isHttpError) Debug.Log(www.error);
            else
            {
                // delete the token, set to none.
                currentToken = "None";

                // sign out.
                signedIn = false;

                Debug.Log("signed out");
            }
        }
    }
}
