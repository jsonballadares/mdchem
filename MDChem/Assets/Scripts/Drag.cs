using System.Collections.Generic;
using FullSerializer;
using System;

/*
This is the data model for all the levels that require dragging so the beginner levels
and levels 6 7 9
 */
public class Drag
{
    public string levelID;
    private static readonly fsSerializer _serializer = new fsSerializer();
    public int score;
    public static List<string> correctDataStatic = new List<string>();
    public static List<string> incorrectDataStatic = new List<string>();
    public List<string> correctData;
    public List<string> incorrectData;

    /*
    Initializes values for a drag object
     */
    public Drag(string levelID, int score)
    {
        this.score = score; /* score for the level */
        this.levelID = levelID; /* the level we are on */
        incorrectData = incorrectDataStatic;
        correctData = correctDataStatic;
    }

    /*
    Returns a Drag object as a JSON
     */
    public string toJSON()
    {
        return Serialize(typeof(Drag), this);
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

    /*
    clears the static arrays so we dont have any overlapping data
     */
    public static void clearArrays()
    {
        correctDataStatic.Clear();
        incorrectDataStatic.Clear();
    }
}
