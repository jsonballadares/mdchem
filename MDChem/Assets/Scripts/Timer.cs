using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

/*
This script handles the timer as soon as the game starts the timer starts counting
dependent on the time the game will branch out to various events foo.
The game mode is a hybrid of time driven and score as the time is constantly counting down 
but you have to meet the threshold of 100 score in order to progress to the next sublevel
 */

public class Timer : MonoBehaviour
{
    private bool myFunctionCalled = false;
    public GameObject dialogBox;
    public static float timeLeft = 30;
    Text countdownText;
    public static int counter = 0;
    int scoreThreshold = 100;

    void Start()
    {
        Debug.Log("Timer Start() is being called re initializing the counter ");
        Timer.timeLeft = 30;
        countdownText = GetComponent<Text>();
        StartCoroutine("SubtractTime");
        
    }
    void Update()
    {
        countdownText.text = ("Time Left: " + timeLeft);

        if (timeLeft <= 0)
        {
            if (Score.scoreValue < scoreThreshold)
            {
                timeLeft = 15;
            }
            else
            {
                //once this point is reached this means the player has reached the threshold of 100 points
                //therefore they may progress to the next sublevel
                StopCoroutine("SubtractTime");
                if(!myFunctionCalled){
                    myFunctionCalled = true;
                    DialogPopUp();
                }

                //StartCoroutine("ChangeScene");
            }
        }
    }

    /*
	Once the thresholds of time and score is reached the game needs to change the scene but we want to wait a couple 
	of seconds to let the player finish. It also sets up the next scene and keeps track of the which scenes have been played. 
	 */
    //TODO: dependent on the level selection and flow of the general ui this may need to be tweaked and played with 
    //also maybe load the next scene asynchrously in the background, maybe a loading screen inbetween the loads
    //the way of keeping track of the level maybe better suited with enumerators

    IEnumerator ChangeScene()
    {
        Debug.Log(buildJSON(Element.elementArray));
        //clear the array for next scene
        //pause execution for 3 seconds
        yield return new WaitForSeconds(3);
        SceneChanger();
    }

    private string buildJSON(ArrayList l)
    {

        string correct = "", missed = "", incorrect = "";

        foreach (Element item in l)
        {
            switch (item.state)
            {
                case Element.State.Correct:

                    if (correct.Equals(""))
                    {
                        //not formatted correctly
                        //correct += “{\“element\“:\” ” + item.getName() + “\”, \“duration\“: \"” + item.getDuration() + ""\ }“;
                    }
                    else
                    {
                        correct += ",{\"element\":\" " + item.getName() + "\", \"duration\":" + item.getDuration() + " }";
                    }

                    break;
                case Element.State.Incorrect:

                    if (correct.Equals(""))
                    {
                        incorrect += "{\"element\":\" " + item.getName() + "\", \"duration\":" + item.getDuration() + " }";
                    }
                    else
                    {
                        incorrect += ",{\"element\":\" " + item.getName() + "\", \"duration\":" + item.getDuration() + " }";
                    }

                    break;
                case Element.State.Missed:
                    if (correct.Equals(""))
                    {
                        missed += "{\"element\":\" " + item.getName() + "\", \"duration\":" + item.getDuration() + " }";
                    }
                    else
                    {
                        missed += ",{\"element\":\" " + item.getName() + "\", \"duration\":" + item.getDuration() + " }";
                    }

                    break;
            }
        }

        return "{  \"data\": [    {      \"correct\": [        " + correct + "      ]    },    {      \"incorrect\": [        " + incorrect + "      ]    },    {      \"missed\": [        " + missed + "      ]    }  ]}";
    }

    /*
	This is what keeps track of the time. For each second that passes it takes one away from the current time
	This function runs in parallel with the Update();
	 */
    IEnumerator SubtractTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }

    }

    private void DialogPopUp()
    {
        Debug.Log("DialogPop has been called");
        if (!(SceneManager.GetActiveScene().name.Equals("Level1c") || SceneManager.GetActiveScene().name.Equals("Level2c") || SceneManager.GetActiveScene().name.Equals("Level3c") || SceneManager.GetActiveScene().name.Equals("Level4c")))
        {
            Debug.Log("DialogPop has been called and its if ran");
            //sublevel b end of aa
            //countdownText.text = "Next Level Is Faster! Get Ready!";
            dialogBox.SetActive(true);
            //    dialogBox.GetComponent<UiCanvasFader>().FadeIn();
        }
        else
        {
            Debug.Log("DialogPop has been called and its else ran");
            dialogBox.SetActive(true);
        }
        Spawner.stop = true;
    }

    void SceneChanger()
    {
        //once executino is over change the scene dependent upon 
        if (SceneManager.GetActiveScene().name.Equals("Level1a"))
        {
            timeLeft = 30;
            SceneManager.LoadScene("Level1b");
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level1b"))
        {
            timeLeft = 30;
            SceneManager.LoadScene("Level1c");
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level1c"))
        {

            FindObjectOfType<AudioManager>().Pause("captainscurvy");
            PlayerPrefs.SetInt("Level2", 1);

            //            if (PlayerPrefs.GetInt("levelReached") < 2)
            //            {
            //                PlayerPrefs.SetInt("levelReached", 2);
            //           }
            FindObjectOfType<SceneFader>().FadeTo("LevelSelector");

        }
        else if (SceneManager.GetActiveScene().name.Equals("Level2a"))
        {
            timeLeft = 30;
            SceneManager.LoadScene("Level2b");
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level2b"))
        {
            timeLeft = 30;
            SceneManager.LoadScene("Level2c");
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level2c"))
        {
            FindObjectOfType<AudioManager>().Pause("captainscurvy");
            if (PlayerPrefs.GetInt("levelReached") < 3)
            {
                PlayerPrefs.SetInt("levelReached", 3);
            }
            FindObjectOfType<SceneFader>().FadeTo("LevelSelector");
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level3a"))
        {
            timeLeft = 30;
            SceneManager.LoadScene("Level3b");
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level3b"))
        {
            timeLeft = 30;
            SceneManager.LoadScene("Level3c");
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level3c"))
        {
            FindObjectOfType<AudioManager>().Pause("captainscurvy");
            if (PlayerPrefs.GetInt("levelReached") < 4)
            {
                PlayerPrefs.SetInt("levelReached", 4);
            }
            FindObjectOfType<SceneFader>().FadeTo("LevelSelector");
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level4a"))
        {
            timeLeft = 30;
            SceneManager.LoadScene("Level4b");
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level4b"))
        {
            timeLeft = 30;
            SceneManager.LoadScene("Level4c");
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level4c"))
        {
            FindObjectOfType<AudioManager>().Pause("captainscurvy");
            if (PlayerPrefs.GetInt("levelReached") < 5)
            {
                PlayerPrefs.SetInt("levelReached", 5);
            }
            FindObjectOfType<SceneFader>().FadeTo("LevelSelector");
        }

    }

}
