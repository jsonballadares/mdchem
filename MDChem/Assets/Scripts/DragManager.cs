using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DragManager : MonoBehaviour
{
    public Transform dropPanel, dragPanel;
    public GameObject submitButton, endDialog, feedBackDialog, rulesDialog, correctDialog;
    public static int rightCounter = 0;
    public static int wrongCounter = 0;


    void Awake()
    {
        if (rightCounter > 0 || wrongCounter > 0)
        {
            rulesDialog.SetActive(false);
            dropPanel.gameObject.SetActive(true);
            dragPanel.gameObject.SetActive(true);
        }
    }
    private bool doesNameMatch()
    {

        int count = 0;
        for (int i = 0; i < dropPanel.childCount; i++)
        {
            if (dropPanel.GetChild(i).name.Equals(dropPanel.GetChild(i).GetChild(0).name))
            {
                count++;
            }
        }
        return count == dropPanel.childCount;
    }
    public void checkDropPanel()
    {


        if (!SceneManager.GetActiveScene().name.Contains("Level10"))
        {
            Debug.Log("Name of the current scene " + SceneManager.GetActiveScene().name);
            if (isCorrectAmount())
            {
                submitButton.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Name of the current scene " + SceneManager.GetActiveScene().name);
            if (levelTenDropPanelCheck())
            {
                submitButton.SetActive(true);
            }
        }

    }
    private bool levelTenDropPanelCheck()
    {
        return dropPanel.childCount > 1;
    }

    public void miscellaneousDialogBoxShow(string msg)
    {
        GameObject miscellaneousDialogBox = null;
        GameObject[] arr = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject x in arr)
        {
            if (x.tag.Equals("miscDialog"))
            {
                x.SetActive(true);
                miscellaneousDialogBox = x;
                miscellaneousDialogBox.SetActive(true);
                miscellaneousDialogBox.GetComponentInChildren<Text>().text = msg;
                miscellaneousDialogBox.GetComponentInChildren<UiCanvasFader>().FadeIn();
            }
        }

    }

    private bool isCorrectAmount()
    {
        int count = 0;
        for (int i = 0; i < dropPanel.childCount; i++)
        {
            if (dropPanel.GetChild(i).childCount >= 1)
            {
                count++;
            }
        }
        Debug.Log("THE COUNT --> " + count + " CHILD COUNT " + dropPanel.childCount);
        return count == dropPanel.childCount;
    }
    private bool isChargeCorrect(string charge)
    {
        int count = 0;
        for (int i = 0; i < dropPanel.childCount; i++)
        {
            Debug.Log(dropPanel.GetChild(i).GetChild(0).tag.ToString());
            if (dropPanel.GetChild(i).GetChild(0).tag.ToString().Equals(charge))
            {
                count++;
            }
        }
        return count == dropPanel.childCount;
    }

    public void ResumeGame()
    {
        SumPause.Status = false; // Set pause status to false
    }


    public void onSubmitButtonPress()
    {
        if (SceneManager.GetActiveScene().name.Equals("BeginnerLevel1a"))
        {
            //in this level we should put alkali metals on top
            if (isChargeCorrect("alkali"))
            {
                /*
                send data to server if they got everything correct
                 */
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Correct);
                DraggableGameData.elementArray.Add(d);
                //transition player to next level
                FindObjectOfType<AudioManager>().Play("truenoise");
                FindObjectOfType<SceneFader>().FadeTo("BeginnerLevel1b");
            }
            else
            {
                /*
                BUILD THE WHATS IN THE DROP PANELS CHILDREN CONCAT TOGETHER
                PUT IN INCORRECT 
                 */
                //send data to server about incorrect try
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Incorrect);
                DraggableGameData.elementArray.Add(d);
                if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
                {
                    FindObjectOfType<QuizCounter>().addToCounter();
                }
                FindObjectOfType<AudioManager>().Play("falsenoise");
                //FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);
                //miscellaneousDialogBoxShow("Nice try but try again!"); see if this is something the professor would want


            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("BeginnerLevel1b"))
        {
            //in this level we should put alkali metals only symbols
            if (isChargeCorrect("alkali"))
            {
                /*
                send data to server if they got everything correct
                 */
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Correct);
                DraggableGameData.elementArray.Add(d);
                //if all is correct move to next level
                FindObjectOfType<AudioManager>().Play("truenoise");
                FindObjectOfType<SceneFader>().FadeTo("BeginnerLevel1c");
            }
            else
            {
                /*
                BUILD THE WHATS IN THE DROP PANELS CHILDREN CONCAT TOGETHER
                PUT IN INCORRECT 
                 */
                //send data to server about incorrect try
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Incorrect);
                DraggableGameData.elementArray.Add(d);
                if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
                {
                    FindObjectOfType<QuizCounter>().addToCounter();
                }
                FindObjectOfType<AudioManager>().Play("falsenoise");
                FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);

            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("BeginnerLevel1c"))
        {
            //in this level we should put alkaline metals on top
            if (isChargeCorrect("alkaline"))
            {
                /*
                send data to server if they got everything correct
                 */
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Correct);
                DraggableGameData.elementArray.Add(d);
                //transition player to next level
                FindObjectOfType<AudioManager>().Play("truenoise");
                FindObjectOfType<SceneFader>().FadeTo("BeginnerLevel1d");
            }
            else
            {
                /*
                BUILD THE WHATS IN THE DROP PANELS CHILDREN CONCAT TOGETHER
                PUT IN INCORRECT 
                 */
                //send data to server about incorrect try
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Incorrect);
                DraggableGameData.elementArray.Add(d);
                if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
                {
                    FindObjectOfType<QuizCounter>().addToCounter();
                }
                FindObjectOfType<AudioManager>().Play("falsenoise");
                FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);

            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("BeginnerLevel1d"))
        {
            //in this level we should put alkali metals only symbols
            if (isChargeCorrect("alkaline"))
            {
                /*
                BUILD THE WHATS IN THE DROP PANELS CHILDREN CONCAT TOGETHER
                PUT IN CORRECT AND SEND 
                 */

                if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
                {
                    Debug.Log("THE CURRENT SCORE IS --> " + FindObjectOfType<QuizCounter>().getCount());
                    int points = 0;
                    if (FindObjectOfType<QuizCounter>().getCount() == 0)
                    {
                        //3stars
                        points = 500;
                        if (PlayerPrefs.GetInt("Level1_score") < points)
                        {
                            PlayerPrefs.SetInt("Level1_score", points);
                        }
                    }
                    else if (FindObjectOfType<QuizCounter>().getCount() == 1)
                    {
                        //2stars
                        points = 400;
                        if (PlayerPrefs.GetInt("Level1_score") < points)
                        {
                            PlayerPrefs.SetInt("Level1_score", points);
                        }

                    }
                    else
                    {
                        //1star
                        points = 300;
                        if (PlayerPrefs.GetInt("Level1_score") < points)
                        {
                            PlayerPrefs.SetInt("Level1_score", points);
                        }

                    }
                    string elements = "";
                    for (int i = 0; i < dropPanel.childCount; i++)
                    {
                        elements += dropPanel.GetChild(i).GetChild(0).name;
                    }
                    DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Correct);
                    DraggableGameData.elementArray.Add(d);
                    if (GameObject.FindGameObjectWithTag("WebRequestManager") != null)
                    {
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("score", points.ToString());
                        dic.Add("uuid", PlayerPrefs.GetString("ui"));
                        dic.Add("levelid", "B1"); //should we distinguish between advanced and beginner??? //B1 COULD BE HOW ASK EARL
                        FindObjectOfType<WebRequest>().PostData(dic, FindObjectOfType<WebRequest>().buildJSONDraggable());
                    }
                }
                FindObjectOfType<AudioManager>().Play("truenoise");
                endDialog.SetActive(true);
            }
            else
            {
                /*
                BUILD THE WHATS IN THE DROP PANELS CHILDREN CONCAT TOGETHER
                PUT IN INCORRECT 
                 */
                //send data to server about incorrect try
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Incorrect);
                DraggableGameData.elementArray.Add(d);
                if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
                {
                    FindObjectOfType<QuizCounter>().addToCounter();
                }
                FindObjectOfType<AudioManager>().Play("falsenoise");
                FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);

            }
        }

        else if (SceneManager.GetActiveScene().name.Equals("BeginnerLevel2a"))
        {
            //in this level we should put alkali metals on top
            if (isChargeCorrect("halogen"))
            {
                /*
                send data to server if they got everything correct
                 */
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Correct);
                DraggableGameData.elementArray.Add(d);
                //transition player to next level
                FindObjectOfType<AudioManager>().Play("truenoise");
                FindObjectOfType<SceneFader>().FadeTo("BeginnerLevel2b");
            }
            else
            {
                /*
                BUILD THE WHATS IN THE DROP PANELS CHILDREN CONCAT TOGETHER
                PUT IN INCORRECT 
                 */
                //send data to server about incorrect try
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Incorrect);
                DraggableGameData.elementArray.Add(d);
                if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
                {
                    FindObjectOfType<QuizCounter>().addToCounter();
                }
                FindObjectOfType<AudioManager>().Play("falsenoise");
                FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);

            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("BeginnerLevel2b"))
        {
            //in this level we should put alkali metals only symbols
            if (isChargeCorrect("halogen"))
            {
                /*
                send data to server if they got everything correct
                 */
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Correct);
                DraggableGameData.elementArray.Add(d);
                //if all is correct move to next level
                FindObjectOfType<AudioManager>().Play("truenoise");
                FindObjectOfType<SceneFader>().FadeTo("BeginnerLevel2c");
            }
            else
            {
                /*
                BUILD THE WHATS IN THE DROP PANELS CHILDREN CONCAT TOGETHER
                PUT IN INCORRECT 
                 */
                //send data to server about incorrect try
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Incorrect);
                DraggableGameData.elementArray.Add(d);
                if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
                {
                    FindObjectOfType<QuizCounter>().addToCounter();
                }
                FindObjectOfType<AudioManager>().Play("falsenoise");
                FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);

            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("BeginnerLevel2c"))
        {
            //in this level we should put alkaline metals on top
            if (isChargeCorrect("noble"))
            {
                /*
                send data to server if they got everything correct
                 */
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Correct);
                DraggableGameData.elementArray.Add(d);
                //transition player to next level
                FindObjectOfType<AudioManager>().Play("truenoise");
                FindObjectOfType<SceneFader>().FadeTo("BeginnerLevel2d");
            }
            else
            {
                /*
                BUILD THE WHATS IN THE DROP PANELS CHILDREN CONCAT TOGETHER
                PUT IN INCORRECT 
                 */
                //send data to server about incorrect try
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Incorrect);
                DraggableGameData.elementArray.Add(d);
                if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
                {
                    FindObjectOfType<QuizCounter>().addToCounter();
                }
                FindObjectOfType<AudioManager>().Play("falsenoise");
                FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);

            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("BeginnerLevel2d"))
        {
            //in this level we should put alkali metals only symbols
            if (isChargeCorrect("noble"))
            {
                /*
                BUILD THE WHATS IN THE DROP PANELS CHILDREN CONCAT TOGETHER
                PUT IN CORRECT AND SEND 
                 */

                if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
                {
                    Debug.Log("THE CURRENT SCORE IS --> " + FindObjectOfType<QuizCounter>().getCount());
                    int points = 0;
                    if (FindObjectOfType<QuizCounter>().getCount() == 0)
                    {
                        //3stars
                        points = 500;
                        if (PlayerPrefs.GetInt("Level2_score") < points)
                        {
                            PlayerPrefs.SetInt("Level2_score", points);
                        }
                    }
                    else if (FindObjectOfType<QuizCounter>().getCount() == 1)
                    {
                        //2stars
                        points = 400;
                        if (PlayerPrefs.GetInt("Level2_score") < points)
                        {
                            PlayerPrefs.SetInt("Level2_score", points);
                        }

                    }
                    else
                    {

                        //1star
                        points = 300;
                        if (PlayerPrefs.GetInt("Level2_score") < points)
                        {
                            PlayerPrefs.SetInt("Level2_score", points);
                        }

                    }
                    string elements = "";
                    for (int i = 0; i < dropPanel.childCount; i++)
                    {
                        elements += dropPanel.GetChild(i).GetChild(0).name;
                    }
                    DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Correct);
                    DraggableGameData.elementArray.Add(d);
                    if (GameObject.FindGameObjectWithTag("WebRequestManager") != null)
                    {
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("score", points.ToString());
                        dic.Add("uuid", PlayerPrefs.GetString("ui"));
                        dic.Add("levelid", "B2");
                        FindObjectOfType<WebRequest>().PostData(dic, FindObjectOfType<WebRequest>().buildJSONDraggable());
                    }
                }
                FindObjectOfType<AudioManager>().Play("truenoise");
                endDialog.SetActive(true);
            }
            else
            {
                /*
                BUILD THE WHATS IN THE DROP PANELS CHILDREN CONCAT TOGETHER
                PUT IN INCORRECT 
                 */
                //send data to server about incorrect try
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Incorrect);
                DraggableGameData.elementArray.Add(d);
                if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
                {
                    FindObjectOfType<QuizCounter>().addToCounter();
                }
                FindObjectOfType<AudioManager>().Play("falsenoise");
                FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);

            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("BeginnerLevel3a"))
        {
            //in this level we should put alkali metals on top
            if (isChargeCorrect("transition"))
            {
                /*
                send data to server if they got everything correct
                 */
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Correct);
                DraggableGameData.elementArray.Add(d);
                //transition player to next level
                FindObjectOfType<AudioManager>().Play("truenoise");
                FindObjectOfType<SceneFader>().FadeTo("BeginnerLevel3b");
            }
            else
            {
                /*
                BUILD THE WHATS IN THE DROP PANELS CHILDREN CONCAT TOGETHER
                PUT IN INCORRECT 
                 */
                //send data to server about incorrect try
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Incorrect);
                DraggableGameData.elementArray.Add(d);
                if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
                {
                    FindObjectOfType<QuizCounter>().addToCounter();
                }
                FindObjectOfType<AudioManager>().Play("falsenoise");
                FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);

            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("BeginnerLevel3b"))
        {
            //in this level we should put alkali metals only symbols
            if (isChargeCorrect("transition"))
            {
                /*
                send data to server if they got everything correct
                 */
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Correct);
                DraggableGameData.elementArray.Add(d);
                //if all is correct move to next level
                FindObjectOfType<AudioManager>().Play("truenoise");
                FindObjectOfType<SceneFader>().FadeTo("BeginnerLevel3c");
            }
            else
            {
                /*
                BUILD THE WHATS IN THE DROP PANELS CHILDREN CONCAT TOGETHER
                PUT IN INCORRECT 
                 */
                //send data to server about incorrect try
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Incorrect);
                DraggableGameData.elementArray.Add(d);
                if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
                {
                    FindObjectOfType<QuizCounter>().addToCounter();
                }
                FindObjectOfType<AudioManager>().Play("falsenoise");
                FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);

            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("BeginnerLevel3c"))
        {
            //in this level we should put alkaline metals on top
            if (isChargeCorrect("metalloid"))
            {
                /*
                send data to server if they got everything correct
                 */
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Correct);
                DraggableGameData.elementArray.Add(d);
                //transition player to next level
                FindObjectOfType<AudioManager>().Play("truenoise");
                FindObjectOfType<SceneFader>().FadeTo("BeginnerLevel3d");
            }
            else
            {
                /*
                BUILD THE WHATS IN THE DROP PANELS CHILDREN CONCAT TOGETHER
                PUT IN INCORRECT 
                 */
                //send data to server about incorrect try
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Incorrect);
                DraggableGameData.elementArray.Add(d);
                if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
                {
                    FindObjectOfType<QuizCounter>().addToCounter();
                }
                FindObjectOfType<AudioManager>().Play("falsenoise");
                FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);

            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("BeginnerLevel3d"))
        {
            //in this level we should put alkali metals only symbols
            if (isChargeCorrect("metalloid"))
            {
                /*
                BUILD THE WHATS IN THE DROP PANELS CHILDREN CONCAT TOGETHER
                PUT IN CORRECT AND SEND 
                 */

                if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
                {
                    Debug.Log("THE CURRENT SCORE IS --> " + FindObjectOfType<QuizCounter>().getCount());
                    int points = 0;
                    if (FindObjectOfType<QuizCounter>().getCount() == 0)
                    {
                        //3stars
                        points = 500;
                        if (PlayerPrefs.GetInt("Level3_score") < points)
                        {
                            PlayerPrefs.SetInt("Level3_score", points);
                        }
                    }
                    else if (FindObjectOfType<QuizCounter>().getCount() == 1)
                    {
                        //2stars
                        points = 400;
                        if (PlayerPrefs.GetInt("Level3_score") < points)
                        {
                            PlayerPrefs.SetInt("Level3_score", points);
                        }

                    }
                    else
                    {

                        //1star
                        points = 300;
                        if (PlayerPrefs.GetInt("Level3_score") < points)
                        {
                            PlayerPrefs.SetInt("Level3_score", points);
                        }

                    }
                    string elements = "";
                    for (int i = 0; i < dropPanel.childCount; i++)
                    {
                        elements += dropPanel.GetChild(i).GetChild(0).name;
                    }
                    DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Correct);
                    DraggableGameData.elementArray.Add(d);
                    if (GameObject.FindGameObjectWithTag("WebRequestManager") != null)
                    {
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("score", points.ToString());
                        dic.Add("uuid", PlayerPrefs.GetString("ui"));
                        dic.Add("levelid", "B3");
                        FindObjectOfType<WebRequest>().PostData(dic, FindObjectOfType<WebRequest>().buildJSONDraggable());
                    }
                }
                FindObjectOfType<AudioManager>().Play("truenoise");
                endDialog.SetActive(true);
            }
            else
            {
                /*
                BUILD THE WHATS IN THE DROP PANELS CHILDREN CONCAT TOGETHER
                PUT IN INCORRECT 
                 */
                //send data to server about incorrect try
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Incorrect);
                DraggableGameData.elementArray.Add(d);
                if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
                {
                    FindObjectOfType<QuizCounter>().addToCounter();
                }
                FindObjectOfType<AudioManager>().Play("falsenoise");
                FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);

            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("BeginnerLevel4a"))
        {
            //in this level we should put alkali metals on top
            if (isChargeCorrect("5A"))
            {
                /*
                send data to server if they got everything correct
                 */
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Correct);
                DraggableGameData.elementArray.Add(d);
                //transition player to next level
                FindObjectOfType<AudioManager>().Play("truenoise");
                FindObjectOfType<SceneFader>().FadeTo("BeginnerLevel4b");
            }
            else
            {
                /*
                BUILD THE WHATS IN THE DROP PANELS CHILDREN CONCAT TOGETHER
                PUT IN INCORRECT 
                 */
                //send data to server about incorrect try
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Incorrect);
                DraggableGameData.elementArray.Add(d);
                if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
                {
                    FindObjectOfType<QuizCounter>().addToCounter();
                }
                FindObjectOfType<AudioManager>().Play("falsenoise");
                FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);

            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("BeginnerLevel4b"))
        {
            //in this level we should put alkali metals only symbols
            if (isChargeCorrect("5A"))
            {
                /*
                send data to server if they got everything correct
                 */
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Correct);
                DraggableGameData.elementArray.Add(d);
                //if all is correct move to next level
                FindObjectOfType<AudioManager>().Play("truenoise");
                FindObjectOfType<SceneFader>().FadeTo("BeginnerLevel4c");
            }
            else
            {
                /*
                BUILD THE WHATS IN THE DROP PANELS CHILDREN CONCAT TOGETHER
                PUT IN INCORRECT 
                 */
                //send data to server about incorrect try
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Incorrect);
                DraggableGameData.elementArray.Add(d);
                if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
                {
                    FindObjectOfType<QuizCounter>().addToCounter();
                }
                FindObjectOfType<AudioManager>().Play("falsenoise");
                FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);

            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("BeginnerLevel4c"))
        {
            //in this level we should put alkaline metals on top
            if (isChargeCorrect("6A"))
            {
                /*
                send data to server if they got everything correct
                 */
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Correct);
                DraggableGameData.elementArray.Add(d);
                //transition player to next level
                FindObjectOfType<AudioManager>().Play("truenoise");
                FindObjectOfType<SceneFader>().FadeTo("BeginnerLevel4d");
            }
            else
            {
                /*
                BUILD THE WHATS IN THE DROP PANELS CHILDREN CONCAT TOGETHER
                PUT IN INCORRECT 
                 */
                //send data to server about incorrect try
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Incorrect);
                DraggableGameData.elementArray.Add(d);
                if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
                {
                    FindObjectOfType<QuizCounter>().addToCounter();
                }
                FindObjectOfType<AudioManager>().Play("falsenoise");
                FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);

            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("BeginnerLevel4d"))
        {
            //in this level we should put alkali metals only symbols
            if (isChargeCorrect("6A"))
            {
                /*
                BUILD THE WHATS IN THE DROP PANELS CHILDREN CONCAT TOGETHER
                PUT IN CORRECT AND SEND 
                 */

                if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
                {
                    Debug.Log("THE CURRENT SCORE IS --> " + FindObjectOfType<QuizCounter>().getCount());
                    int points = 0;
                    if (FindObjectOfType<QuizCounter>().getCount() == 0)
                    {
                        //3stars
                        points = 500;
                        if (PlayerPrefs.GetInt("Level4_score") < points)
                        {
                            PlayerPrefs.SetInt("Level4_score", points);
                        }
                    }
                    else if (FindObjectOfType<QuizCounter>().getCount() == 1)
                    {
                        //2stars
                        points = 400;
                        if (PlayerPrefs.GetInt("Level4_score") < points)
                        {
                            PlayerPrefs.SetInt("Level4_score", points);
                        }

                    }
                    else
                    {

                        //1star
                        points = 300;
                        if (PlayerPrefs.GetInt("Level4_score") < points)
                        {
                            PlayerPrefs.SetInt("Level4_score", points);
                        }

                    }
                    string elements = "";
                    for (int i = 0; i < dropPanel.childCount; i++)
                    {
                        elements += dropPanel.GetChild(i).GetChild(0).name;
                    }
                    DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Correct);
                    DraggableGameData.elementArray.Add(d);
                    if (GameObject.FindGameObjectWithTag("WebRequestManager") != null)
                    {
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("score", points.ToString());
                        dic.Add("uuid", PlayerPrefs.GetString("ui"));
                        dic.Add("levelid", "B4");
                        FindObjectOfType<WebRequest>().PostData(dic, FindObjectOfType<WebRequest>().buildJSONDraggable());
                    }
                }
                FindObjectOfType<AudioManager>().Play("truenoise");
                endDialog.SetActive(true);
            }
            else
            {
                /*
                BUILD THE WHATS IN THE DROP PANELS CHILDREN CONCAT TOGETHER
                PUT IN INCORRECT 
                 */
                //send data to server about incorrect try
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Incorrect);
                DraggableGameData.elementArray.Add(d);
                if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
                {
                    FindObjectOfType<QuizCounter>().addToCounter();
                }
                FindObjectOfType<AudioManager>().Play("falsenoise");
                FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);

            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level6a"))
        {
            if (isChargeCorrect("+1"))
            {

                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Correct);
                DraggableGameData.elementArray.Add(d);
                FindObjectOfType<AudioManager>().Play("truenoise");
                FindObjectOfType<SceneFader>().FadeTo("Level6b");
            }
            else
            {
                /*
                BUILD THE WHATS IN THE DROP PANELS CHILDREN CONCAT TOGETHER
                PUT IN INCORRECT 
                 */
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Incorrect);
                DraggableGameData.elementArray.Add(d);
                if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
                {
                    FindObjectOfType<QuizCounter>().addToCounter();
                }
                FindObjectOfType<AudioManager>().Play("falsenoise");
                FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);

            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level6b"))
        {
            if (isChargeCorrect("+2"))
            {
                /*
                BUILD THE WHATS IN THE DROP PANELS CHILDREN CONCAT TOGETHER
                PUT IN CORRECT AND SEND 
                 */

                if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
                {
                    Debug.Log("THE CURRENT SCORE IS --> " + FindObjectOfType<QuizCounter>().getCount());
                    int points = 0;
                    if (FindObjectOfType<QuizCounter>().getCount() == 0)
                    {
                        //3stars
                        points = 500;
                        if (PlayerPrefs.GetInt("Level6_score") < points)
                        {
                            PlayerPrefs.SetInt("Level6_score", points);
                        }
                    }
                    else if (FindObjectOfType<QuizCounter>().getCount() == 1)
                    {
                        //2stars
                        points = 400;
                        if (PlayerPrefs.GetInt("Level6_score") < points)
                        {
                            PlayerPrefs.SetInt("Level6_score", points);
                        }

                    }
                    else
                    {

                        //1star
                        points = 300;
                        if (PlayerPrefs.GetInt("Level6_score") < points)
                        {
                            PlayerPrefs.SetInt("Level6_score", points);
                        }

                    }
                    string elements = "";
                    for (int i = 0; i < dropPanel.childCount; i++)
                    {
                        elements += dropPanel.GetChild(i).GetChild(0).name;
                    }
                    DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Correct);
                    DraggableGameData.elementArray.Add(d);
                    if (GameObject.FindGameObjectWithTag("WebRequestManager") != null)
                    {
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("score", points.ToString());
                        dic.Add("uuid", PlayerPrefs.GetString("ui"));
                        dic.Add("levelid", "6");
                        FindObjectOfType<WebRequest>().PostData(dic, FindObjectOfType<WebRequest>().buildJSONDraggable());
                    }
                }
                FindObjectOfType<AudioManager>().Play("truenoise");
                endDialog.SetActive(true);
            }
            else
            {
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Incorrect);
                DraggableGameData.elementArray.Add(d);
                if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
                {
                    FindObjectOfType<QuizCounter>().addToCounter();
                }
                FindObjectOfType<AudioManager>().Play("falsenoise");
                FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level7a"))
        {
            if (isChargeCorrect("-1"))
            {
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Correct);
                DraggableGameData.elementArray.Add(d);
                FindObjectOfType<AudioManager>().Play("truenoise");
                FindObjectOfType<SceneFader>().FadeTo("Level7b");

            }
            else
            {
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Incorrect);
                DraggableGameData.elementArray.Add(d);
                if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
                {
                    FindObjectOfType<QuizCounter>().addToCounter();
                }
                FindObjectOfType<AudioManager>().Play("falsenoise");
                FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level7b"))
        {
            if (isChargeCorrect("-2"))
            {
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Correct);
                DraggableGameData.elementArray.Add(d);
                FindObjectOfType<AudioManager>().Play("truenoise");
                FindObjectOfType<SceneFader>().FadeTo("Level7c");
            }
            else
            {
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Incorrect);
                DraggableGameData.elementArray.Add(d);
                if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
                {
                    FindObjectOfType<QuizCounter>().addToCounter();
                }
                FindObjectOfType<AudioManager>().Play("falsenoise");
                FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level7c"))
        {
            if (isChargeCorrect("-3"))
            {
                if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
                {
                    Debug.Log("THE CURRENT SCORE IS --> " + FindObjectOfType<QuizCounter>().getCount());
                    int points = 0;
                    if (FindObjectOfType<QuizCounter>().getCount() == 0)
                    {
                        //3stars
                        points = 500;
                        if (PlayerPrefs.GetInt("Level7_score") < points)
                        {
                            PlayerPrefs.SetInt("Level7_score", points);
                        }
                    }
                    else if (FindObjectOfType<QuizCounter>().getCount() == 1)
                    {
                        //2stars
                        points = 400;
                        if (PlayerPrefs.GetInt("Level7_score") < points)
                        {
                            PlayerPrefs.SetInt("Level7_score", points);
                        }

                    }
                    else
                    {
                        //1star
                        points = 300;
                        if (PlayerPrefs.GetInt("Level7_score") < points)
                        {
                            PlayerPrefs.SetInt("Level7_score", points);
                        }
                    }
                    string elements = "";
                    for (int i = 0; i < dropPanel.childCount; i++)
                    {
                        elements += dropPanel.GetChild(i).GetChild(0).name;
                    }
                    DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Correct);
                    DraggableGameData.elementArray.Add(d);
                    if (GameObject.FindGameObjectWithTag("WebRequestManager") != null)
                    {
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("score", points.ToString());
                        dic.Add("uuid", PlayerPrefs.GetString("ui"));
                        dic.Add("levelid", "7");
                        FindObjectOfType<WebRequest>().PostData(dic, FindObjectOfType<WebRequest>().buildJSONDraggable());
                    }
                }

                FindObjectOfType<AudioManager>().Play("truenoise");
                endDialog.SetActive(true);
            }
            else
            {
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Incorrect);
                DraggableGameData.elementArray.Add(d);
                if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
                {
                    FindObjectOfType<QuizCounter>().addToCounter();
                }
                FindObjectOfType<AudioManager>().Play("falsenoise");
                FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level9a"))
        {

            if (doesNameMatch())
            {

                FindObjectOfType<AudioManager>().Play("truenoise");
                FindObjectOfType<SceneFader>().FadeTo("Level9b");

            }
            else
            {

                FindObjectOfType<AudioManager>().Play("falsenoise");
                FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);
            }

        }
        else if (SceneManager.GetActiveScene().name.Equals("Level9b"))
        {
            if (doesNameMatch())
            {
                FindObjectOfType<AudioManager>().Play("truenoise");
                FindObjectOfType<SceneFader>().FadeTo("Level9c");
                //endDialog.SetActive(true);
            }
            else
            {
                FindObjectOfType<AudioManager>().Play("falsenoise");
                FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level9c"))
        {
            if (doesNameMatch())
            {
                if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
                {
                    Debug.Log("THE CURRENT SCORE IS --> " + FindObjectOfType<QuizCounter>().getCount());
                    int points = 0;
                    if (FindObjectOfType<QuizCounter>().getCount() == 0)
                    {
                        //3stars
                        points = 500;
                        if (PlayerPrefs.GetInt("Level9_score") < points)
                        {
                            PlayerPrefs.SetInt("Level9_score", points);
                        }
                    }
                    else if (FindObjectOfType<QuizCounter>().getCount() == 1)
                    {
                        //2stars
                        points = 400;
                        if (PlayerPrefs.GetInt("Level9_score") < points)
                        {
                            PlayerPrefs.SetInt("Level9_score", points);
                        }

                    }
                    else
                    {
                        //1star
                        points = 300;
                        if (PlayerPrefs.GetInt("Level9_score") < points)
                        {
                            PlayerPrefs.SetInt("Level9_score", points);
                        }
                    }

                    string elements = "";
                    for (int i = 0; i < dropPanel.childCount; i++)
                    {
                        elements += dropPanel.GetChild(i).name + dropPanel.GetChild(i).GetChild(0).name;
                    }
                    DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Correct);
                    DraggableGameData.elementArray.Add(d);
                    if (GameObject.FindGameObjectWithTag("WebRequestManager") != null)
                    {
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        dic.Add("score", points.ToString());
                        dic.Add("uuid", PlayerPrefs.GetString("ui"));
                        dic.Add("levelid", "9");
                        FindObjectOfType<WebRequest>().PostData(dic, FindObjectOfType<WebRequest>().buildJSONDraggable());
                    }
                }
                FindObjectOfType<AudioManager>().Play("truenoise");
                endDialog.SetActive(true);
            }
            else
            {
                string elements = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    elements += dropPanel.GetChild(i).name + dropPanel.GetChild(i).GetChild(0).name;
                }
                DraggableGameData d = new DraggableGameData(elements, DraggableGameData.State.Incorrect);
                DraggableGameData.elementArray.Add(d);
                if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
                {
                    FindObjectOfType<QuizCounter>().addToCounter();
                }
                FindObjectOfType<AudioManager>().Play("falsenoise");
                FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);
            }
        }
        else if (SceneManager.GetActiveScene().name.Contains("Level10"))
        {
            if (DropPanelController.currentSpriteName.Equals("sodium bromide"))
            {

                string answer = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    answer += dropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (dropPanel.childCount > 0)
                {
                    if (answer.Equals("nabr"))
                    {
                        right();
                        correctDialogTrigger("NaBr");
                    }
                    else
                    {
                        wrong();
                    }

                }
                else
                {
                    Debug.Log("restart scene");
                }

            }
            else if (DropPanelController.currentSpriteName.Equals("sodium nitrate"))
            {
                string answer = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    answer += dropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (dropPanel.childCount > 0)
                {
                    if (answer.Equals("nano3"))
                    {
                        right();
                        correctDialogTrigger("NaNO\u2083");
                    }
                    else
                    {
                        wrong();
                    }

                }
                else
                {
                    Debug.Log("restart scene");
                }
            }
            else if (DropPanelController.currentSpriteName.Equals("sodium clorate"))
            {
                string answer = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    answer += dropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (dropPanel.childCount > 0)
                {
                    if (answer.Equals("naclo3"))
                    {
                        right();
                        correctDialogTrigger("NaClO\u2083");
                    }
                    else
                    {
                        wrong();
                    }

                }
                else
                {
                    Debug.Log("restart scene");
                }
            }
            else if (DropPanelController.currentSpriteName.Equals("magnesium oxide"))
            {
                string answer = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    answer += dropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (dropPanel.childCount > 0)
                {
                    if (answer.Equals("mg2o2"))
                    {
                        right();
                        correctDialogTrigger("MgO");
                    }
                    else
                    {
                        wrong();
                    }

                }
                else
                {
                    Debug.Log("restart scene");
                }
            }
            else if (DropPanelController.currentSpriteName.Equals("magnesium sulfide"))
            {
                string answer = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    answer += dropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (dropPanel.childCount > 0)
                {
                    if (answer.Equals("mg2s2"))
                    {
                        right();
                        correctDialogTrigger("MgS");
                    }
                    else
                    {
                        wrong();
                    }

                }
                else
                {
                    Debug.Log("restart scene");
                }
            }
            else if (DropPanelController.currentSpriteName.Equals("magnesium carbonate"))
            {
                string answer = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    answer += dropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (dropPanel.childCount > 0)
                {
                    if (answer.Equals("mg2co32"))
                    {
                        right();
                        correctDialogTrigger("MgCO\u2083");
                    }
                    else
                    {
                        wrong();
                    }

                }
                else
                {
                    Debug.Log("restart scene");
                }
            }
            else if (DropPanelController.currentSpriteName.Equals("aluminum nitrate"))
            {
                string answer = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    answer += dropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (dropPanel.childCount > 0)
                {
                    if (answer.Equals("al3no3no3no3"))
                    {
                        right();
                        correctDialogTrigger("Al(NO\u2083)\u2083");
                    }
                    else
                    {
                        wrong();
                    }
                }
                else
                {
                    Debug.Log("restart scene");
                }
            }
            else if (DropPanelController.currentSpriteName.Equals("aluminum phosphide"))
            {
                string answer = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    answer += dropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (dropPanel.childCount > 0)
                {
                    if (answer.Equals("al3p3"))
                    {
                        right();
                        correctDialogTrigger("AlP");
                    }
                    else
                    {
                        wrong();
                    }
                }
                else
                {
                    Debug.Log("restart scene");
                }
            }
            else if (DropPanelController.currentSpriteName.Equals("aluminum phosphate"))
            {
                string answer = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    answer += dropPanel.GetChild(i).name;
                }

                Debug.Log(answer);

                if (dropPanel.childCount > 0)
                {
                    if (answer.Equals("al3po43"))
                    {
                        right();
                        correctDialogTrigger("AlPO\u2084");
                    }
                    else
                    {
                        wrong();
                    }
                }
                else
                {
                    Debug.Log("restart scene");
                }
            }
            else if (DropPanelController.currentSpriteName.Equals("aluminum phosphite"))
            {
                string answer = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    answer += dropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (dropPanel.childCount > 0)
                {
                    if (answer.Equals("al3p033"))
                    {
                        right();
                        correctDialogTrigger("AlPO\u2083");
                    }
                    else
                    {
                        wrong();
                    }
                }
                else
                {
                    Debug.Log("nothing useful");
                }
            }
            else if (DropPanelController.currentSpriteName.Equals("sodium sulfide"))
            {
                string answer = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    answer += dropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (dropPanel.childCount > 0)
                {
                    if (answer.Equals("nanas2"))
                    {
                        right();
                        correctDialogTrigger("Na\u2082S");
                    }
                    else
                    {
                        wrong();
                    }
                }
                else
                {
                    Debug.Log("restart scene");
                }
            }
            else if (DropPanelController.currentSpriteName.Equals("sodium nitride"))
            {
                string answer = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    answer += dropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (dropPanel.childCount > 0)
                {
                    if (answer.Equals("nananan3"))
                    {
                        right();
                        correctDialogTrigger("Na\u2083N");
                    }
                    else
                    {
                        wrong();
                    }
                }
                else
                {
                    Debug.Log("restart scene");
                }
            }
            else if (DropPanelController.currentSpriteName.Equals("sodium phosphide"))
            {
                string answer = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    answer += dropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (dropPanel.childCount > 0)
                {
                    if (answer.Equals("nananap3"))
                    {
                        right();
                        correctDialogTrigger("Na\u2083P");
                    }
                    else
                    {
                        wrong();
                    }
                }
                else
                {
                    Debug.Log("restart scene");
                }
            }
            else if (DropPanelController.currentSpriteName.Equals("magnesium phosphide"))
            {
                string answer = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    answer += dropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (dropPanel.childCount > 0)
                {
                    if (answer.Equals("mg2mg2mg2p3p3"))
                    {
                        right();
                        correctDialogTrigger("Mg\u2083P\u2082");
                    }
                    else
                    {
                        wrong();
                    }
                }
                else
                {
                    Debug.Log("restart scene");
                }
            }
            else if (DropPanelController.currentSpriteName.Equals("aluminum oxide"))
            {
                string answer = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    answer += dropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (dropPanel.childCount > 0)
                {
                    if (answer.Equals("al3al3o2o2o2"))
                    {
                        right();
                        correctDialogTrigger("Al\u2082O\u2083");
                    }
                    else
                    {
                        wrong();
                    }
                }
                else
                {
                    Debug.Log("restart scene");
                }
            }
            else if (DropPanelController.currentSpriteName.Equals("aluminum sulfide"))
            {
                string answer = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    answer += dropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (dropPanel.childCount > 0)
                {
                    if (answer.Equals("al3al3s2s2s2"))
                    {
                        right();
                        correctDialogTrigger("Al\u2082S\u2083");
                    }
                    else
                    {
                        wrong();
                    }
                }
                else
                {
                    Debug.Log("restart scene");
                }
            }
            else if (DropPanelController.currentSpriteName.Equals("sodium sulfate"))
            {
                string answer = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    answer += dropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (dropPanel.childCount > 0)
                {
                    if (answer.Equals("nanaso42"))
                    {
                        right();
                        correctDialogTrigger("Na\u2082SO\u2084");
                    }
                    else
                    {
                        wrong();
                    }
                }
                else
                {
                    Debug.Log("restart scene");
                }
            }
            else if (DropPanelController.currentSpriteName.Equals("sodium carbonate"))
            {
                string answer = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    answer += dropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (dropPanel.childCount > 0)
                {
                    if (answer.Equals("nanaco32"))
                    {
                        right();
                        correctDialogTrigger("Na\u2082CO\u2083");
                    }
                    else
                    {
                        wrong();
                    }
                }
                else
                {
                    Debug.Log("restart scene");
                }
            }
            else if (DropPanelController.currentSpriteName.Equals("magnesium nitrate"))
            {
                string answer = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    answer += dropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (dropPanel.childCount > 0)
                {
                    if (answer.Equals("mg2no3no3"))
                    {
                        right();
                        correctDialogTrigger("Mg(NO\u2083)\u2082");
                    }
                    else
                    {
                        wrong();
                    }
                }
                else
                {
                    Debug.Log("restart scene");
                }
            }
            else if (DropPanelController.currentSpriteName.Equals("aluminum nitrate"))
            {
                string answer = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    answer += dropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (dropPanel.childCount > 0)
                {
                    if (answer.Equals("al3no3no3no3"))
                    {
                        right();
                        correctDialogTrigger("Al(NO\u2083)\u2083");
                    }
                    else
                    {
                        wrong();
                    }
                }
                else
                {
                    Debug.Log("restart scene");
                }
            }
            else if (DropPanelController.currentSpriteName.Equals("aluminum sulfate"))
            {
                string answer = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    answer += dropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (dropPanel.childCount > 0)
                {
                    if (answer.Equals("al3al3so42so42so42"))
                    {
                        right();
                        correctDialogTrigger("Al\u2082(SO\u2084)\u2083");
                    }
                    else
                    {
                        wrong();
                    }
                }
                else
                {
                    Debug.Log("restart scene");
                }
            }
            else if (DropPanelController.currentSpriteName.Equals("magnesium nitride"))
            {

                string answer = "";
                for (int i = 0; i < dropPanel.childCount; i++)
                {
                    answer += dropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (dropPanel.childCount > 0)
                {
                    if (answer.Equals("mg2mg2mg2n3n3"))
                    {
                        right();
                        correctDialogTrigger("Mg\u2083N\u2082");
                    }
                    else
                    {
                        wrong();
                    }
                }
                else
                {
                    Debug.Log("restart scene");
                }
            }
        }


    }

    private void correctDialogTrigger(string formula)
    {
        correctDialog.SetActive(true);
        correctDialog.GetComponentInChildren<Text>().fontStyle = FontStyle.Bold;
        correctDialog.GetComponentInChildren<Text>().text = "Correct!\n" + formula;
    }

    /*
        TODO: SEND DATA TO SERVER 
        AT THE END OF RIGHT SEND ALL THE DATA ANYDATA PRIOR NEEDS TO BE BUILT IN SOME OBJECT MAKE A CLASS TO
     */

    /*
    data to send : compound they got right, score,  
     */
    private void right()
    {
        string formula = DropPanelController.currentSpriteName;
        string attempt = "";

        for (int i = 0; i < dropPanel.childCount; i++)
        {
            attempt += dropPanel.GetChild(i).name;

        }

        LastLevelGameData.attemptArray.Add(formula + "," + attempt);

        foreach (string item in LastLevelGameData.attemptArray)
        {
            Debug.Log("in the array " + item);
        }


        rightCounter++;
        FindObjectOfType<AudioManager>().Play("correctnoise");
        //score 15 to win
        if (rightCounter >= 1)
        {

            //beat the game
            //FindObjectOfType<AudioManager>().Pause("quizgamenoise");
            rightCounter = 0;
            wrongCounter = 0;
            if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
            {
                int points = 0;
                if (FindObjectOfType<QuizCounter>().getCount() == 0)
                {
                    //3stars
                    if (AchievementManager.THIS)
                    {
                        AchievementManager.THIS.UnlockAchievement(3);
                    }
                    points = 500;

                    if (PlayerPrefs.GetInt("Level10_score") < points)
                    {
                        PlayerPrefs.SetInt("Level10_score", points);
                    }
                }
                else if (FindObjectOfType<QuizCounter>().getCount() == 1)
                {
                    //2stars
                    points = 400;
                    if (PlayerPrefs.GetInt("Level10_score") < points)
                    {
                        PlayerPrefs.SetInt("Level10_score", points);
                    }
                }
                else if (FindObjectOfType<QuizCounter>().getCount() == 2)
                {
                    //1star
                    points = 300;
                    if (PlayerPrefs.GetInt("Level10_score") < points)
                    {
                        PlayerPrefs.SetInt("Level10_score", points);
                    }
                }
                else
                {
                    points = 300;
                    if (PlayerPrefs.GetInt("Level10_score") < points)
                    {
                        PlayerPrefs.SetInt("Level10_score", points);
                    }
                    endDialog.GetComponent<Text>().text = "CONGRATULATIONS, YOU HAVE MASTERED THE GAME! BUT YOU MAY WANT TO CONTACT A TUTOR.";
                    //see a tutor dialog and 1 star for completion
                }

                if (GameObject.FindGameObjectWithTag("WebRequestManager") != null)
                {
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic.Add("score", points.ToString());
                    dic.Add("uuid", PlayerPrefs.GetString("ui"));
                    dic.Add("levelid", "10");
                    FindObjectOfType<WebRequest>().PostData(dic, FindObjectOfType<WebRequest>().buildJSONLastLevel());
                }
            }





            if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
            {
                FindObjectOfType<QuizCounter>().Destroy();
            }
            if (GameObject.FindGameObjectWithTag("RandomQuizNumbers") != null)
            {
                FindObjectOfType<RandomQuizNumbers>().Destroy();
            }

            endDialog.SetActive(true);


        }
        else
        {
            //correctDialog.SetActive(true);
            //FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);
            //rulesDialog.SetActive(false);
        }

    }

    /*
    data to send : compound they got wrong, the incorrect answer, 
     */
    private void wrong()
    {
        string formula = DropPanelController.currentSpriteName;
        string attempt = "";

        for (int i = 0; i < dropPanel.childCount; i++)
        {
            attempt += dropPanel.GetChild(i).name;

        }

        LastLevelGameData.attemptArray.Add(formula + "," + attempt);
        foreach (string item in LastLevelGameData.attemptArray)
        {
            Debug.Log("in the array " + item);
        }
        wrongCounter++;
        FindObjectOfType<AudioManager>().Play("falsenoise");
        feedBackDialog.SetActive(true);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("wrong answer");
        /*
        if they get 15 right first try 3 stars 
        if they get 7 wrong take them back to the video which will make them have 2 stars
        if they make 3 attempts prompt to see a tutor
         */
        //7
        if (wrongCounter == 7)
        {
            if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
            {
                FindObjectOfType<QuizCounter>().addToCounter();
            }
            rightCounter = 0;
            wrongCounter = 0;
            FindObjectOfType<SceneFader>().FadeTo("LevelTenVideoAnimation");

        }

    }

}



