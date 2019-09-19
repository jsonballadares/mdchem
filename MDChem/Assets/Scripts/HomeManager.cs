using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

/*
This class acts as a manager for the home screen
 */
public class HomeManager : MonoBehaviour
{
    public Toggle beginner;
    public Toggle advanced;
    public GameObject loginScreen;

    void Start()
    {
        PlayerPrefs.SetString("data", @"{""levelID"":""1b"",""score"":500,""correctData"":[""AlkaliMetals=LiNaKRbCs"",""AlkaliMetals=LiNaKRbCs"",""AlkalineMetals=SrCaBaBeMg"",""AlkalineMetals=BaBeSrMgCa""],""incorrectData"":[]}|{""levelID"":""2b"",""score"":500,""correctData"":[""AlkaliMetals=LiNaKRbCs"",""AlkaliMetals=LiNaKRbCs"",""AlkalineMetals=SrCaBaBeMg"",""AlkalineMetals=BaBeSrMgCa""],""incorrectData"":[]}|{""levelID"":""3b"",""score"":500,""correctData"":[""AlkaliMetals=LiNaKRbCs"",""AlkaliMetals=LiNaKRbCs"",""AlkalineMetals=SrCaBaBeMg"",""AlkalineMetals=BaBeSrMgCa""],""incorrectData"":[]}|{""levelID"":""4b"",""score"":500,""correctData"":[""AlkaliMetals=LiNaKRbCs"",""AlkaliMetals=LiNaKRbCs"",""AlkalineMetals=SrCaBaBeMg"",""AlkalineMetals=BaBeSrMgCa""],""incorrectData"":[]}");
        //UnityWebRequest.ClearCookieCache();
        //PlayerPrefs.DeleteAll();
        Debug.Log("The current user is " + PlayerPrefs.GetString("email"));
        if (GameObject.FindGameObjectWithTag("AudioManager") != null)
        {
            FindObjectOfType<AudioManager>().Play("levelselectnoise");
        }
        checkLogin();
        checkTokenStatus();
    }

    public void deCacheData()
    {
        /*
        SEND THE ENTIRE STRING INSTEAD OF THE INDIVIDUAL
         */
        if (PlayerPrefs.HasKey("data"))
        {
            // String dataToSend = PlayerPrefs.GetString("data");
            // StartCoroutine(WebRequestManager.sendData(Enviorment.URL + "/api/player/", dataToSend));
            String dataToSend = PlayerPrefs.GetString("data");
            var stringArr = dataToSend.Split('|');
            var stringList = new ArrayList(stringArr);
            Debug.Log("BEFORE THE COUNT IS " + stringList.Count);
            PlayerPrefs.DeleteKey("data");
            foreach (var item in stringList)
            {
                Debug.Log("THE SIZE OF THE LIST " + stringList.Count);
                StartCoroutine(WebRequestManager.sendData(Enviorment.URL + "/api/player/", (string)item, (request) =>
                {
                    Debug.Log("FOR LOOP RESPONSE CODE == " + request.responseCode);
                    Debug.Log("RESPONSE TEXT " + request.downloadHandler.text);
                    if (request.responseCode != 200)
                    {
                        WebRequestManager.cacheData((string)item);
                    }
                }));
            }
        }

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

    }

    /*
    checks to see if the token is experied if it is we prompt the user to sign in again
    which gives them a new token
     */
    public void checkTokenStatus()
    {
        if (!PlayerPrefs.GetString("email").Equals("guest@guest.com"))
        {
            StartCoroutine(WebRequestManager.tokenCheck(Enviorment.URL + "/api/auth/ping", (myReturnValue) =>
            {
                if (myReturnValue)
                {
                    //dont bring up the login screen
                    Debug.Log("The token isnt expired therefore no need to login so dont bring the screen up");
                    loginScreen.SetActive(false);
                    deCacheData();
                }
                else
                {
                    //bring up the login screen
                    Debug.Log("The token is expired therefore we need to bring up the login so they can renew it");
                    loginScreen.SetActive(true);
                }
            }));
        }
        else
        {
            Debug.Log("guest login no need to check for cookie");
        }
    }

}
