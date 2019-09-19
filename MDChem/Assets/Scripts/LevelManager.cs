using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public string levelText;
        public int unlocked;
        public bool isInteracteable;
        public Button.ButtonClickedEvent onClickEvent;
    }

    public List<Level> levelList;
    public GameObject levelButton;
    public Transform spacer;

    void Awake()
    {
        //PlayerPrefs.DeleteAll();
        //Debug.Log("UUID IN LEVELMANAGER IS FROM SHARED PREF ----> " + PlayerPrefs.GetString("ui"));
        if (GameObject.FindGameObjectWithTag("AudioManager") != null)
        {
            FindObjectOfType<AudioManager>().StopAllAudio();

        }
    }

    void Start()
    {
        if (GameObject.FindGameObjectWithTag("AudioManager") != null)
        {
            FindObjectOfType<AudioManager>().Play("levelselectreggae");
        }
        /*
        how we will send data using PostData
         */
        /*         if (GameObject.Find("WebRequestManager") != null)
                {
                    Dictionary<string,string> d = new Dictionary<string, string>();
                    d.Add("score","300");
                    d.Add("uuid","IS2mQ1a8UfVyZYxcZLFKpKgdhry1");
                    d.Add("levelid","1a");

                    FindObjectOfType<WebRequest>().PostData(d,FindObjectOfType<WebRequest>().buildJSON());
                } */

        fillList();



        // PlayerPrefs.SetInt("Level1_score",350);

    }
    void fillList()
    {
        foreach (var level in levelList)
        {
            GameObject newButton = Instantiate(levelButton) as GameObject;
            LevelButton button = newButton.GetComponent<LevelButton>();
            button.LevelText.text = level.levelText;
            //Level1, Level2, Level3
            if (PlayerPrefs.GetInt("Level" + button.LevelText.text) == 1)
            {
                level.unlocked = 1;
                level.isInteracteable = true;
            }

            button.unlocked = level.unlocked;
            button.GetComponent<Button>().interactable = level.isInteracteable;
            newButton.GetComponent<Button>().onClick = level.onClickEvent;

            /*
            this is the unlocks the stars for 
             */

            if (PlayerPrefs.GetInt("Level" + button.LevelText.text + "_score") >= 700)
            {
                if (AchievementManager.THIS)
                {
                    AchievementManager.THIS.UnlockAchievement(2);
                }
            }

            if (PlayerPrefs.GetInt("Level" + button.LevelText.text + "_score") >= 300)
            {
                if (AchievementManager.THIS)
                {
                    AchievementManager.THIS.UnlockAchievement(4);
                }
            }


            Debug.Log("button.LevelText.text ---> " + button.LevelText.text);
            if (PlayerPrefs.GetInt("Level" + button.LevelText.text + "_score") >= 500)
            {
                button.star1Locked.SetActive(false);
                button.star1Unlocked.SetActive(true);
                button.star2Locked.SetActive(false);
                button.star2Unlocked.SetActive(true);
                button.star3Locked.SetActive(false);
                button.star3Unlocked.SetActive(true);
            }
            else if (PlayerPrefs.GetInt("Level" + button.LevelText.text + "_score") >= 400)
            {
                button.star1Locked.SetActive(false);
                button.star1Unlocked.SetActive(true);
                button.star2Locked.SetActive(false);
                button.star2Unlocked.SetActive(true);
            }
            else if (PlayerPrefs.GetInt("Level" + button.LevelText.text + "_score") >= 300)
            {
                button.star1Locked.SetActive(false);
                button.star1Unlocked.SetActive(true);
            }
            else if (PlayerPrefs.GetInt("Level" + button.LevelText.text + "_score") == -1)
            {
                button.star1Locked.SetActive(true);
                button.star1Unlocked.SetActive(false);
                button.star2Locked.SetActive(true);
                button.star2Unlocked.SetActive(false);
                button.star3Locked.SetActive(true);
                button.star3Unlocked.SetActive(false);
            }






            newButton.transform.SetParent(spacer, false);
        }
        SaveAll();
    }



    void SaveAll()
    {
        GameObject[] allButtons = GameObject.FindGameObjectsWithTag("LevelButton");
        foreach (GameObject buttons in allButtons)
        {
            LevelButton button = buttons.GetComponent<LevelButton>();
            PlayerPrefs.SetInt("Level" + button.LevelText.text, button.unlocked);
        }
    }

    void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }


}
