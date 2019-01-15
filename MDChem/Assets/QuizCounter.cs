using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizCounter : MonoBehaviour
{
    private static int counter;
    public static QuizCounter insance;
    void Start()
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
        counter = 0;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
    public void clearCounter()
    {
        counter = 0;
    }
    public void addToCounter()
    {
        counter++;
    }

    public int getCount(){
        return counter;
    }
}
