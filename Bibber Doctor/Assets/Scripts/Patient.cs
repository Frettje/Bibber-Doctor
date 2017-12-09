using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Genders {male, female};

public class Patient : MonoBehaviour {

    public int ID;
    public string patientName;
    public int patientAge;
    public Genders gender;
    public float weigth;
    public bool pregnant = false;
    public List<string> complications = new List<string>();
    public List<string> previousDosages = new List<string>();
    public string prescription;


	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
