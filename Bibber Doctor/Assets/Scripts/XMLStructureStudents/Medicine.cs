using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System;
using System.IO;

[Serializable]
public enum Unit
{
    ml,
    units,
    l
}

[Serializable]
public enum Package
{
    box,
    flask,
    bottle,
    baxter
}

public class Medicine  {
    [XmlAttribute]
    public int mID;
    public string mName;
    public int quantity;
    public Unit mUnit;
    public Package mPackage;
    public List<PointsOfAttention> mPointsOfAttention;

	public Medicine()
    {
        mName = "";
        mID = 0;
        quantity = 0;
        mUnit = Unit.ml;
        mPackage = Package.box;
        mPointsOfAttention = new List<PointsOfAttention>();
    }
}
