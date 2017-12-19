using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

public class GenerateXMLOnStart : MonoBehaviour {
    [SerializeField]
    public Canvas ParentCanvas;
    public GameObject PatientUIPrefab;

    protected MedicalAppData medi = new MedicalAppData();
    private const string XMLFILE = "medidata.xml";

    //TODO: patient, cabinet, cabinetdrawer
    
    // Use this for initialization
    void Start ()
    {
        //Write XML
        medi.mPatients.Add(new Patient());
        medi.mPatients.Add(new Patient());

        CabinetDrawer cd = new CabinetDrawer();
        cd.mID = 0;
        cd.mIsLocked = true;  

      //  medi.mCabinets.Add(new Cabinet());



        MedicalAppData.WriteToFile(ref medi, XMLFILE);

        //Load XML
        MedicalAppData loaded;
        loaded = readXml(XMLFILE);
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private MedicalAppData readXml(string xmlFilePath)
    {
        MedicalAppData loaded;
        bool succes = MedicalAppData.ReadFromFile(xmlFilePath, out loaded);
        Debug.Log(succes);

        return loaded;
    }
}
