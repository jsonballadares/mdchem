using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;


/*
This class handles anything that deals with dialog windows in the game
 */
public class DialogManager : MonoBehaviour
{
    public GameObject loginScreen;

    /*
    deletes a local profile aka clears cache and player prefs
    so it takes the user to the login screen
     */
    public void deleteProfile()
    {
        PlayerPrefs.DeleteAll();
        UnityWebRequest.ClearCookieCache();
        loginScreen.SetActive(true);
    }

    /*
    Will play the button noise for when a button is clicked
     */
    public void playButtonNoise()
    {
        if (GameObject.FindGameObjectWithTag("AudioManager"))
            FindObjectOfType<AudioManager>().Play("buttonnoise");
    }

    /*
    This is the code that is ran when the finish button is pressed
     */
    public void onFinishGameButtonPress()
    {
        if (GameObject.FindGameObjectWithTag("RandomQuizNumbers"))
            FindObjectOfType<RandomQuizNumbers>().Destroy();

        FindObjectOfType<SceneFader>().FadeTo("LevelSelector");
    }

    /*
    This code will handle all the dialogs that have the level selector button
    which will take the user back to the level selector scene. Dependent on which
    scene this code is triggered from various things need to happen like sending 
    data saving stuff etc
     */
    public void onLevelSelectorButtonPress()
    {
        playButtonNoise();

        /*
        if at end of level that matters fort score save it 
         */

        if (SceneManager.GetActiveScene().name.Equals("Level1c"))
        {
            Timer.timeLeft = 30;
            PlayerPrefs.SetInt("Level2", 1);
            int score = 0;
            if (GameObject.FindGameObjectWithTag("ScoreManager") != null)
            {
                FindObjectOfType<ScoreManager>().addToScore(Score.scoreValue);
                score = FindObjectOfType<ScoreManager>().getScore();
                Debug.Log("THE SCORE FOR LEVEL 2 IS ---> " + FindObjectOfType<ScoreManager>().getScore());
            }
            
            if (PlayerPrefs.GetInt("Level1_score") < score)
            {
                PlayerPrefs.SetInt("Level1_score", score);

            }

            Drag d = new Drag("1a", score);
            StartCoroutine(WebRequestManager.sendData(Enviorment.URL + "/api/player/", d.toJSON()));
            Debug.Log("DA JSON ------> " + d.toJSON());
            Drag.clearArrays();



            if (GameObject.FindGameObjectWithTag("ScoreManager") != null)
            {
                FindObjectOfType<ScoreManager>().zeroScore();
                FindObjectOfType<ScoreManager>().Destroy();
            }


        }
        else if (SceneManager.GetActiveScene().name.Equals("Level2c"))
        {
            Timer.timeLeft = 30;
            PlayerPrefs.SetInt("Level3", 1);
            int score = 0;
            if (GameObject.FindGameObjectWithTag("ScoreManager") != null)
            {
                FindObjectOfType<ScoreManager>().addToScore(Score.scoreValue);
                score = FindObjectOfType<ScoreManager>().getScore();
                Debug.Log("THE SCORE FOR LEVEL 2 IS ---> " + score);
            }


            Drag d = new Drag("2a", score);
            StartCoroutine(WebRequestManager.sendData(Enviorment.URL + "/api/player/", d.toJSON()));
            Debug.Log("DA JSON ------> " + d.toJSON());
            Drag.clearArrays();
            if (PlayerPrefs.GetInt("Level2_score") < score)
            {
                PlayerPrefs.SetInt("Level2_score", score);
            }
            if (GameObject.FindGameObjectWithTag("ScoreManager") != null)
            {
                FindObjectOfType<ScoreManager>().zeroScore();
                FindObjectOfType<ScoreManager>().Destroy();
            }

        }
        else if (SceneManager.GetActiveScene().name.Equals("Level3c"))
        {
            Timer.timeLeft = 30;
            PlayerPrefs.SetInt("Level4", 1);
            int score = 0;
            if (GameObject.FindGameObjectWithTag("ScoreManager") != null)
            {
                FindObjectOfType<ScoreManager>().addToScore(Score.scoreValue);
                score = FindObjectOfType<ScoreManager>().getScore();
                Debug.Log("THE SCORE FOR LEVEL 3 IS ---> " + score);
            }
            if (PlayerPrefs.GetInt("Level3_score") < score)
            {
                PlayerPrefs.SetInt("Level3_score", score);
            }


            Drag d = new Drag("3a", score);
            StartCoroutine(WebRequestManager.sendData(Enviorment.URL + "/api/player/", d.toJSON()));
            Debug.Log("DA JSON ------> " + d.toJSON());
            Drag.clearArrays();

            if (GameObject.FindGameObjectWithTag("ScoreManager") != null)
            {
                FindObjectOfType<ScoreManager>().zeroScore();
                FindObjectOfType<ScoreManager>().Destroy();
            }

        }
        else if (SceneManager.GetActiveScene().name.Equals("Level4c"))
        {
            Timer.timeLeft = 30;
            PlayerPrefs.SetInt("Level5", 1);
            int score = 0;
            if (GameObject.FindGameObjectWithTag("ScoreManager") != null)
            {
                FindObjectOfType<ScoreManager>().addToScore(Score.scoreValue);
                score = FindObjectOfType<ScoreManager>().getScore();
                Debug.Log("THE SCORE FOR LEVEL 4 IS ---> " + score);
            }

            Drag d = new Drag("4a", score);
            StartCoroutine(WebRequestManager.sendData(Enviorment.URL + "/api/player/", d.toJSON()));
            Debug.Log("DA JSON ------> " + d.toJSON());
            Drag.clearArrays();
            if (PlayerPrefs.GetInt("Level4_score") < score)
            {
                PlayerPrefs.SetInt("Level4_score", score);
            }
            if (GameObject.FindGameObjectWithTag("ScoreManager") != null)
            {
                FindObjectOfType<ScoreManager>().zeroScore();
                FindObjectOfType<ScoreManager>().Destroy();
            }

        }
        else if (SceneManager.GetActiveScene().name.Equals("Level6b"))
        {
            if (GameObject.FindGameObjectWithTag("QuizCounter"))
            {
                FindObjectOfType<QuizCounter>().Destroy();
            }
            PlayerPrefs.SetInt("Level7", 1);

        }
        else if (SceneManager.GetActiveScene().name.Equals("Level7c"))
        {
            if (GameObject.FindGameObjectWithTag("QuizCounter"))
            {
                FindObjectOfType<QuizCounter>().Destroy();
            }
            PlayerPrefs.SetInt("Level8", 1);

        }
        else if (SceneManager.GetActiveScene().name.Equals("Level9c"))
        {
            if (GameObject.FindGameObjectWithTag("QuizCounter"))
            {
                FindObjectOfType<QuizCounter>().Destroy();
            }
            PlayerPrefs.SetInt("Level10", 1);

        }
        else if (SceneManager.GetActiveScene().name.Equals("Level10") || SceneManager.GetActiveScene().name.Equals("Level8") || SceneManager.GetActiveScene().name.Equals("Level5"))
        {
            DragManager.rightCounter = 0;
            DragManager.wrongCounter = 0;
            QuizManager.count = 0;
            Debug.Log("GOING BACK TO LEVEL SELECTOR BUT CLEARING THE LIST");
            QuizManager.clearQuestionsList();
            Debug.Log("CLEARED LIST");
            if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
            {
                FindObjectOfType<QuizCounter>().Destroy();
            }
            if (GameObject.FindGameObjectWithTag("RandomQuizNumbers") != null)
            {
                FindObjectOfType<RandomQuizNumbers>().Destroy();
            }

        }

        if (SceneManager.GetActiveScene().name.Contains("Level1") || SceneManager.GetActiveScene().name.Contains("Level2") || SceneManager.GetActiveScene().name.Contains("Level3") || SceneManager.GetActiveScene().name.Contains("Level4"))
        {
            Timer.timeLeft = 30;
            if (GameObject.FindGameObjectWithTag("ScoreManager") != null)
            {

                FindObjectOfType<ScoreManager>().Destroy();
            }
            // FindObjectOfType<AudioManager>().Stop("captainscurvy");
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level5") || SceneManager.GetActiveScene().name.Equals("Level8"))
        {
            if (GameObject.FindGameObjectWithTag("QuizCounter"))
            {
                FindObjectOfType<QuizCounter>().Destroy();
            }
            //FindObjectOfType<AudioManager>().Stop("masterylevelmusic");
        }
        else if (SceneManager.GetActiveScene().name.Contains("Level6") || SceneManager.GetActiveScene().name.Contains("Level7") || SceneManager.GetActiveScene().name.Contains("Level9") || SceneManager.GetActiveScene().name.Equals("Level10") || SceneManager.GetActiveScene().name.Contains("Beginner"))
        {
            if (GameObject.FindGameObjectWithTag("QuizCounter"))
            {
                FindObjectOfType<QuizCounter>().Destroy();
            }
            if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
            {
                FindObjectOfType<QuizCounter>().Destroy();
            }
            if (GameObject.FindGameObjectWithTag("RandomQuizNumbers") != null)
            {
                FindObjectOfType<RandomQuizNumbers>().Destroy();
            }
            //FindObjectOfType<AudioManager>().Stop("quizgamenoise");
        }

        if (SceneManager.GetActiveScene().name.Equals("BeginnerLevel1d"))
        {
            if (GameObject.FindGameObjectWithTag("QuizCounter"))
            {
                FindObjectOfType<QuizCounter>().Destroy();
            }
            FindObjectOfType<AudioManager>().Stop("quizgamenoise");
            PlayerPrefs.SetInt("Level2", 1); //unlocks level2
            FindObjectOfType<SceneFader>().FadeTo("LevelTwoVideoAnimation");
        }
        else if (SceneManager.GetActiveScene().name.Equals("BeginnerLevel2d"))
        {
            if (GameObject.FindGameObjectWithTag("QuizCounter"))
            {
                FindObjectOfType<QuizCounter>().Destroy();
            }
            FindObjectOfType<AudioManager>().Stop("quizgamenoise");
            PlayerPrefs.SetInt("Level3", 1); //unlocks level3
            FindObjectOfType<SceneFader>().FadeTo("LevelThreeVideoAnimation");
        }
        else if (SceneManager.GetActiveScene().name.Equals("BeginnerLevel3d"))
        {
            if (GameObject.FindGameObjectWithTag("QuizCounter"))
            {
                FindObjectOfType<QuizCounter>().Destroy();
            }
            FindObjectOfType<AudioManager>().Stop("quizgamenoise");
            PlayerPrefs.SetInt("Level4", 1); //unlocks level3
            FindObjectOfType<SceneFader>().FadeTo("LevelFourVideoAnimation");
        }
        else if (SceneManager.GetActiveScene().name.Equals("BeginnerLevel4d"))
        {
            if (GameObject.FindGameObjectWithTag("QuizCounter"))
            {
                FindObjectOfType<QuizCounter>().Destroy();
            }
            FindObjectOfType<AudioManager>().Stop("quizgamenoise");
            PlayerPrefs.SetInt("Level5", 1); //unlocks level3
            FindObjectOfType<SceneFader>().FadeTo("Level5");
        }


        FindObjectOfType<SceneFader>().FadeTo("LevelSelector");
    }

    public void onContinueButtonPress()
    {
        FindObjectOfType<AudioManager>().Play("buttonnoise");

        if (SceneManager.GetActiveScene().name.Equals("Level1a"))
        {
            if (GameObject.FindGameObjectWithTag("ScoreManager") != null)
            {
                FindObjectOfType<ScoreManager>().addToScore(Score.scoreValue);
                Debug.Log("THE 1a SCORE IS ---> " + FindObjectOfType<ScoreManager>().getScore());
            }

            Timer.timeLeft = 30;
            FindObjectOfType<SceneFader>().FadeTo("Level1b");
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level1b"))
        {
            if (GameObject.FindGameObjectWithTag("ScoreManager") != null)
            {
                FindObjectOfType<ScoreManager>().addToScore(Score.scoreValue);
                Debug.Log("THE 1b SCORE IS ---> " + FindObjectOfType<ScoreManager>().getScore());
            }

            Timer.timeLeft = 30;
            FindObjectOfType<SceneFader>().FadeTo("Level1c");

        }
        else if (SceneManager.GetActiveScene().name.Equals("Level1c"))
        {
            PlayerPrefs.SetInt("Level2", 1);
            int score = 0;
            if (GameObject.FindGameObjectWithTag("ScoreManager") != null)
            {
                FindObjectOfType<ScoreManager>().addToScore(Score.scoreValue);
                score = FindObjectOfType<ScoreManager>().getScore();
                Debug.Log("THE 1c SCORE IS ---> " + score);
            }


            Timer.timeLeft = 30;
            FindObjectOfType<AudioManager>().Stop("captainscurvy");


            if (PlayerPrefs.GetInt("Level1_score") < score)
            {
                PlayerPrefs.SetInt("Level1_score", score);
            }


            // if (GameObject.FindGameObjectWithTag("WebRequestManager") != null)
            // {
            //     Dictionary<string, string> d = new Dictionary<string, string>();
            //     d.Add("score", score.ToString());
            //     d.Add("uuid", PlayerPrefs.GetString("ui"));
            //     d.Add("levelid", "1");
            //     FindObjectOfType<WebRequest>().PostData(d, FindObjectOfType<WebRequest>().buildJSON());

            // }
            Drag d = new Drag("1a", score);
            StartCoroutine(WebRequestManager.sendData(Enviorment.URL + "/api/player/", d.toJSON()));
            Debug.Log("DA JSON ------> " + d.toJSON());
            Drag.clearArrays();


            if (GameObject.FindGameObjectWithTag("ScoreManager") != null)
            {
                FindObjectOfType<ScoreManager>().zeroScore();
                FindObjectOfType<ScoreManager>().Destroy();
            }


            FindObjectOfType<SceneFader>().FadeTo("LevelTwoVideoAnimation");
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level2a"))
        {
            if (GameObject.FindGameObjectWithTag("ScoreManager") != null)
            {

                FindObjectOfType<ScoreManager>().addToScore(Score.scoreValue);
                Debug.Log("THE 2A SCORE IS ---> " + FindObjectOfType<ScoreManager>().getScore());
            }

            Timer.timeLeft = 30;
            FindObjectOfType<SceneFader>().FadeTo("Level2b");
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level2b"))
        {
            if (GameObject.FindGameObjectWithTag("ScoreManager") != null)
            {
                FindObjectOfType<ScoreManager>().addToScore(Score.scoreValue);
                Debug.Log("THE 2b SCORE IS ---> " + FindObjectOfType<ScoreManager>().getScore());
            }

            Timer.timeLeft = 30;
            FindObjectOfType<SceneFader>().FadeTo("Level2c");
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level2c"))
        {
            PlayerPrefs.SetInt("Level3", 1);
            int score = 0;
            if (GameObject.FindGameObjectWithTag("ScoreManager") != null)
            {
                FindObjectOfType<ScoreManager>().addToScore(Score.scoreValue);
                score = FindObjectOfType<ScoreManager>().getScore();
                Debug.Log("THE 2c SCORE IS ---> " + score);
            }

            Timer.timeLeft = 30;
            FindObjectOfType<AudioManager>().Stop("captainscurvy");
            if (PlayerPrefs.GetInt("Level2_score") < score)
            {
                PlayerPrefs.SetInt("Level2_score", score);
            }

            // if (GameObject.FindGameObjectWithTag("WebRequestManager") != null)
            // {
            //     Dictionary<string, string> d = new Dictionary<string, string>();
            //     d.Add("score", score.ToString());
            //     d.Add("uuid", PlayerPrefs.GetString("ui"));
            //     d.Add("levelid", "2");
            //     FindObjectOfType<WebRequest>().PostData(d, FindObjectOfType<WebRequest>().buildJSON());

            // }
            Drag d = new Drag("2a", score);
            StartCoroutine(WebRequestManager.sendData(Enviorment.URL + "/api/player/", d.toJSON()));
            Debug.Log("DA JSON ------> " + d.toJSON());
            Drag.clearArrays();
            if (GameObject.FindGameObjectWithTag("ScoreManager") != null)
            {
                FindObjectOfType<ScoreManager>().zeroScore();
                FindObjectOfType<ScoreManager>().Destroy();
            }


            FindObjectOfType<SceneFader>().FadeTo("LevelThreeVideoAnimation");
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level3a"))
        {
            if (GameObject.FindGameObjectWithTag("ScoreManager") != null)
            {
                FindObjectOfType<ScoreManager>().addToScore(Score.scoreValue);
                Debug.Log("THE 3a SCORE IS ---> " + FindObjectOfType<ScoreManager>().getScore());
            }

            Timer.timeLeft = 30;
            FindObjectOfType<SceneFader>().FadeTo("Level3b");
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level3b"))
        {
            if (GameObject.FindGameObjectWithTag("ScoreManager") != null)
            {
                FindObjectOfType<ScoreManager>().addToScore(Score.scoreValue);
                Debug.Log("THE 3b SCORE IS ---> " + FindObjectOfType<ScoreManager>().getScore());
            }
            Timer.timeLeft = 30;
            FindObjectOfType<SceneFader>().FadeTo("Level3c");
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level3c"))
        {
            PlayerPrefs.SetInt("Level4", 1);
            int score = 0;
            if (GameObject.FindGameObjectWithTag("ScoreManager") != null)
            {
                FindObjectOfType<ScoreManager>().addToScore(Score.scoreValue);
                score = FindObjectOfType<ScoreManager>().getScore();
                Debug.Log("THE 3c SCORE IS ---> " + score);

            }
            Timer.timeLeft = 30;
            FindObjectOfType<AudioManager>().Stop("captainscurvy");

            if (PlayerPrefs.GetInt("Level3_score") < score)
            {
                PlayerPrefs.SetInt("Level3_score", score);
            }

            // if (GameObject.FindGameObjectWithTag("WebRequestManager") != null)
            // {
            //     Dictionary<string, string> d = new Dictionary<string, string>();
            //     d.Add("score", score.ToString());
            //     d.Add("uuid", PlayerPrefs.GetString("ui"));
            //     d.Add("levelid", "3");
            //     FindObjectOfType<WebRequest>().PostData(d, FindObjectOfType<WebRequest>().buildJSON());

            // }
            Drag d = new Drag("3a", score);
            StartCoroutine(WebRequestManager.sendData(Enviorment.URL + "/api/player/", d.toJSON()));
            Debug.Log("DA JSON ------> " + d.toJSON());
            Drag.clearArrays();
            if (GameObject.FindGameObjectWithTag("ScoreManager") != null)
            {
                FindObjectOfType<ScoreManager>().zeroScore();
                FindObjectOfType<ScoreManager>().Destroy();
            }


            FindObjectOfType<SceneFader>().FadeTo("LevelFourVideoAnimation");
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level4a"))
        {
            if (GameObject.FindGameObjectWithTag("ScoreManager") != null)
            {
                FindObjectOfType<ScoreManager>().addToScore(Score.scoreValue);
                Debug.Log("THE 4a SCORE IS ---> " + FindObjectOfType<ScoreManager>().getScore());
            }
            Timer.timeLeft = 30;
            FindObjectOfType<SceneFader>().FadeTo("Level4b");
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level4b"))
        {
            if (GameObject.FindGameObjectWithTag("ScoreManager") != null)
            {
                FindObjectOfType<ScoreManager>().addToScore(Score.scoreValue);
                Debug.Log("THE 4b SCORE IS ---> " + FindObjectOfType<ScoreManager>().getScore());
            }
            Timer.timeLeft = 30;
            FindObjectOfType<SceneFader>().FadeTo("Level4c");
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level4c"))
        {
            PlayerPrefs.SetInt("Level5", 1);
            int score = 0;
            if (GameObject.FindGameObjectWithTag("ScoreManager") != null)
            {
                FindObjectOfType<ScoreManager>().addToScore(Score.scoreValue);
                score = FindObjectOfType<ScoreManager>().getScore();
                Debug.Log("THE 4c SCORE IS ---> " + score);
            }

            // if (GameObject.FindGameObjectWithTag("WebRequestManager") != null)
            // {
            //     Dictionary<string, string> d = new Dictionary<string, string>();
            //     d.Add("score", score.ToString());
            //     d.Add("uuid", PlayerPrefs.GetString("ui"));
            //     d.Add("levelid", "4");
            //     FindObjectOfType<WebRequest>().PostData(d, FindObjectOfType<WebRequest>().buildJSON());

            // }
            Drag d = new Drag("4a", score);
            StartCoroutine(WebRequestManager.sendData(Enviorment.URL + "/api/player/", d.toJSON()));
            Debug.Log("DA JSON ------> " + d.toJSON());
            Drag.clearArrays();
            if (PlayerPrefs.GetInt("Level4_score") < score)
            {
                PlayerPrefs.SetInt("Level4_score", score);
            }
            Timer.timeLeft = 30;
            FindObjectOfType<AudioManager>().Stop("captainscurvy");


            if (GameObject.FindGameObjectWithTag("ScoreManager") != null)
            {
                FindObjectOfType<ScoreManager>().zeroScore();
                FindObjectOfType<ScoreManager>().Destroy();
            }
            FindObjectOfType<SceneFader>().FadeTo("Level5");
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level6b"))
        {
            if (GameObject.FindGameObjectWithTag("QuizCounter"))
            {
                FindObjectOfType<QuizCounter>().Destroy();
            }
            FindObjectOfType<AudioManager>().Stop("quizgamenoise");
            PlayerPrefs.SetInt("Level7", 1);
            FindObjectOfType<SceneFader>().FadeTo("LevelSevenVideoAnimation");
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level7c"))
        {
            if (GameObject.FindGameObjectWithTag("QuizCounter"))
            {
                FindObjectOfType<QuizCounter>().Destroy();
            }
            FindObjectOfType<AudioManager>().Stop("quizgamenoise");
            PlayerPrefs.SetInt("Level8", 1);

            FindObjectOfType<SceneFader>().FadeTo("Level8");
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level9b"))
        {

            FindObjectOfType<SceneFader>().FadeTo("Level9c");

        }
        else if (SceneManager.GetActiveScene().name.Equals("Level9c"))
        {
            if (GameObject.FindGameObjectWithTag("QuizCounter"))
            {
                FindObjectOfType<QuizCounter>().Destroy();
            }
            FindObjectOfType<AudioManager>().Stop("quizgamenoise");
            PlayerPrefs.SetInt("Level10", 1);
            FindObjectOfType<SceneFader>().FadeTo("LevelTenVideoAnimation");
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level10"))
        {
            FindObjectOfType<SceneFader>().FadeTo("Level10");
        }
        else if (SceneManager.GetActiveScene().name.Equals("BeginnerLevel1d"))
        {
            if (GameObject.FindGameObjectWithTag("QuizCounter"))
            {
                FindObjectOfType<QuizCounter>().Destroy();
            }
            FindObjectOfType<AudioManager>().Stop("quizgamenoise");
            PlayerPrefs.SetInt("Level2", 1); //unlocks level2
            FindObjectOfType<SceneFader>().FadeTo("LevelTwoVideoAnimation");
        }
        else if (SceneManager.GetActiveScene().name.Equals("BeginnerLevel2d"))
        {
            if (GameObject.FindGameObjectWithTag("QuizCounter"))
            {
                FindObjectOfType<QuizCounter>().Destroy();
            }
            FindObjectOfType<AudioManager>().Stop("quizgamenoise");
            PlayerPrefs.SetInt("Level3", 1); //unlocks level3
            FindObjectOfType<SceneFader>().FadeTo("LevelThreeVideoAnimation");
        }
        else if (SceneManager.GetActiveScene().name.Equals("BeginnerLevel3d"))
        {
            if (GameObject.FindGameObjectWithTag("QuizCounter"))
            {
                FindObjectOfType<QuizCounter>().Destroy();
            }
            FindObjectOfType<AudioManager>().Stop("quizgamenoise");
            PlayerPrefs.SetInt("Level4", 1); //unlocks level3
            FindObjectOfType<SceneFader>().FadeTo("LevelFourVideoAnimation");
        }
        else if (SceneManager.GetActiveScene().name.Equals("BeginnerLevel4d"))
        {
            if (GameObject.FindGameObjectWithTag("QuizCounter"))
            {
                FindObjectOfType<QuizCounter>().Destroy();
            }
            FindObjectOfType<AudioManager>().Stop("quizgamenoise");
            PlayerPrefs.SetInt("Level5", 1); //unlocks level3
            FindObjectOfType<SceneFader>().FadeTo("Level5");
        }

    }



}


