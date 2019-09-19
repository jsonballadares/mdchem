using System.Collections;
using UnityEngine;
using System;
using UnityEngine.Networking;
using System.Text;
using UnityEngine.SceneManagement;

/*
This class will be a singleton class that makes https requests
 */
public class WebRequestManager : MonoBehaviour
{
    /*
    This method will register a user in the database
     */
    public static IEnumerator register(string url, String jsonObject, System.Action<bool> callback)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonObject);

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        Debug.Log(request.downloadHandler.text);

        /*
        a 200 means a success the user has been registered 
        anything else means a failure if failure than make the user try again
        once a 200 is hit save the user data in player prefs and allow them to change
        scenes
         */

        if (request.responseCode == 200)
        {
            Debug.Log("USER HAS BEEN REGISTERED CODE 200");
            callback(true);
        }
        else if (request.responseCode == 400)
        {
            Debug.Log("USER WANSNT REGISTERED CODE 400");
            callback(false);
        }
        else
        {
            callback(false);
            Debug.Log("USER WANSNT REGISTERED NEITHER CODE 400 OR 200");
        }
        Debug.Log("POST Request Complete!");
    }

    /*
    this method will login a user
     */
    public static IEnumerator login(string url, String jsonObject, System.Action<bool> callback)
    {
        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonObject);

        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        Debug.Log(request.downloadHandler.text);
        Debug.Log("RESPONSE COOKIE " + request.GetResponseHeader("Set-Cookie"));

        /*
        a 200 means a success the user has been registered 
        anything else means a failure if failure than make the user try again
        once a 200 is hit save the user data in player prefs and allow them to change
        scenes
         */

        if (request.responseCode == 200)
        {
            Debug.Log("USER HAS BEEN LOGGED IN CODE 200");
            callback(true);
        }
        else if (request.responseCode == 400)
        {
            Debug.Log("USER COULDNT BE LOGGED IN CODE 400");
            callback(false);
        }
        else
        {
            callback(false);
            Debug.Log("USER COULDNT BE LOGGED IN NEITHER CODE 200 or 400");
        }
        Debug.Log("POST Request Complete!");
    }

    public static void cacheData(string jsonObject)
    {
        if (PlayerPrefs.HasKey("data"))
        {
            String oldPref = PlayerPrefs.GetString("data");
            PlayerPrefs.SetString("data", oldPref + "|" + jsonObject);
        }
        else
            PlayerPrefs.SetString("data", jsonObject);

        Debug.Log("PLAYER PREFS DATA KEY VALUE ----> " + PlayerPrefs.GetString("data"));
    }


    /*
    this method is what is used to send data from the application to the database
     */
    public static IEnumerator sendData(string url, String jsonObject, System.Action<UnityWebRequest> callback)
    {
        if (!PlayerPrefs.GetString("email").Equals("guest@guest.com"))
        {

            var request = new UnityWebRequest(url, "PATCH");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonObject);

            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            callback(request);
            if (request.isHttpError || request.isNetworkError)
            {
                Debug.Log("CANT REALLY SEND DATA WITH AN ERROR SO CACHE IT");
                cacheData(jsonObject);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
            }

            Debug.Log(request.downloadHandler.text);
            Debug.Log("RESPONSE COOKIE " + request.GetResponseHeader("Set-Cookie"));

            /*
            a 200 means a success the user has been registered 
            anything else means a failure if failure than make the user try again
            once a 200 is hit save the user data in player prefs and allow them to change
            scenes
             */
            if (request.responseCode == 200)
                Debug.Log("good youve sent some data from " + SceneManager.GetActiveScene().name);
            else
                Debug.Log("mistakes where made data couldnt be sent");

            Debug.Log("Status Code: " + request.responseCode);
            Debug.Log("Post Request Complete!");
        }
        else
        {
            Debug.Log("guest login therefore there is no need to send data");
        }

    }

    public static IEnumerator sendData(string url, String jsonObject)
    {
        if (!PlayerPrefs.GetString("email").Equals("guest@guest.com"))
        {

            var request = new UnityWebRequest(url, "PATCH");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonObject);

            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            Debug.Log(request.downloadHandler.text);
            Debug.Log("RESPONSE COOKIE " + request.GetResponseHeader("Set-Cookie"));

            /*
            a 200 means a success the user has been registered 
            anything else means a failure if failure than make the user try again
            once a 200 is hit save the user data in player prefs and allow them to change
            scenes
             */
            if (request.responseCode == 200)
                Debug.Log("good youve sent some data from " + SceneManager.GetActiveScene().name);
            else
                Debug.Log("mistakes where made data couldnt be sent");

            Debug.Log("Status Code: " + request.responseCode);
            Debug.Log("Post Request Complete!");
        }
        else
        {
            Debug.Log("guest login therefore there is no need to send data");
        }

    }
    /*
    this method checks to see if the token is experied if it is it requires 
    the user to login agin which gives them a new cookie
     */
    public static IEnumerator tokenCheck(string url, System.Action<bool> callback)
    {
        var request = new UnityWebRequest(url, "GET");
        Debug.Log("THE COOKIE ------> " + request.GetResponseHeader("Cookie"));
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        Debug.Log("THE TOKEN REQUEST REPONSE CODE -----> " + request.responseCode);


        if (request.responseCode == 200)
        {
            Debug.Log("the token is good you dont need to login again");
            callback(true);
        }
        else if (request.responseCode == 400)
        {
            Debug.Log("the token is expired so you need to login again");
            callback(false);
        }
        else
        {
            callback(false);
            Debug.Log("lmao not a 400 or a 200 idk bruh");
        }

    }

    /*
    Gets the score for each level inorder to setup the stars for the level selector
     */
    public static IEnumerator getScores(string url,System.Action<UnityWebRequest> callback)
    {
        var request = new UnityWebRequest(url, "POST");
        
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        Debug.Log(request.downloadHandler.text);

        /*
        a 200 means a success the user has been registered 
        anything else means a failure if failure than make the user try again
        once a 200 is hit save the user data in player prefs and allow them to change
        scenes
         */

        if (request.responseCode == 200)
        {
            Debug.Log("CSV HAS BEEN SENT CODE 200");
            callback(request);
        }
        else if (request.responseCode == 400)
        {
            Debug.Log("CSV NOT SENT CODE 400");
            callback(request);
        }
        else
        {
            callback(request);
            Debug.Log("USER WANSNT REGISTERED NEITHER CODE 400 OR 200");
        }
        Debug.Log("POST Request Complete!");
    }
}
