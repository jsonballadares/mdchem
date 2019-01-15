using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeManager : MonoBehaviour
{

    void Awake()
    {
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

    // Update is called once per frame
    void Update()
    {

    }
}
