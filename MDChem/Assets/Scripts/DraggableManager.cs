using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
This class handles every game mode with draggable objects
 */
public class DraggableManager : MonoBehaviour
{
    [SerializeField] Transform DragPanel, DropPanel;
    public static int count = 0;

    [SerializeField]
    private Sprite[] spriteArray;

    [SerializeField]
    private GameObject[] slotArray;

    private static int rightCounter = 0;

    void Start()
    {


        count++;



        //randomize order
        randomDragPanelOrder();

        if (SceneManager.GetActiveScene().name.Equals("Level6"))
        {
            if (count == 1)
            {
                //level 6a match the +1 elements
                //show user dialog of which elements need to be choosen
                for (int i = 0; i < DropPanel.childCount; i++)
                {
                    DropPanel.GetChild(i).tag = "+1";
                }
            }
            else if (count == 2)
            {
                //level 6b match the +2 elements
                //show user dialog of which elements need to be choosen
                for (int i = 0; i < DropPanel.childCount; i++)
                {
                    DropPanel.GetChild(i).tag = "+2";
                }
            }

        }
        else if (SceneManager.GetActiveScene().name.Equals("Level7"))
        {
            if (count == 1)
            {
                DropPanel.GetChild(DropPanel.childCount - 1).gameObject.SetActive(false);
                for (int i = 0; i < DropPanel.childCount; i++)
                {
                    DropPanel.GetChild(i).tag = "-1";
                }
            }
            else if (count == 2)
            {
                DropPanel.GetChild(DropPanel.childCount - 1).gameObject.SetActive(false);
                DropPanel.GetChild(DropPanel.childCount - 2).gameObject.SetActive(false);
                for (int i = 0; i < DropPanel.childCount; i++)
                {
                    DropPanel.GetChild(i).tag = "-2";
                }
            }
            else if (count == 3)
            {
                DropPanel.GetChild(DropPanel.childCount - 1).gameObject.SetActive(false);
                DropPanel.GetChild(DropPanel.childCount - 2).gameObject.SetActive(false);
                DropPanel.GetChild(DropPanel.childCount - 3).gameObject.SetActive(false);
                for (int i = 0; i < DropPanel.childCount; i++)
                {
                    DropPanel.GetChild(i).tag = "-3";
                }
            }

        }
        else if (SceneManager.GetActiveScene().name.Equals("Level9"))
        {

            if (count == 2)
            {
                for (int i = 0; i < slotArray.Length; i++)
                {
                    slotArray[i].GetComponent<Image>().sprite = spriteArray[i];
                    slotArray[i].tag = spriteArray[i].name;
                }
            }

            for (int i = 0; i < DragPanel.childCount; i++)
            {
                DragPanel.GetChild(i).SetSiblingIndex(Random.Range(0, DragPanel.childCount));

            }

            for (int i = 0; i < DropPanel.childCount; i++)
            {
                DropPanel.GetChild(i).SetSiblingIndex(Random.Range(0, DropPanel.childCount));
            }



        }


    }

    void randomDragPanelOrder()
    {
        if (SceneManager.GetActiveScene().name == "Level6" || SceneManager.GetActiveScene().name == "Level7" || SceneManager.GetActiveScene().name == "Level9" || SceneManager.GetActiveScene().name == "Level10")
        {
            for (int i = 0; i < DragPanel.childCount; i++)
            {
                DragPanel.GetChild(i).SetSiblingIndex(Random.Range(0, DragPanel.childCount));
            }
        }
    }

    public void progressButtonPress()
    {
        if (SceneManager.GetActiveScene().name.Equals("Level6"))
        {
            if (DraggableManager.count == 1)
            {

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
            else if (DraggableManager.count == 2)
            {
                if (PlayerPrefs.GetInt("levelReached") < 7)
                {
                    PlayerPrefs.SetInt("levelReached", 7);
                }
                count = 0;
                FindObjectOfType<AudioManager>().Pause("quizgamenoise");
                SceneManager.LoadScene("LevelSelector");
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level7"))
        {
            if (DraggableManager.count == 1 || DraggableManager.count == 2)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                if (PlayerPrefs.GetInt("levelReached") < 8)
                {
                    PlayerPrefs.SetInt("levelReached", 8);
                }
                count = 0;
                FindObjectOfType<AudioManager>().Pause("quizgamenoise");
                SceneManager.LoadScene("LevelSelector");
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level9"))
        {
            if (DraggableManager.count == 1)
            {

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            }
            else if (DraggableManager.count == 2)
            {
                if (PlayerPrefs.GetInt("levelReached") < 10)
                { 
                    PlayerPrefs.SetInt("levelReached", 10);
                }
                count = 0;
                FindObjectOfType<AudioManager>().Pause("quizgamenoise");
                SceneManager.LoadScene("LevelSelector");
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level10"))
        {
            if (DropPanelController.currentSpriteName.Equals("sodium bromide"))
            {
                string answer = "";
                for (int i = 0; i < DropPanel.childCount; i++)
                {
                    answer += DropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (DropPanel.childCount > 0)
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
                for (int i = 0; i < DropPanel.childCount; i++)
                {
                    answer += DropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (DropPanel.childCount > 0)
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
                for (int i = 0; i < DropPanel.childCount; i++)
                {
                    answer += DropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (DropPanel.childCount > 0)
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
                for (int i = 0; i < DropPanel.childCount; i++)
                {
                    answer += DropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (DropPanel.childCount > 0)
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
                for (int i = 0; i < DropPanel.childCount; i++)
                {
                    answer += DropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (DropPanel.childCount > 0)
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
                for (int i = 0; i < DropPanel.childCount; i++)
                {
                    answer += DropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (DropPanel.childCount > 0)
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
                for (int i = 0; i < DropPanel.childCount; i++)
                {
                    answer += DropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (DropPanel.childCount > 0)
                {
                    if (answer.Equals("al3no3"))
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
                for (int i = 0; i < DropPanel.childCount; i++)
                {
                    answer += DropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (DropPanel.childCount > 0)
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
                for (int i = 0; i < DropPanel.childCount; i++)
                {
                    answer += DropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (DropPanel.childCount > 0)
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
                for (int i = 0; i < DropPanel.childCount; i++)
                {
                    answer += DropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (DropPanel.childCount > 0)
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
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level11"))
        {
            if (DropPanelController.currentSpriteName.Equals("sodium sulfide"))
            {
                string answer = "";
                for (int i = 0; i < DropPanel.childCount; i++)
                {
                    answer += DropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (DropPanel.childCount > 0)
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
                for (int i = 0; i < DropPanel.childCount; i++)
                {
                    answer += DropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (DropPanel.childCount > 0)
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
                for (int i = 0; i < DropPanel.childCount; i++)
                {
                    answer += DropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (DropPanel.childCount > 0)
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
                for (int i = 0; i < DropPanel.childCount; i++)
                {
                    answer += DropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (DropPanel.childCount > 0)
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
                for (int i = 0; i < DropPanel.childCount; i++)
                {
                    answer += DropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (DropPanel.childCount > 0)
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
                for (int i = 0; i < DropPanel.childCount; i++)
                {
                    answer += DropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (DropPanel.childCount > 0)
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
                for (int i = 0; i < DropPanel.childCount; i++)
                {
                    answer += DropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (DropPanel.childCount > 0)
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
                for (int i = 0; i < DropPanel.childCount; i++)
                {
                    answer += DropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (DropPanel.childCount > 0)
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
                for (int i = 0; i < DropPanel.childCount; i++)
                {
                    answer += DropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (DropPanel.childCount > 0)
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
                for (int i = 0; i < DropPanel.childCount; i++)
                {
                    answer += DropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (DropPanel.childCount > 0)
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
                for (int i = 0; i < DropPanel.childCount; i++)
                {
                    answer += DropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (DropPanel.childCount > 0)
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
                for (int i = 0; i < DropPanel.childCount; i++)
                {
                    answer += DropPanel.GetChild(i).name;

                }

                Debug.Log(answer);

                if (DropPanel.childCount > 0)
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
        Debug.Log("correct answer");
        if (rightCounter >= 8)
        {

            FindObjectOfType<AudioManager>().Pause("quizgamenoise");
            rightCounter = 0;
            SceneManager.LoadScene("LevelSelector");
            if (PlayerPrefs.GetInt("levelReached") < 11)
            {
                PlayerPrefs.SetInt("levelReached", 11);
            }

        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

    }
    private void wrong()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        FindObjectOfType<AudioManager>().Play("falsenoise");
        Debug.Log("wrong answer");
    }



}



