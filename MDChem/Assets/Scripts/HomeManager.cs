using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    public Toggle beginner;
    public Toggle advanced;
    public GameObject loginScreen;

    // Start is called before the first frame update
    void Start()
    {

        //UnityWebRequest.ClearCookieCache();
        Debug.Log("The current user is " + PlayerPrefs.GetString("email"));
        Debug.Log("The current users uuid is " + PlayerPrefs.GetString("ui"));
        if (GameObject.FindGameObjectWithTag("AudioManager") != null)
        {
            FindObjectOfType<AudioManager>().Play("levelselectnoise");
        }
        checkLogin();
        checkTokenStatus();
    }

    public void setBeginnerDifficulty(bool isOn)
    {
        if (isOn)
        {
            Debug.Log("The diffulculty is beginner");
            GameValues.diffuclty = GameValues.Difficulties.Beginner;
            PlayerPrefs.SetInt("Diffilculty", (int)GameValues.Difficulties.Beginner);
        }
    }

    public void setAdvancedDifficulty(bool isOn)
    {
        if (isOn)
        {
            Debug.Log("The diffulculty is advanced");
            GameValues.diffuclty = GameValues.Difficulties.Advanced;
            PlayerPrefs.SetInt("Diffilculty", (int)GameValues.Difficulties.Advanced);
        }
    }

    public void checkLogin()
    {
        //PlayerPrefs.DeleteAll();
        /*
        Checks to see the difficulty and sets the ui accordingly
         */
        if (!PlayerPrefs.HasKey("Diffilculty"))
        {
            //if the key doesnt exist create the key and set the difficulty to beginner 
            Debug.Log("Diffilculty is set to beginner and the Difficulty key has been created");
            PlayerPrefs.SetInt("Diffilculty", (int)GameValues.Difficulties.Beginner);
            GameValues.diffuclty = GameValues.Difficulties.Beginner;
            beginner.isOn = true;
            advanced.isOn = false;
        }
        else
        {
            if (PlayerPrefs.GetInt("Diffilculty") == 0)
            {
                Debug.Log("Setting difficulty to beginner");
                GameValues.diffuclty = GameValues.Difficulties.Beginner;
                beginner.isOn = true;
                advanced.isOn = false;
            }
            else
            {
                Debug.Log("Setting difficulty to advanced");
                GameValues.diffuclty = GameValues.Difficulties.Advanced;
                beginner.isOn = false;
                advanced.isOn = true;
            }
        }


        /*
        Checks to see if there is a uuid in the player pref if there isnt we need 
        to delete all the player prefs and ask the user to sign in/up
        */

        if (!PlayerPrefs.HasKey("ui") || PlayerPrefs.GetString("ui") == null || PlayerPrefs.GetString("ui").Equals(""))
        {
            Debug.Log("UUID IS NULL");
            PlayerPrefs.DeleteAll();
            loginScreen.SetActive(true);
        }
        else
        {
            Debug.Log("UUID IS ---> " + PlayerPrefs.GetString("ui"));
        }
    }


    public void checkTokenStatus()
    {
        StartCoroutine(WebRequestManager.tokenCheck(Enviorment.URL + "/api/auth/ping", (myReturnValue) =>
        {
            if (myReturnValue)
            {
                //dont bring up the login screen
                Debug.Log("The token isnt expired therefore no need to login so dont bring the screen up");
                loginScreen.SetActive(false);
            }
            else
            {
                //bring up the login screen
                Debug.Log("The token is expired therefore we need to bring up the login so they can renew it");
                loginScreen.SetActive(true);
            }
        }));
        //StartCoroutine(WebRequestManager.tokenCheck(Enviorment.URL + "/api/auth/ping"));
    }

}
