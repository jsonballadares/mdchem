using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerButtonTextUpdate : MonoBehaviour
{
    Boolean flag = false;

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.HasKey("email"))
        {
            // Debug.Log("The users email " + PlayerPrefs.GetString("email"));
            // Debug.Log("The users uiid " + PlayerPrefs.GetString("ui"));
            this.GetComponent<TMPro.TMP_Text>().text = "Welcome " + PlayerPrefs.GetString("email");
        }else{
            this.GetComponent<TMPro.TMP_Text>().text = "";
        }
    }
}
