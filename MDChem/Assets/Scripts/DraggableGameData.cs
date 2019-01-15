using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggableGameData
{
    public static ArrayList elementArray = new ArrayList();
    public enum State { Correct, Incorrect };
    public State state;
    public string elements;
    public int score;
    public DraggableGameData(string elements, State state)
    {
        this.elements = elements;
        this.state = state;
       
    }

    public string getElements()
    {
        return elements;
    }


    public State getState()
    {
        return state;
    }

    public override string ToString()
    {
        return "elements --> " + elements + ", state --> " + state;
    }
}
