using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour {

    public GameObject keyboard;
    public Canvas searchInterface;
    public Canvas patientID;
    public Patient[] patientList;
    public Patient patientToTreat;
    public Text input;
    public Text id;
    public Text fullName;
    public Text prescription;
    public string inputStr;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        inputStr = input.text;
	}

    public void Searchpatient(string patientName)
    {
        patientName = inputStr;

        for (int i = 0; i < patientList.Length; i++)
        {
            if(patientList[i].name == patientName || patientList[i].ID == int.Parse(patientName))
            {
                id.text = patientList[i].ID.ToString();
                fullName.text = patientList[i].name;
                prescription.text = patientList[i].prescription;
            }
        }

        searchInterface.enabled = false;
        keyboard.SetActive(false);
        patientID.enabled = true;

    }

	public void Back(string patienName)
	{
        patientID.enabled = false;
        searchInterface.enabled = true;
        keyboard.SetActive(true);
	}

}
