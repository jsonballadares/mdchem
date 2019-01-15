using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DropPanelController : MonoBehaviour
{
    [SerializeField]
    private Sprite[] formulaSpriteArray;

    [SerializeField]
    private Sprite[] ionSpriteArray;

    [SerializeField]
    private GameObject[] slotSprites;

    [SerializeField]
    private Transform DragPanel;

    public static string currentSpriteName;

    public static int arraySize;

    public static bool willRestart = false;

    public static int currentIndex;


    void Start()
    {
        if (!willRestart)
        {
            arraySize = formulaSpriteArray.Length;
            int randomIndex = 0;
            if (GameObject.FindGameObjectWithTag("RandomQuizNumbers") != null)
            {
                randomIndex = FindObjectOfType<RandomQuizNumbers>().serveNumber();
            }
            gameObject.GetComponent<Image>().sprite = formulaSpriteArray[randomIndex];
            currentIndex = randomIndex;
            Debug.Log("INDEX WE ARE ON ---> " + randomIndex);
            currentSpriteName = formulaSpriteArray[randomIndex].name;
            Debug.Log(currentSpriteName);

            if (SceneManager.GetActiveScene().name.Equals("Level10"))
            {
                LevelTenSetup();
                randomDragPanelOrder();
            }
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = formulaSpriteArray[currentIndex];
            currentSpriteName = formulaSpriteArray[currentIndex].name;
            Debug.Log("CURRENT SPRITE NAME FOR RESTART --> " + currentSpriteName);

            if (SceneManager.GetActiveScene().name.Equals("Level10"))
            {
                LevelTenSetup();
                randomDragPanelOrder();
            }
            willRestart = false;
        }



    }

    /*
                QuizData q = new QuizData(currentQuestion.fact, QuizData.State.Correct,(int)TimeManager.timeThreshhold);
            Debug.Log("THE QURRENT QUESITON IS ---> " + q.ToString());
            QuizData.elementArray.Add(q);
     */

    public void restartScene()
    {
        FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);
        willRestart = true;
    }


    private void LevelTenSetup()
    {
        if (currentSpriteName.Equals("sodium bromide"))
        {
            Debug.Log("in sodium bormide");
            slotSprites[0].GetComponent<Image>().sprite = ionSpriteArray[0];
            slotSprites[1].GetComponent<Image>().sprite = ionSpriteArray[0];
            slotSprites[2].GetComponent<Image>().sprite = ionSpriteArray[0];
            slotSprites[3].GetComponent<Image>().sprite = ionSpriteArray[3];
            slotSprites[4].GetComponent<Image>().sprite = ionSpriteArray[3];
            slotSprites[5].GetComponent<Image>().sprite = ionSpriteArray[3];

            slotSprites[0].GetComponent<Image>().name = ionSpriteArray[0].name;
            slotSprites[1].GetComponent<Image>().name = ionSpriteArray[0].name;
            slotSprites[2].GetComponent<Image>().name = ionSpriteArray[0].name;
            slotSprites[3].GetComponent<Image>().name = ionSpriteArray[3].name;
            slotSprites[4].GetComponent<Image>().name = ionSpriteArray[3].name;
            slotSprites[5].GetComponent<Image>().name = ionSpriteArray[3].name;
        }
        else if (currentSpriteName.Equals("sodium nitrate"))
        {
            slotSprites[0].GetComponent<Image>().sprite = ionSpriteArray[0];
            slotSprites[1].GetComponent<Image>().sprite = ionSpriteArray[0];
            slotSprites[2].GetComponent<Image>().sprite = ionSpriteArray[0];
            slotSprites[3].GetComponent<Image>().sprite = ionSpriteArray[4];
            slotSprites[4].GetComponent<Image>().sprite = ionSpriteArray[4];
            slotSprites[5].GetComponent<Image>().sprite = ionSpriteArray[4];

            slotSprites[0].GetComponent<Image>().name = ionSpriteArray[0].name;
            slotSprites[1].GetComponent<Image>().name = ionSpriteArray[0].name;
            slotSprites[2].GetComponent<Image>().name = ionSpriteArray[0].name;
            slotSprites[3].GetComponent<Image>().name = ionSpriteArray[4].name;
            slotSprites[4].GetComponent<Image>().name = ionSpriteArray[4].name;
            slotSprites[5].GetComponent<Image>().name = ionSpriteArray[4].name;
        }
        else if (currentSpriteName.Equals("sodium clorate"))
        {
            slotSprites[0].GetComponent<Image>().sprite = ionSpriteArray[0];
            slotSprites[1].GetComponent<Image>().sprite = ionSpriteArray[0];
            slotSprites[2].GetComponent<Image>().sprite = ionSpriteArray[0];
            slotSprites[3].GetComponent<Image>().sprite = ionSpriteArray[5];
            slotSprites[4].GetComponent<Image>().sprite = ionSpriteArray[5];
            slotSprites[5].GetComponent<Image>().sprite = ionSpriteArray[5];

            slotSprites[0].GetComponent<Image>().name = ionSpriteArray[0].name;
            slotSprites[1].GetComponent<Image>().name = ionSpriteArray[0].name;
            slotSprites[2].GetComponent<Image>().name = ionSpriteArray[0].name;
            slotSprites[3].GetComponent<Image>().name = ionSpriteArray[5].name;
            slotSprites[4].GetComponent<Image>().name = ionSpriteArray[5].name;
            slotSprites[5].GetComponent<Image>().name = ionSpriteArray[5].name;
        }
        else if (currentSpriteName.Equals("magnesium oxide"))
        {
            slotSprites[0].GetComponent<Image>().sprite = ionSpriteArray[1];
            slotSprites[1].GetComponent<Image>().sprite = ionSpriteArray[1];
            slotSprites[2].GetComponent<Image>().sprite = ionSpriteArray[1];
            slotSprites[3].GetComponent<Image>().sprite = ionSpriteArray[6];
            slotSprites[4].GetComponent<Image>().sprite = ionSpriteArray[6];
            slotSprites[5].GetComponent<Image>().sprite = ionSpriteArray[6];

            slotSprites[0].GetComponent<Image>().name = ionSpriteArray[1].name;
            slotSprites[1].GetComponent<Image>().name = ionSpriteArray[1].name;
            slotSprites[2].GetComponent<Image>().name = ionSpriteArray[1].name;
            slotSprites[3].GetComponent<Image>().name = ionSpriteArray[6].name;
            slotSprites[4].GetComponent<Image>().name = ionSpriteArray[6].name;
            slotSprites[5].GetComponent<Image>().name = ionSpriteArray[6].name;
        }
        else if (currentSpriteName.Equals("magnesium sulfide"))
        {
            slotSprites[0].GetComponent<Image>().sprite = ionSpriteArray[1];
            slotSprites[1].GetComponent<Image>().sprite = ionSpriteArray[1];
            slotSprites[2].GetComponent<Image>().sprite = ionSpriteArray[1];
            slotSprites[3].GetComponent<Image>().sprite = ionSpriteArray[7];
            slotSprites[4].GetComponent<Image>().sprite = ionSpriteArray[7];
            slotSprites[5].GetComponent<Image>().sprite = ionSpriteArray[7];

            slotSprites[0].GetComponent<Image>().name = ionSpriteArray[1].name;
            slotSprites[1].GetComponent<Image>().name = ionSpriteArray[1].name;
            slotSprites[2].GetComponent<Image>().name = ionSpriteArray[1].name;
            slotSprites[3].GetComponent<Image>().name = ionSpriteArray[7].name;
            slotSprites[4].GetComponent<Image>().name = ionSpriteArray[7].name;
            slotSprites[5].GetComponent<Image>().name = ionSpriteArray[7].name;
        }
        else if (currentSpriteName.Equals("magnesium carbonate"))
        {
            slotSprites[0].GetComponent<Image>().sprite = ionSpriteArray[1];
            slotSprites[1].GetComponent<Image>().sprite = ionSpriteArray[1];
            slotSprites[2].GetComponent<Image>().sprite = ionSpriteArray[1];
            slotSprites[3].GetComponent<Image>().sprite = ionSpriteArray[9];
            slotSprites[4].GetComponent<Image>().sprite = ionSpriteArray[9];
            slotSprites[5].GetComponent<Image>().sprite = ionSpriteArray[9];

            slotSprites[0].GetComponent<Image>().name = ionSpriteArray[1].name;
            slotSprites[1].GetComponent<Image>().name = ionSpriteArray[1].name;
            slotSprites[2].GetComponent<Image>().name = ionSpriteArray[1].name;
            slotSprites[3].GetComponent<Image>().name = ionSpriteArray[9].name;
            slotSprites[4].GetComponent<Image>().name = ionSpriteArray[9].name;
            slotSprites[5].GetComponent<Image>().name = ionSpriteArray[9].name;
        }
        else if (currentSpriteName.Equals("aluminum nitrate"))
        {
            slotSprites[0].GetComponent<Image>().sprite = ionSpriteArray[2];
            slotSprites[1].GetComponent<Image>().sprite = ionSpriteArray[2];
            slotSprites[2].GetComponent<Image>().sprite = ionSpriteArray[2];
            slotSprites[3].GetComponent<Image>().sprite = ionSpriteArray[4];
            slotSprites[4].GetComponent<Image>().sprite = ionSpriteArray[4];
            slotSprites[5].GetComponent<Image>().sprite = ionSpriteArray[4];

            slotSprites[0].GetComponent<Image>().name = ionSpriteArray[2].name;
            slotSprites[1].GetComponent<Image>().name = ionSpriteArray[2].name;
            slotSprites[2].GetComponent<Image>().name = ionSpriteArray[2].name;
            slotSprites[3].GetComponent<Image>().name = ionSpriteArray[4].name;
            slotSprites[4].GetComponent<Image>().name = ionSpriteArray[4].name;
            slotSprites[5].GetComponent<Image>().name = ionSpriteArray[4].name;
        }
        else if (currentSpriteName.Equals("aluminum phosphide"))
        {
            slotSprites[0].GetComponent<Image>().sprite = ionSpriteArray[2];
            slotSprites[1].GetComponent<Image>().sprite = ionSpriteArray[2];
            slotSprites[2].GetComponent<Image>().sprite = ionSpriteArray[2];
            slotSprites[3].GetComponent<Image>().sprite = ionSpriteArray[11];
            slotSprites[4].GetComponent<Image>().sprite = ionSpriteArray[11];
            slotSprites[5].GetComponent<Image>().sprite = ionSpriteArray[11];

            slotSprites[0].GetComponent<Image>().name = ionSpriteArray[2].name;
            slotSprites[1].GetComponent<Image>().name = ionSpriteArray[2].name;
            slotSprites[2].GetComponent<Image>().name = ionSpriteArray[2].name;
            slotSprites[3].GetComponent<Image>().name = ionSpriteArray[11].name;
            slotSprites[4].GetComponent<Image>().name = ionSpriteArray[11].name;
            slotSprites[5].GetComponent<Image>().name = ionSpriteArray[11].name;
        }
        else if (currentSpriteName.Equals("aluminum phosphate"))
        {
            slotSprites[0].GetComponent<Image>().sprite = ionSpriteArray[2];
            slotSprites[1].GetComponent<Image>().sprite = ionSpriteArray[2];
            slotSprites[2].GetComponent<Image>().sprite = ionSpriteArray[2];
            slotSprites[3].GetComponent<Image>().sprite = ionSpriteArray[12];
            slotSprites[4].GetComponent<Image>().sprite = ionSpriteArray[12];
            slotSprites[5].GetComponent<Image>().sprite = ionSpriteArray[12];

            slotSprites[0].GetComponent<Image>().name = ionSpriteArray[2].name;
            slotSprites[1].GetComponent<Image>().name = ionSpriteArray[2].name;
            slotSprites[2].GetComponent<Image>().name = ionSpriteArray[2].name;
            slotSprites[3].GetComponent<Image>().name = ionSpriteArray[12].name;
            slotSprites[4].GetComponent<Image>().name = ionSpriteArray[12].name;
            slotSprites[5].GetComponent<Image>().name = ionSpriteArray[12].name;
        }
        else if (currentSpriteName.Equals("aluminum phosphite"))
        {
            slotSprites[0].GetComponent<Image>().sprite = ionSpriteArray[2];
            slotSprites[1].GetComponent<Image>().sprite = ionSpriteArray[2];
            slotSprites[2].GetComponent<Image>().sprite = ionSpriteArray[2];
            slotSprites[3].GetComponent<Image>().sprite = ionSpriteArray[13];
            slotSprites[4].GetComponent<Image>().sprite = ionSpriteArray[13];
            slotSprites[5].GetComponent<Image>().sprite = ionSpriteArray[13];

            slotSprites[0].GetComponent<Image>().name = ionSpriteArray[2].name;
            slotSprites[1].GetComponent<Image>().name = ionSpriteArray[2].name;
            slotSprites[2].GetComponent<Image>().name = ionSpriteArray[2].name;
            slotSprites[3].GetComponent<Image>().name = ionSpriteArray[13].name;
            slotSprites[4].GetComponent<Image>().name = ionSpriteArray[13].name;
            slotSprites[5].GetComponent<Image>().name = ionSpriteArray[13].name;
        }
        //basically level 11 now combined with 10
        else if (currentSpriteName.Equals("sodium sulfide"))
        {
            Debug.Log("in level 11 question");
            slotSprites[0].GetComponent<Image>().sprite = ionSpriteArray[0];
            slotSprites[1].GetComponent<Image>().sprite = ionSpriteArray[0];
            slotSprites[2].GetComponent<Image>().sprite = ionSpriteArray[0];
            slotSprites[3].GetComponent<Image>().sprite = ionSpriteArray[7];
            slotSprites[4].GetComponent<Image>().sprite = ionSpriteArray[7];
            slotSprites[5].GetComponent<Image>().sprite = ionSpriteArray[7];

            slotSprites[0].GetComponent<Image>().name = ionSpriteArray[0].name;
            slotSprites[1].GetComponent<Image>().name = ionSpriteArray[0].name;
            slotSprites[2].GetComponent<Image>().name = ionSpriteArray[0].name;
            slotSprites[3].GetComponent<Image>().name = ionSpriteArray[7].name;
            slotSprites[4].GetComponent<Image>().name = ionSpriteArray[7].name;
            slotSprites[5].GetComponent<Image>().name = ionSpriteArray[7].name;
        }
        else if (currentSpriteName.Equals("sodium nitride"))
        {
            Debug.Log("in level 11 question");
            slotSprites[0].GetComponent<Image>().sprite = ionSpriteArray[0];
            slotSprites[1].GetComponent<Image>().sprite = ionSpriteArray[0];
            slotSprites[2].GetComponent<Image>().sprite = ionSpriteArray[0];
            slotSprites[3].GetComponent<Image>().sprite = ionSpriteArray[10];
            slotSprites[4].GetComponent<Image>().sprite = ionSpriteArray[10];
            slotSprites[5].GetComponent<Image>().sprite = ionSpriteArray[10];

            slotSprites[0].GetComponent<Image>().name = ionSpriteArray[0].name;
            slotSprites[1].GetComponent<Image>().name = ionSpriteArray[0].name;
            slotSprites[2].GetComponent<Image>().name = ionSpriteArray[0].name;
            slotSprites[3].GetComponent<Image>().name = ionSpriteArray[10].name;
            slotSprites[4].GetComponent<Image>().name = ionSpriteArray[10].name;
            slotSprites[5].GetComponent<Image>().name = ionSpriteArray[10].name;
        }
        else if (currentSpriteName.Equals("sodium phosphide"))
        {
            Debug.Log("in level 11 question");
            slotSprites[0].GetComponent<Image>().sprite = ionSpriteArray[0];
            slotSprites[1].GetComponent<Image>().sprite = ionSpriteArray[0];
            slotSprites[2].GetComponent<Image>().sprite = ionSpriteArray[0];
            slotSprites[3].GetComponent<Image>().sprite = ionSpriteArray[11];
            slotSprites[4].GetComponent<Image>().sprite = ionSpriteArray[11];
            slotSprites[5].GetComponent<Image>().sprite = ionSpriteArray[11];

            slotSprites[0].GetComponent<Image>().name = ionSpriteArray[0].name;
            slotSprites[1].GetComponent<Image>().name = ionSpriteArray[0].name;
            slotSprites[2].GetComponent<Image>().name = ionSpriteArray[0].name;
            slotSprites[3].GetComponent<Image>().name = ionSpriteArray[11].name;
            slotSprites[4].GetComponent<Image>().name = ionSpriteArray[11].name;
            slotSprites[5].GetComponent<Image>().name = ionSpriteArray[11].name;
        }
        else if (currentSpriteName.Equals("magnesium nitride"))
        {
            Debug.Log("in level 11 question");
            slotSprites[0].GetComponent<Image>().sprite = ionSpriteArray[1];
            slotSprites[1].GetComponent<Image>().sprite = ionSpriteArray[1];
            slotSprites[2].GetComponent<Image>().sprite = ionSpriteArray[1];
            slotSprites[3].GetComponent<Image>().sprite = ionSpriteArray[10];
            slotSprites[4].GetComponent<Image>().sprite = ionSpriteArray[10];
            slotSprites[5].GetComponent<Image>().sprite = ionSpriteArray[10];

            slotSprites[0].GetComponent<Image>().name = ionSpriteArray[1].name;
            slotSprites[1].GetComponent<Image>().name = ionSpriteArray[1].name;
            slotSprites[2].GetComponent<Image>().name = ionSpriteArray[1].name;
            slotSprites[3].GetComponent<Image>().name = ionSpriteArray[10].name;
            slotSprites[4].GetComponent<Image>().name = ionSpriteArray[10].name;
            slotSprites[5].GetComponent<Image>().name = ionSpriteArray[10].name;
        }
        else if (currentSpriteName.Equals("magnesium phosphide"))
        {

            Debug.Log("in level 11 question");
            slotSprites[0].GetComponent<Image>().sprite = ionSpriteArray[1];
            slotSprites[1].GetComponent<Image>().sprite = ionSpriteArray[1];
            slotSprites[2].GetComponent<Image>().sprite = ionSpriteArray[1];
            slotSprites[3].GetComponent<Image>().sprite = ionSpriteArray[11];
            slotSprites[4].GetComponent<Image>().sprite = ionSpriteArray[11];
            slotSprites[5].GetComponent<Image>().sprite = ionSpriteArray[11];

            slotSprites[0].GetComponent<Image>().name = ionSpriteArray[1].name;
            slotSprites[1].GetComponent<Image>().name = ionSpriteArray[1].name;
            slotSprites[2].GetComponent<Image>().name = ionSpriteArray[1].name;
            slotSprites[3].GetComponent<Image>().name = ionSpriteArray[11].name;
            slotSprites[4].GetComponent<Image>().name = ionSpriteArray[11].name;
            slotSprites[5].GetComponent<Image>().name = ionSpriteArray[11].name;
        }
        else if (currentSpriteName.Equals("aluminum oxide"))
        {
            Debug.Log("in level 11 question");
            slotSprites[0].GetComponent<Image>().sprite = ionSpriteArray[2];
            slotSprites[1].GetComponent<Image>().sprite = ionSpriteArray[2];
            slotSprites[2].GetComponent<Image>().sprite = ionSpriteArray[2];
            slotSprites[3].GetComponent<Image>().sprite = ionSpriteArray[6];
            slotSprites[4].GetComponent<Image>().sprite = ionSpriteArray[6];
            slotSprites[5].GetComponent<Image>().sprite = ionSpriteArray[6];

            slotSprites[0].GetComponent<Image>().name = ionSpriteArray[2].name;
            slotSprites[1].GetComponent<Image>().name = ionSpriteArray[2].name;
            slotSprites[2].GetComponent<Image>().name = ionSpriteArray[2].name;
            slotSprites[3].GetComponent<Image>().name = ionSpriteArray[6].name;
            slotSprites[4].GetComponent<Image>().name = ionSpriteArray[6].name;
            slotSprites[5].GetComponent<Image>().name = ionSpriteArray[6].name;
        }
        else if (currentSpriteName.Equals("aluminum sulfide"))
        {
            Debug.Log("in level 11 question");
            slotSprites[0].GetComponent<Image>().sprite = ionSpriteArray[2];
            slotSprites[1].GetComponent<Image>().sprite = ionSpriteArray[2];
            slotSprites[2].GetComponent<Image>().sprite = ionSpriteArray[2];
            slotSprites[3].GetComponent<Image>().sprite = ionSpriteArray[7];
            slotSprites[4].GetComponent<Image>().sprite = ionSpriteArray[7];
            slotSprites[5].GetComponent<Image>().sprite = ionSpriteArray[7];

            slotSprites[0].GetComponent<Image>().name = ionSpriteArray[2].name;
            slotSprites[1].GetComponent<Image>().name = ionSpriteArray[2].name;
            slotSprites[2].GetComponent<Image>().name = ionSpriteArray[2].name;
            slotSprites[3].GetComponent<Image>().name = ionSpriteArray[7].name;
            slotSprites[4].GetComponent<Image>().name = ionSpriteArray[7].name;
            slotSprites[5].GetComponent<Image>().name = ionSpriteArray[7].name;
        }
        else if (currentSpriteName.Equals("sodium sulfate"))
        {
            Debug.Log("in level 11 question");
            slotSprites[0].GetComponent<Image>().sprite = ionSpriteArray[0];
            slotSprites[1].GetComponent<Image>().sprite = ionSpriteArray[0];
            slotSprites[2].GetComponent<Image>().sprite = ionSpriteArray[0];
            slotSprites[3].GetComponent<Image>().sprite = ionSpriteArray[8];
            slotSprites[4].GetComponent<Image>().sprite = ionSpriteArray[8];
            slotSprites[5].GetComponent<Image>().sprite = ionSpriteArray[8];

            slotSprites[0].GetComponent<Image>().name = ionSpriteArray[0].name;
            slotSprites[1].GetComponent<Image>().name = ionSpriteArray[0].name;
            slotSprites[2].GetComponent<Image>().name = ionSpriteArray[0].name;
            slotSprites[3].GetComponent<Image>().name = ionSpriteArray[8].name;
            slotSprites[4].GetComponent<Image>().name = ionSpriteArray[8].name;
            slotSprites[5].GetComponent<Image>().name = ionSpriteArray[8].name;
        }
        else if (currentSpriteName.Equals("sodium carbonate"))
        {
            Debug.Log("in level 11 question");
            slotSprites[0].GetComponent<Image>().sprite = ionSpriteArray[0];
            slotSprites[1].GetComponent<Image>().sprite = ionSpriteArray[0];
            slotSprites[2].GetComponent<Image>().sprite = ionSpriteArray[0];
            slotSprites[3].GetComponent<Image>().sprite = ionSpriteArray[9];
            slotSprites[4].GetComponent<Image>().sprite = ionSpriteArray[9];
            slotSprites[5].GetComponent<Image>().sprite = ionSpriteArray[9];

            slotSprites[0].GetComponent<Image>().name = ionSpriteArray[0].name;
            slotSprites[1].GetComponent<Image>().name = ionSpriteArray[0].name;
            slotSprites[2].GetComponent<Image>().name = ionSpriteArray[0].name;
            slotSprites[3].GetComponent<Image>().name = ionSpriteArray[9].name;
            slotSprites[4].GetComponent<Image>().name = ionSpriteArray[9].name;
            slotSprites[5].GetComponent<Image>().name = ionSpriteArray[9].name;
        }
        else if (currentSpriteName.Equals("magnesium nitrate"))
        {
            Debug.Log("in level 11 question");
            slotSprites[0].GetComponent<Image>().sprite = ionSpriteArray[1];
            slotSprites[1].GetComponent<Image>().sprite = ionSpriteArray[1];
            slotSprites[2].GetComponent<Image>().sprite = ionSpriteArray[1];
            slotSprites[3].GetComponent<Image>().sprite = ionSpriteArray[4];
            slotSprites[4].GetComponent<Image>().sprite = ionSpriteArray[4];
            slotSprites[5].GetComponent<Image>().sprite = ionSpriteArray[4];

            slotSprites[0].GetComponent<Image>().name = ionSpriteArray[1].name;
            slotSprites[1].GetComponent<Image>().name = ionSpriteArray[1].name;
            slotSprites[2].GetComponent<Image>().name = ionSpriteArray[1].name;
            slotSprites[3].GetComponent<Image>().name = ionSpriteArray[4].name;
            slotSprites[4].GetComponent<Image>().name = ionSpriteArray[4].name;
            slotSprites[5].GetComponent<Image>().name = ionSpriteArray[4].name;
        }
        else if (currentSpriteName.Equals("aluminum nitrate"))
        {
            Debug.Log("in level 11 question");
            slotSprites[0].GetComponent<Image>().sprite = ionSpriteArray[2];
            slotSprites[1].GetComponent<Image>().sprite = ionSpriteArray[2];
            slotSprites[2].GetComponent<Image>().sprite = ionSpriteArray[2];
            slotSprites[3].GetComponent<Image>().sprite = ionSpriteArray[4];
            slotSprites[4].GetComponent<Image>().sprite = ionSpriteArray[4];
            slotSprites[5].GetComponent<Image>().sprite = ionSpriteArray[4];

            slotSprites[0].GetComponent<Image>().name = ionSpriteArray[2].name;
            slotSprites[1].GetComponent<Image>().name = ionSpriteArray[2].name;
            slotSprites[2].GetComponent<Image>().name = ionSpriteArray[2].name;
            slotSprites[3].GetComponent<Image>().name = ionSpriteArray[4].name;
            slotSprites[4].GetComponent<Image>().name = ionSpriteArray[4].name;
            slotSprites[5].GetComponent<Image>().name = ionSpriteArray[4].name;
        }
        else if (currentSpriteName.Equals("aluminum sulfate"))
        {
            Debug.Log("in level 11 question");
            slotSprites[0].GetComponent<Image>().sprite = ionSpriteArray[2];
            slotSprites[1].GetComponent<Image>().sprite = ionSpriteArray[2];
            slotSprites[2].GetComponent<Image>().sprite = ionSpriteArray[2];
            slotSprites[3].GetComponent<Image>().sprite = ionSpriteArray[8];
            slotSprites[4].GetComponent<Image>().sprite = ionSpriteArray[8];
            slotSprites[5].GetComponent<Image>().sprite = ionSpriteArray[8];

            slotSprites[0].GetComponent<Image>().name = ionSpriteArray[2].name;
            slotSprites[1].GetComponent<Image>().name = ionSpriteArray[2].name;
            slotSprites[2].GetComponent<Image>().name = ionSpriteArray[2].name;
            slotSprites[3].GetComponent<Image>().name = ionSpriteArray[8].name;
            slotSprites[4].GetComponent<Image>().name = ionSpriteArray[8].name;
            slotSprites[5].GetComponent<Image>().name = ionSpriteArray[8].name;
        }


    }
    void randomDragPanelOrder()
    {

        for (int i = 0; i < DragPanel.childCount; i++)
        {
            DragPanel.GetChild(i).SetSiblingIndex(Random.Range(0, DragPanel.childCount));
        }

    }

}
