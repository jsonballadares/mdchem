using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    public Toggle beginner;
    public Toggle advanced;
    public GameObject loginScreen;


    void Update()
    {

    }

    void Awake()
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

        if (PlayerPrefs.GetString("ui") == null || PlayerPrefs.GetString("ui") == "")
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
    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.FindGameObjectWithTag("AudioManager") != null)
        {
            FindObjectOfType<AudioManager>().Play("levelselectnoise");
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
}
