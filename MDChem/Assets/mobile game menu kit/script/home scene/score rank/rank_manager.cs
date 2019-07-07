using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;
using System.Collections.Generic;

public class rank_manager : MonoBehaviour
{
    public score_rank_item[] rank_items;

    public void CallHighScore()
    {
        StartCoroutine(GetHighScore());
    }

    void Awake()
    {
        for (int i = 0; i < rank_items.Length; i++)
        {
            rank_items[i].name_text.text = "";
            rank_items[i].rank_number_text.text = "";
            rank_items[i].score_text.text = "";
        }
    }
    void Start()
    {
        CallHighScore();
    }

    public IEnumerator GetHighScore()
    {
        Debug.Log("Testing");
        using (UnityWebRequest www = UnityWebRequest.Get("https://www.mdchem.app/api/highscore"))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                for (int i = 0; i < rank_items.Length; i++)
                {
                    rank_items[i].name_text.text = "Error";
                    rank_items[i].rank_number_text.text = "Error";
                    rank_items[i].score_text.text = "Error";
                }
                this.GetComponent<UiCanvasFader>().FadeIn();
            }
            else
            {
                Debug.Log("From the server " + www.downloadHandler.text);
                test(www.downloadHandler.text);
                this.GetComponent<UiCanvasFader>().FadeIn();
                //possibly cache data??
            }
        }
    }

    void test(String data)
    {
        Debug.Log("THE DATA PASSED IN ---> " + data);
        Scores score = JsonUtility.FromJson<Scores>(data);
        Debug.Log("The list --> " + score.scores.Count);

        foreach (LeaderBoardEntries entries in score.scores)
        {
            Debug.Log("The data is ---> score " + entries.score + " user ---> " + entries.user);
        }

        /* we have the users inorder in score.scores so now we just need to update the ui */

        for (int i = 0; i < rank_items.Length; i++)
        {
            rank_items[i].name_text.text = score.scores[i].user.Split('@')[0];
            rank_items[i].rank_number_text.text = (i + 1).ToString();
            rank_items[i].score_text.text = score.scores[i].score.ToString();
        }
    }
}


