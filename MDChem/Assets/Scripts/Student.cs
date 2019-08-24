using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FullSerializer;
using System;

/*
This class will serve as the model for the users of the app
 */
public class Student
{
    /*
    for json converter
     */
    private static readonly fsSerializer _serializer = new fsSerializer();
    /*
    email: students email
    password: students password    
    quesiton: recovery question 
    answer: recovery answer  
    group: class number
    */
    public string email;
    public string password;
    public string quesiton;
    public string answer;
    public int group;
    /* 
    Creates a student object with the given fields
    */
    public Student(string email, string password, string quesiton, string answer, int group)
    {
        this.email = email;
        this.password = password;
        this.quesiton = quesiton;
        this.answer = answer;
        this.group = group;
    }

    public Student(string email, string password)
    {
        this.email = email;
        this.password = password;
    }

    /*
    Returns a student object as a JSON
     */
    public string toJSON()
    {
        return Serialize(typeof(Student), this);
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
}
