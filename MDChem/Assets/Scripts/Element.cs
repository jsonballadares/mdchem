using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element
{
    public static ArrayList elementArray = new ArrayList();
    private string name;
    private int duration;    
    public State state;
    public enum State {Correct, Missed, Incorrect};

    public Element(string name,int duration,State state){
        this.name = name;
        this.duration = duration;
        this.state = state;
    }

    public int getDuration(){
        return duration;
    }

    public string getName(){
        return name;
    }

    public State getState(){
        return state;
    }

    public override string ToString()
    {
        return "name --> " + name + ", duration --> " + duration + ", state --> " + state;
    }
}
