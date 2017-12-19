using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System;
using System.IO;


public class CabinetDrawer
{
    [XmlAttribute]
    public int mID;
    public List<Medicine> mMedicines;
    public List<DeliveryTool> mDeliveryTools;
    public bool mIsLocked;

    public CabinetDrawer()
    {
        mID = 0;
        mMedicines = new List<Medicine>();
        mDeliveryTools = new List<DeliveryTool>();
        mIsLocked = false;
    }

}

