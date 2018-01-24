using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syringe : MonoBehaviour {

    public GameObject needle = null;
    public GameObject needlePlace;
    public Vector3 newNeedlePos;
    public Vector3 needlePos;
    public bool hasNeedle = false;
    public SphereCollider radius;

    //Needle Parent
    public GameObject needleCol;

    //Fluid
    public GameObject fluid;
    public GameObject press;
    public float pressYPos;

    protected float dist = 5.0f;

    //public GameObject press;

	// Use this for initialization
	void Start ()
    {
        newNeedlePos = needlePlace.transform.position;
        radius = GetComponent<SphereCollider>();
        pressYPos = press.transform.position.y;
	}
	
	// Update is called once per frame
	void Update ()
    {
        AddFluid();

        Debug.Log(hasNeedle);
        Debug.Log(Vector3.Distance(needlePos, newNeedlePos));
        //Debug.Log(needle);
	}

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Needle" && !hasNeedle)
        {
            needle = other.gameObject;
            needle.transform.parent = this.transform;
            needle.transform.position = newNeedlePos;
            hasNeedle = true;
        }

        Debug.Log(other.gameObject);
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject == needle && hasNeedle)
		{
			needle = null;
            needle.transform.parent = needleCol.transform;
            hasNeedle = false;
		}
    }

    public void AddFluid()
    {
        if (press.transform.position.y > pressYPos && pressYPos <= 0.047f)
        {
            Instantiate(fluid);
            pressYPos = press.transform.position.y;
        }
    }
}
