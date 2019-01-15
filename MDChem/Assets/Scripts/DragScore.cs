using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragScore : MonoBehaviour
{

    public Text correctScore;
    public Text incorrectScore;
    void Start()
    {
        correctScore.text = DragManager.rightCounter.ToString();
        incorrectScore.text = DragManager.wrongCounter.ToString();
    }

}
