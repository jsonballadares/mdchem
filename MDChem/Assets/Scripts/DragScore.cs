using UnityEngine;
using UnityEngine.UI;
/*
used to update the scores for the drag games
 */
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
