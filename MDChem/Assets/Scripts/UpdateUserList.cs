using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UpdateUserList : MonoBehaviour
{
    public void onLoginButtonPress()
    {
        StartCoroutine(PostCrt());
    }

    private IEnumerator PostCrt()
    {
        Debug.Log("POSTCRT IS CALLED");
        WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Get("https://www.rrmi.co/api/updatestudent"))
        {

            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.responseCode);
                Debug.Log(www.ToString());
            }
        }
    }
}
