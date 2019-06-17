using UnityEngine;
using System.Collections;
using System;
using UnityEngine.Networking;
using System.Collections.Generic;

public class rank_manager : MonoBehaviour
{
    score_rank_item[] rank_items;
    int[] sort_scores;
    string[] sort_names;
    bool[] name_assigned;
    int child_count;
    game_master my_game_master;
    public void CallHighScore()
    {
        StartCoroutine(GetHighScore());
    }



    public IEnumerator GetHighScore()
    {
        Debug.Log("Testing");
        using (UnityWebRequest www = UnityWebRequest.Get("https://www.rrmi.co/api/highscore"))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("From the server " + www.downloadHandler.text);
                test(www.downloadHandler.text);
            }
        }
    }
    void Start()
    {
        CallHighScore();
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






        if (game_master.game_master_obj)
            my_game_master = (game_master)game_master.game_master_obj.GetComponent("game_master");
        //create and arrays
        child_count = this.transform.childCount;
        rank_items = new score_rank_item[child_count];
        sort_scores = new int[child_count];
        sort_names = new string[child_count];
        name_assigned = new bool[child_count];
        //load data

        //Debug.Log(HighScoreJSON.CreateFromJSON(highScores).score);
        //Debug.Log(HighScoreJSON.CreateFromJSON(highScores).user);


        /*
        ONCE WE HAVE THE ARRAYLIST JUST PASS THE INDEX OF EACH OBJECTS FIELD

         */
        my_game_master.number_of_save_profile_slot_avaibles = score.scores.Count;

        for (int i = 0; i < score.scores.Count; i++)
        {                                                       //instead of PlayerPrefs...get the data from Earl's server.
            my_game_master.best_int_score_for_current_player[i] = score.scores[i].score;
            my_game_master.profile_name[i] = score.scores[i].user;
            // Debug.Log("["+i+"] originale: " + my_game_master.best_int_score_for_current_player[i] + " " + my_game_master.profile_name[i] + " ... " + my_game_master.this_profile_have_a_save_state_in_it[i]);
            // Debug.Log("["+i+"] copia: " + sort_scores[i]);
        }

        //fill arrays
        Array.Copy(my_game_master.best_int_score_for_current_player, sort_scores, my_game_master.number_of_save_profile_slot_avaibles);
        Array.Sort(sort_scores);
        Array.Reverse(sort_scores);
        /*
        for (int i = 0; i < my_game_master.number_of_save_profile_slot_avaibles; i++)
        {
            //Debug.Log("["+i+"] originale: " + my_game_master.best_int_score_for_current_player[i]);
            Debug.Log("["+i+"] copia riordinata: " + sort_scores[i]);
        }*/
        Debug.Log("CHILD COUNT ---> " + child_count);
        for (int i = 0; i < child_count; i++)
            rank_items[i] = (score_rank_item)this.transform.GetChild(i).GetComponent<score_rank_item>();
        for (int i = 0; i < my_game_master.number_of_save_profile_slot_avaibles; i++)
        {
            if (i < my_game_master.number_of_save_profile_slot_avaibles && my_game_master.this_profile_have_a_save_state_in_it[i])//is there is a save profile here
            {
                for (int n = 0; n < child_count; n++)
                {
                    if (my_game_master.best_int_score_for_current_player[i] == sort_scores[n] && !name_assigned[n])
                    {
                        //Debug.Log(sort_scores[n] + " == " + my_game_master.best_int_score_for_current_player[i] + " : " + my_game_master.profile_name[i]);
                        sort_names[n] = my_game_master.profile_name[i];
                        name_assigned[n] = true;
                        break;
                    }
                }
            }
        }

        Update_local(score);
    }


    /*     public void Update_local()
        {
            int slot_skipped = 0;
            for (int i = 0; i < child_count; i++)
            {
                //show only the profile slot avaibles
                if (i < my_game_master.number_of_save_profile_slot_avaibles && name_assigned[i])
                    this.transform.GetChild(i).gameObject.SetActive(true);
                else
                {
                    this.transform.GetChild(i).gameObject.SetActive(false);
                    slot_skipped++;
                }
                rank_items[i].Update_me(i + 1 - slot_skipped, sort_names[i], sort_scores[i]);
            }
        } */

    public void Update_local(Scores s)
    {

        for (int i = 0; i < child_count; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(true);
            rank_items[i].Update_me(i + 1, s.scores[i].user.Split('@')[0], s.scores[i].score);
        }
    }
}


