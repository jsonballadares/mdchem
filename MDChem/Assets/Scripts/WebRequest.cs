using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequest : MonoBehaviour
{
    public static WebRequest insance;
    void Awake()
    {
        if (insance == null)
        {
            insance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
    public void PostData(Dictionary<string, string> d, string data)
    {
        StartCoroutine(PostCrt(d, data));
    }
    private IEnumerator PostCrt(Dictionary<string, string> d, string data)
    {
        Debug.Log("POSTCRT IS CALLED");
        WWWForm form = new WWWForm();

        using (UnityWebRequest www = UnityWebRequest.Post("https://www.mdchem.app/api/save", form))
        {
            byte[] rawData = System.Text.Encoding.ASCII.GetBytes(data);
            UploadHandler uploader = new UploadHandlerRaw(rawData);
            www.uploadHandler = uploader;
            foreach (string x in d.Keys)
            {
                www.SetRequestHeader(x, d[x]);
            }
            www.SetRequestHeader("Content-Type", "application/json");
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
 /*                Element.elementArray.Clear();
                QuizData.elementArray.Clear();
                DraggableGameData.elementArray.Clear(); */
                Debug.Log(www.responseCode);
                Debug.Log(www.ToString());
                Debug.Log("Post Request Complete!");
            }
        }
    }
    public void Destroy()
    {
        Destroy(this);
    }


    public string buildJSON()
    {
        string correct = "", missed = "", incorrect = "";


        foreach (Element item in Element.elementArray)
        {
            Debug.Log(item.ToString());
            switch (item.state)
            {
                case Element.State.Correct:

                    correct += "{\"element\":\" " + item.getName() + "\", \"duration\":" + item.getDuration() + " },";
                    break;

                case Element.State.Incorrect:

                    incorrect += "{\"element\":\" " + item.getName() + "\", \"duration\":" + item.getDuration() + " },";
                    break;

                case Element.State.Missed:

                    missed += "{\"element\":\" " + item.getName() + "\", \"duration\":" + item.getDuration() + " },";
                    break;
            }
        }
        Element.elementArray.Clear();
        if (missed != "")
        {
            missed = missed.Substring(0, missed.Length - 1);
        }
        if (correct != "")
        {
            correct = correct.Substring(0, correct.Length - 1);
        }
        if (incorrect != "")
        {
            incorrect = incorrect.Substring(0, incorrect.Length - 1);
        }
        return "{\"data\":[{\"correct\":[" + correct + "]},{\"incorrect\":[" + incorrect + "]},{\"missed\":[" + missed + "]}]}";
    }


    public string buildJSONQuiz()
    {
        string correct = "", missed = "", incorrect = "";


        foreach (QuizData item in QuizData.elementArray)
        {
            Debug.Log(item.ToString());
            switch (item.state)
            {
                case QuizData.State.Correct:

                    correct += "{\"element\":\" " + item.getQuestionText() + "\", \"duration\":" + item.getDuration() + " },";
                    break;

                case QuizData.State.Incorrect:

                    incorrect += "{\"element\":\" " + item.getQuestionText() + "\", \"duration\":" + item.getDuration() + " },";
                    break;

                case QuizData.State.Missed:

                    missed += "{\"element\":\" " + item.getQuestionText() + "\", \"duration\":" + item.getDuration() + " },";
                    break;
            }
        }
        QuizData.elementArray.Clear();
        if (missed != "")
        {
            missed = missed.Substring(0, missed.Length - 1);
        }
        if (correct != "")
        {
            correct = correct.Substring(0, correct.Length - 1);
        }
        if (incorrect != "")
        {
            incorrect = incorrect.Substring(0, incorrect.Length - 1);
        }
        return "{\"data\":[{\"correct\":[" + correct + "]},{\"incorrect\":[" + incorrect + "]},{\"missed\":[" + missed + "]}]}";
    }

    
    public string buildJSONDraggable()
    {
        string correct = "", missed = "", incorrect = "";


        foreach (DraggableGameData item in DraggableGameData.elementArray)
        {
            Debug.Log(item.ToString());
            switch (item.state)
            {
                case DraggableGameData.State.Correct:

                    correct += "{\"element\":\" " + item.elements + "\", \"duration\":" + 0.ToString() + " },";
                    break;

                case DraggableGameData.State.Incorrect:

                    incorrect += "{\"element\":\" " + item.elements + "\", \"duration\":" + 0.ToString() + " },";
                    break;

            }
        }
        DraggableGameData.elementArray.Clear();
        if (missed != "")
        {
            missed = missed.Substring(0, missed.Length - 1);
        }
        if (correct != "")
        {
            correct = correct.Substring(0, correct.Length - 1);
        }
        if (incorrect != "")
        {
            incorrect = incorrect.Substring(0, incorrect.Length - 1);
        }
        return "{\"data\":[{\"correct\":[" + correct + "]},{\"incorrect\":[" + incorrect + "]},{\"missed\":[" + missed + "]}]}";
    }
}
