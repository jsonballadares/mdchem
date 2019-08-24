using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizData : MonoBehaviour
{
    public static ArrayList elementArray = new ArrayList();
    public State state;
    public enum State { Correct, Incorrect, Missed};
    private string questionText;
    private int duration;

    public QuizData(string questionText,State state,int duration)
    {
        this.questionText = questionText;
        this.state = state;
        this.duration = duration;
    }

    public string getQuestionText()
    {
        return questionText;
    }

    public int getDuration(){
        return duration;
    }

    public override string ToString()
    {
        return "questionText --> " + questionText + ", duration --> " + duration + ", state --> " + state;
    }
}
