using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DragManager : MonoBehaviour
{
    public Transform dropPanel, dragPanel;
    public GameObject submitButton, endDialog, feedBackDialog, rulesDialog;
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
            if (isCorrectAmount())
            {
                //build list of what was created in the 
                submitButton.SetActive(true);
            }
        }
        else
        {
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

    public void onSubmitButtonPress()
    {
        if (SceneManager.GetActiveScene().name.Equals("Level6a"))
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
                    if (answer.Equals("al3po33"))
                    {
                        right();
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

    private void right()
    {

        rightCounter++;
        FindObjectOfType<AudioManager>().Play("correctnoise");
        //score 15 to win
        if (rightCounter >= 15)
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
            FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);
            //rulesDialog.SetActive(false);
        }

    }
    private void wrong()
    {
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



