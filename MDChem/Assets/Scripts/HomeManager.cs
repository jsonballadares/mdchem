using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeManager : MonoBehaviour
{
    public Toggle beginner;
    public Toggle advanced;
    void Awake()
    {
        //PlayerPrefs.DeleteAll();
        if (!PlayerPrefs.HasKey("Diffilculty"))
        {
            Debug.Log("Diffilculty set to beginner");
            PlayerPrefs.SetInt("Diffilculty", (int)GameValues.Difficulties.Beginner);
            GameValues.diffuclty = GameValues.Difficulties.Beginner;
            beginner.isOn = true;
            advanced.isOn = false;
        }
        else
        {
            if (PlayerPrefs.GetInt("Diffilculty") == 0)
            {
                GameValues.diffuclty = GameValues.Difficulties.Beginner;
                beginner.isOn = true;
                advanced.isOn = false;
            }
            else
            {
                GameValues.diffuclty = GameValues.Difficulties.Advanced;
                beginner.isOn = false;
                advanced.isOn = true;
            }

        }




        if (PlayerPrefs.GetString("ui") == null || PlayerPrefs.GetString("ui") == "")
        {
            Debug.Log("UUID IS NULL");
            PlayerPrefs.DeleteAll();
        }
        else
        {
            Debug.Log("UUID IS ---> " + PlayerPrefs.GetString("ui"));
        }

        if (GameObject.FindGameObjectWithTag("AudioManager") != null)
        {
            FindObjectOfType<AudioManager>().StopAllAudio();

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
