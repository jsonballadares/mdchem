using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager insance;
    private static int score;

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
        score = 0;
    }

    public void addToScore(int amount){
        score += amount;
    }

    public void zeroScore(){
        score = 0;
    }

    public int getScore(){
        return score;
    }

    public void Destroy(){
        Destroy(gameObject);
    }
}
