using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePageManager : MonoBehaviour
{

    public void onStartGameButtonPress(){
        FindObjectOfType<SceneFader>().FadeTo("LevelSelector");
    }

    public void onAboutButtonPress(){
        
    }

    public void onPeriodicTableButtonPress(){

    }

    public void onLeaderBoardButtonPress(){

    }

}
