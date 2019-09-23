using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{

    public Question[] questions;
    public static List<Question> unansweredQuestions;
    public static Question currentQuestion;

    [SerializeField]
    public Button TrueButton, FalseButton;

    public GameObject trueButton, falseButton, rulesDialog, questionBox, timeManager, trueAnswerText, falseAnswerText;

    [SerializeField]
    private Text factText;

    [SerializeField]
    private float timeBetweenQuestions;

    [SerializeField]
    private Text trueAnswer;

    [SerializeField]
    private Text falseAnswer;

    [SerializeField]
    private Text correctScore;

    [SerializeField]
    private Text incorrectScore;

    [SerializeField]
    private Animator animator;

    public static int score = 0;
    public static int numWrong = 0;
    public SceneFader fader;
    public static int count = 0;
    private static int numberOfAttempts;

    public static int questionArraySize;



    void Start()
    {
        questionArraySize = questions.Length;
        correctScore.text = score.ToString();
        incorrectScore.text = numWrong.ToString();
        Debug.Log("USERID" + PlayerPrefs.GetString("ui"));
        if (count <= 0)
        {
            if (GameObject.FindGameObjectWithTag("AudioManager") != null)
            {
                FindObjectOfType<AudioManager>().StopAllAudio();
                FindObjectOfType<AudioManager>().Play("masterylevelmusic");
            }

            questionBox.SetActive(false);
            trueButton.SetActive(false);
            falseButton.SetActive(false);
            rulesDialog.SetActive(true);
            trueAnswerText.SetActive(false);
            falseAnswerText.SetActive(false);
        }
        else
        {
            questionBox.SetActive(true);
            trueButton.SetActive(true);
            falseButton.SetActive(true);
            rulesDialog.SetActive(false);
            timeManager.SetActive(true);
            trueAnswerText.SetActive(true);
            falseAnswerText.SetActive(true);

        }
        count++;

        if (unansweredQuestions == null || unansweredQuestions.Count == 0)
        {
            Debug.Log("THE UNANSWERED QUESTIONS IS BEING FILLED WITH QUESTIONS");
            unansweredQuestions = questions.ToList<Question>();
        }
        //score 15
        if (score >= 15)
        {
            QuizManager.clearQuestionsList();
            win();
        }
        //6 TO LOOSE
        if (numWrong >= 6)
        {
            QuizManager.clearQuestionsList();
            loose();
        }
        for (int i = 0; i < unansweredQuestions.Count; i++)
        {
            Debug.Log("unanswered question fact at " + i + " " + unansweredQuestions[i].fact);
        }

        if (unansweredQuestions.Count > 0)
        {
            SetCurrentQuestion();
        }


    }



    void SetCurrentQuestion()
    {


        int randomQuestionIndex = FindObjectOfType<RandomQuizNumbers>().serveNumber();
        Debug.Log("THE RANDOM QUESITON INDEX IS ----> " + randomQuestionIndex);
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        factText.text = currentQuestion.fact;

        if (currentQuestion.isTrue)
        {
            trueAnswer.text = "CORRECT!";
            trueAnswer.color = Color.green;
            falseAnswer.text = "INCORRECT!";
            falseAnswer.color = Color.red;

        }
        else
        {
            trueAnswer.text = "INCORRECT!";
            trueAnswer.color = Color.red;
            falseAnswer.text = "CORRECT!";
            falseAnswer.color = Color.green;
        }

    }
    IEnumerator TrasnsitionToNextQuestionTrue()
    {
        yield return new WaitForSeconds(timeBetweenQuestions);
        FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);
    }

    IEnumerator TrasnsitionToNextQuestionFalse()
    {
        yield return new WaitForSeconds(timeBetweenQuestions);
        FindObjectOfType<SceneFader>().FadeTo(SceneManager.GetActiveScene().name);
    }

    public void UserSelectTrue()
    {
        animator.SetTrigger("True");

        if (currentQuestion.isTrue)
        {
            QuestionData.correctQuestions.Add(currentQuestion.fact);
            Debug.Log("THE CURRENT QUESITON IS ---> " + currentQuestion.fact);
            if (GameObject.FindGameObjectWithTag("AudioManager") != null)
            {
                FindObjectOfType<AudioManager>().Play("truenoise");
            }

            score += 1;
            disableButtons();

        }
        else
        {
            QuestionData.incorrectQuestions.Add(currentQuestion.fact);
            Debug.Log("THE CURRENT QUESITON IS ---> " + currentQuestion.fact);

            if (GameObject.FindGameObjectWithTag("AudioManager") != null)
            {
                FindObjectOfType<AudioManager>().Play("falsenoise");
            }
            numWrong += 1;
            disableButtons();

        }
        TimeManager.timeThreshhold = 9.5f;
        StartCoroutine(TrasnsitionToNextQuestionTrue());
    }
    public void UserSelectFalse()
    {
        animator.SetTrigger("False");

        if (!currentQuestion.isTrue)
        {
            QuestionData.correctQuestions.Add(currentQuestion.fact);
            Debug.Log("THE CURRENT QUESITON IS ---> " + currentQuestion.fact);
            if (GameObject.FindGameObjectWithTag("AudioManager") != null)
            {
                FindObjectOfType<AudioManager>().Play("truenoise");
            }
            score += 1;
            disableButtons();

        }
        else
        {
            QuestionData.incorrectQuestions.Add(currentQuestion.fact);
            Debug.Log("THE CURRENT QUESITON IS ---> " + currentQuestion.fact);
            if (GameObject.FindGameObjectWithTag("AudioManager") != null)
            {
                FindObjectOfType<AudioManager>().Play("falsenoise");
            }
            numWrong += 1;
            disableButtons();
        }
        TimeManager.timeThreshhold = 9.5f;
        StartCoroutine(TrasnsitionToNextQuestionFalse());
    }

    public void win()
    {
        int points = 0;
        if (SceneManager.GetActiveScene().name.Equals("Level5"))
        {
            PlayerPrefs.SetInt("Level6", 1);
            if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
            {
                Debug.Log("THE COUNT IS ----> " + FindObjectOfType<QuizCounter>().getCount());
                if (FindObjectOfType<QuizCounter>().getCount() <= 0)
                {

                    //3stars
                    if (numWrong == 0)
                    {
                        if (AchievementManager.THIS)
                        {
                            AchievementManager.THIS.UnlockAchievement(0);
                        }
                    }
                    points = 500;
                    if (PlayerPrefs.GetInt("Level5_score") < points)
                    {
                        PlayerPrefs.SetInt("Level5_score", points);
                    }

                }
                else if (FindObjectOfType<QuizCounter>().getCount() == 1)
                {
                    //2stars
                    points = 400;
                    if (PlayerPrefs.GetInt("Level5_score") < points)
                    {
                        PlayerPrefs.SetInt("Level5_score", points);
                    }
                }
                else
                {
                    points = 300;
                    if (PlayerPrefs.GetInt("Level5_score") < points)
                    {
                        PlayerPrefs.SetInt("Level5_score", points);
                    }
                }

                // if (GameObject.FindGameObjectWithTag("WebRequestManager") != null)
                // {
                //     Dictionary<string, string> d = new Dictionary<string, string>();
                //     d.Add("score", points.ToString());
                //     d.Add("uuid", PlayerPrefs.GetString("ui"));
                //     d.Add("levelid", "5");
                //     FindObjectOfType<WebRequest>().PostData(d, FindObjectOfType<WebRequest>().buildJSONQuiz());

                // }

                Debug.Log("question data is being SENT ------------");
                QuestionData d = new QuestionData("5", points);
                Debug.Log("DA JSON -----> " + d.toJSON());
                StartCoroutine(WebRequestManager.sendData(Enviorment.URL + "/api/player/", d.toJSON()));
                QuestionData.clearArrays();
                Debug.Log("question data is being SENT ------------");

                FindObjectOfType<QuizCounter>().Destroy();

            }
            score = 0;
            count = 0;
            numWrong = 0;
            //FindObjectOfType<AudioManager>().Stop("masterylevelmusic");
            if (GameObject.FindGameObjectWithTag("RandomQuizNumbers") != null)
            {
                FindObjectOfType<RandomQuizNumbers>().Destroy();
            }


            //FindObjectOfType<SceneFader>().FadeTo("LevelSelector");

            SceneManager.LoadScene("LevelSelector");
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level8"))
        {
            PlayerPrefs.SetInt("Level9", 1);
            if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
            {
                Debug.Log("THE COUNT IS ----> " + FindObjectOfType<QuizCounter>().getCount());
                if (FindObjectOfType<QuizCounter>().getCount() <= 0)
                {
                    if (numWrong == 0)
                    {
                        if (AchievementManager.THIS)
                        {
                            AchievementManager.THIS.UnlockAchievement(1);
                            //just getting started!
                        }
                    }

                    //3stars
                    points = 500;
                    if (PlayerPrefs.GetInt("Level8_score") < points)
                    {
                        PlayerPrefs.SetInt("Level8_score", points);
                    }

                }
                else if (FindObjectOfType<QuizCounter>().getCount() == 1)
                {
                    //2stars
                    points = 400;
                    if (PlayerPrefs.GetInt("Level8_score") < points)
                    {
                        PlayerPrefs.SetInt("Level8_score", points);
                    }
                }
                else
                {
                    points = 300;
                    if (PlayerPrefs.GetInt("Level8_score") < points)
                    {
                        PlayerPrefs.SetInt("Level8_score", points);
                    }
                }
                QuestionData d = new QuestionData("8", points);
                StartCoroutine(WebRequestManager.sendData(Enviorment.URL + "/api/player/", d.toJSON()));
                Drag.clearArrays();
                // if (GameObject.FindGameObjectWithTag("WebRequestManager") != null)
                // {
                //     Dictionary<string, string> d = new Dictionary<string, string>();
                //     d.Add("score", points.ToString());
                //     d.Add("uuid", PlayerPrefs.GetString("ui"));
                //     d.Add("levelid", "8");
                //     FindObjectOfType<WebRequest>().PostData(d, FindObjectOfType<WebRequest>().buildJSONQuiz());

                // }

                FindObjectOfType<QuizCounter>().Destroy();
            }
            score = 0;
            count = 0;
            numWrong = 0;
            //FindObjectOfType<AudioManager>().Stop("masterylevelmusic");
            if (GameObject.FindGameObjectWithTag("RandomQuizNumbers") != null)
            {
                FindObjectOfType<RandomQuizNumbers>().Destroy();
            }

            SceneManager.LoadScene("LevelSelector");
            //FindObjectOfType<SceneFader>().FadeTo("LevelSelector");
        }
    }

    public void loose()
    {

        if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
        {
            Debug.Log("THE COUNT IS ----> " + FindObjectOfType<QuizCounter>().getCount());
        }

        if (SceneManager.GetActiveScene().name.Equals("Level5"))
        {
            score = 0;
            count = 0;
            numWrong = 0;
            if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
            {
                FindObjectOfType<QuizCounter>().addToCounter();
            }
            //FindObjectOfType<AudioManager>().Stop("masterylevelmusic");
            SceneManager.LoadScene("LevelFiveVideoAnimation");
            //FindObjectOfType<SceneFader>().FadeTo("LevelFiveVideoAnimation");
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level8"))
        {
            score = 0;
            count = 0;
            numWrong = 0;
            if (GameObject.FindGameObjectWithTag("QuizCounter") != null)
            {
                FindObjectOfType<QuizCounter>().addToCounter();
            }
            //FindObjectOfType<AudioManager>().Stop("masterylevelmusic");
            SceneManager.LoadScene("LevelEightVideoAnimation");
            //FindObjectOfType<SceneFader>().FadeTo("LevelEightVideoAnimation");
        }

    }

    public void disableButtons()
    {
        TrueButton.enabled = false;
        FalseButton.enabled = false;
    }

    public static void clearQuestionsList()
    {
        Debug.Log("clearQuestionsList was called");
        for (int i = 0; i < unansweredQuestions.Count; i++)
        {
            Debug.Log("unanswered question fact at " + i + " " + unansweredQuestions[i].fact);
        }
        Debug.Log("CLEARING THE LIST OF QUESTIONS");
        unansweredQuestions.Clear();
        Debug.Log("CLEARED");
        for (int i = 0; i < unansweredQuestions.Count; i++)
        {
            Debug.Log("unanswered question fact at " + i + " " + unansweredQuestions[i].fact);
        }
    }

}
