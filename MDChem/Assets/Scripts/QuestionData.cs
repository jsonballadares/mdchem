using System;
using System.Collections;
using System.Collections.Generic;
using FullSerializer;
using UnityEngine;

public class QuestionData
{
    public string levelID;
    private static readonly fsSerializer _serializer = new fsSerializer();
    public int score;
    public static List<string> correctQuestions = new List<string>();
    public static List<string> incorrectQuestions = new List<string>();
    public List<string> correctData;
    public List<string> incorrectData;

     public QuestionData(string levelID, int score)
    {
        this.score = score;
        this.levelID = levelID;
        incorrectData = incorrectQuestions;
        correctData = correctQuestions;
    }
    /*
    Returns a student object as a JSON
     */
    public string toJSON()
    {
        return Serialize(typeof(QuestionData), this);
    }

    /*
    Method that actually serializes the object. it does all of the heavy lifting using fullserializer
     */
    public static string Serialize(Type type, object value)
    {
        // serialize the data
        fsData data;
        _serializer.TrySerialize(type, value, out data).AssertSuccessWithoutWarnings();
        // emit the data via JSON
        return fsJsonPrinter.CompressedJson(data);
    }

    public static void clearArrays()
    {
        correctQuestions.Clear();
        incorrectQuestions.Clear();
    }

}
