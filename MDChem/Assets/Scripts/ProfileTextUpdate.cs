using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileTextUpdate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!(PlayerPrefs.GetString("ui") == null || PlayerPrefs.GetString("ui") == ""))
        {
            // Debug.Log("The users email " + PlayerPrefs.GetString("email"));
            // Debug.Log("The users uiid " + PlayerPrefs.GetString("ui"));
            this.GetComponent<TMPro.TMP_Text>().text = PlayerPrefs.GetString("email");
        }else{
            this.GetComponent<TMPro.TMP_Text>().text = "";
        }
    }
}
