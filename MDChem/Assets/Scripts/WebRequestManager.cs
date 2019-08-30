using System.Collections;
using System.Collections.Generic;
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
    public static IEnumerator register(string url, String jsonObject)
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
            GameObject.FindGameObjectWithTag("SignUpScreen").SetActive(false);
            Debug.Log("good youve been registered");
        }
        else
            Debug.Log("mistakes where made try again!");

        //date/time mechanism 
        PlayerPrefs.SetString("LOGINAT", DateTime.Now.ToString());

        Debug.Log("LOGIN: " + PlayerPrefs.GetString("LOGINAT"));
        Debug.Log("Status Code: " + request.responseCode);
        Debug.Log("Post Request Complete!");
    }
    public static IEnumerator login(string url, String jsonObject)
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
            Debug.Log("good youve been login");
        else
            Debug.Log("mistakes where made try again!");

        //date/time mechanism 
        PlayerPrefs.SetString("LOGINAT", DateTime.Now.ToString());

        Debug.Log("LOGIN: " + PlayerPrefs.GetString("LOGINAT"));
        Debug.Log("Status Code: " + request.responseCode);
        Debug.Log("Post Request Complete!");
    }
    public static IEnumerator sendData(string url, String jsonObject)
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

        //date/time mechanism 
        PlayerPrefs.SetString("LOGINAT", DateTime.Now.ToString());

        Debug.Log("LOGIN: " + PlayerPrefs.GetString("LOGINAT"));
        Debug.Log("Status Code: " + request.responseCode);
        Debug.Log("Post Request Complete!");
    }

    public static IEnumerator tokenCheck(string url, System.Action<bool> callback)
    {
        var request = new UnityWebRequest(url, "GET");
        Debug.Log("DA COOKIE -------> " + request.GetResponseHeader("Cookie"));
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
}
