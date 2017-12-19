using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System;
using System.IO;

[Serializable]
public enum PatientType
{
    adult = 1,
    child,
    pregnant,
    senior
}

public enum Sex
{
    male = 1,
    female,
    X
}


public class Patient
{
    [XmlAttribute]
    public int mID;
    public string mName;
    public int mAge;
    public int mWeight;
    public PatientType mType;
    public Sex mSex;
    public List<string> mAllergies;

    public Patient()
    {
        mID = 0;
        mName = "John Doe";

        mAge = 45;
        mWeight = 80;
        mSex = Sex.male;
        mAllergies = new List<string>();

        mType = PatientType.adult;
    }
}
