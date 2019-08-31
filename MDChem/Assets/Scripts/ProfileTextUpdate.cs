using UnityEngine;

/*
updates the profile name 
 */
public class ProfileTextUpdate : MonoBehaviour
{
    void Update()
    {
        if (PlayerPrefs.HasKey("email"))
            this.GetComponent<TMPro.TMP_Text>().text = PlayerPrefs.GetString("email");
        else
            this.GetComponent<TMPro.TMP_Text>().text = "";

    }
}
