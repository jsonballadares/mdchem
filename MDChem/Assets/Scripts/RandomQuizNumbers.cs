using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomQuizNumbers : MonoBehaviour
{
    private List<int> numbers;
    public static RandomQuizNumbers insance;
    void Awake()
    {

        

        if (insance == null)
        {
            insance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

    }


    void Start()
    {
        addRandomNumbers();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public int serveNumber()
    {
        if (numbers.Count <= 0 || numbers == null)
        {
            addRandomNumbers();
        }
        int number = numbers[0];
        numbers.RemoveAt(0);
        return number;
    }

    private void addRandomNumbers()
    {
        numbers = new List<int>();
        if (SceneManager.GetActiveScene().name.Equals("Level5") || SceneManager.GetActiveScene().name.Equals("Level8"))
        {
            for (int i = 0; i < QuizManager.questionArraySize; i++)
            {
                int randNum = Random.Range(0, QuizManager.questionArraySize);
                while (numbers.Contains(randNum))
                {
                    randNum = Random.Range(0, QuizManager.questionArraySize);
                }
                numbers.Add(randNum);
            }
        }
        else if (SceneManager.GetActiveScene().name.Equals("Level10"))
        {
            for (int i = 0; i < DropPanelController.arraySize; i++)
            {
                int randNum = Random.Range(0, DropPanelController.arraySize);
                while (numbers.Contains(randNum))
                {
                    randNum = Random.Range(0, DropPanelController.arraySize);
                }
                numbers.Add(randNum);
            }
        }

        foreach (int x in numbers)
        {
            Debug.Log(x);
        }
    }
}
