using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreJSON
{
    public int score;
    public string user;

    public static HighScoreJSON CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<HighScoreJSON>(jsonString);
    }
}
