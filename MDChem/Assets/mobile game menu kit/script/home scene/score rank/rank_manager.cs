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
    public string highScores = "test";
    public void CallHighScore()
    {
        StartCoroutine(GetHighScore());
    }

    public IEnumerator GetHighScore()
    {
        Debug.Log("Testing");
        using (UnityWebRequest www = UnityWebRequest.Get("http://68.183.111.180/api/highscore"))
        {
            yield return www.SendWebRequest();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("From the server " + www.downloadHandler.text);
                highScores = www.downloadHandler.text;
                Debug.Log("Web data " + highScores);
                test();
            }
        }
    }
    void Start()
    {
        CallHighScore();
    }

    void test()
    {
		/*
		so far we have the highScores json as a string
		 */
/*         Debug.Log("HighScore ---> " + highScores);
        highScores = highScores.Substring(1, highScores.Length - 1);
        Debug.Log("AFTER THE SUB " + highScores);
        char[] args = { ',', '{' }; */
		/*
		split method is broken logic needs to be redone
		 */
/*         string[] str = highScores.Split(args);
        for (int j = 0; j < str.Length; j++)
        {
            Debug.Log(str[j]);
        } */
		highScores = "{\"score\": 300,\"user\": \"jason@email.com\"}";
        List<HighScoreJSON> list = new List<HighScoreJSON>();

/*         for (int i = 0; i < str.Length; i++)
        {
            Debug.Log(str[i]);
            if (str[i][0].Equals("{"))
            {
                str[i] = "{" + str[i];
            } */
			/*
			serialize each json array memeber to a HighScoreJSON
			test with real json
			 */
            //list.Add(HighScoreJSON.CreateFromJSON(str[i]));
        //}
		list.Add(HighScoreJSON.CreateFromJSON(highScores));
		
		/*
		
		 */

        foreach (HighScoreJSON h in list)
        {
            Debug.Log(h.score + " " + h.user);
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
	 //my_game_master.number_of_save_profile_slot_avaibles
	 Debug.Log(list.Count);
        for (int i = 0; i < list.Count; i++)
        {                                                       //instead of PlayerPrefs...get the data from Earl's server.
            my_game_master.best_int_score_for_current_player[i] = list[i].score;
            my_game_master.profile_name[i] = list[i].user;
            //Debug.Log("["+i+"] originale: " + my_game_master.best_int_score_for_current_player[i] + " " + my_game_master.profile_name[i] + " ... " + my_game_master.this_profile_have_a_save_state_in_it[i]);
            //Debug.Log("["+i+"] copia: " + sort_scores[i]);
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

        Update_local();
    }


    public void Update_local()
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
    }
}


