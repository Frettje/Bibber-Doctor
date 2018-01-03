using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Syringe : MonoBehaviour {

    public GameObject needle = null;
    public GameObject needlePlace;
    public Vector3 newNeedlePos;
    public Vector3 needlePos;
    public bool hasNeedle = false;
    public Collider[] detectedNeedles = new Collider[1];
    public GameObject[] needles = new GameObject[27];
    public SphereCollider radius;
    public GameObject needleCol;

    protected float dist = 5.0f;

    //public GameObject press;

	// Use this for initialization
	void Start ()
    {
        newNeedlePos = needlePlace.transform.position;
        radius = needlePlace.GetComponent<SphereCollider>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //needlePos = needle.transform.position;
        //GetNeedle();

        //CheckNeedleProximity();

        Debug.Log(hasNeedle);
        Debug.Log(Vector3.Distance(needlePos, newNeedlePos));
        //Debug.Log(needle);
	}

    //public void GetNeedle()
    //{
    //    detectedNeedles = Physics.OverlapSphere(newNeedlePos, 0.1f);
    //    Debug.Log(detectedNeedles[0].gameObject.name);

    //    foreach(Collider col in detectedNeedles)
    //    {
    //        if (col.gameObject.tag == "needle" && hasNeedle == false)
    //        {
    //            needle = detectedNeedles[0].gameObject;
    //            hasNeedle = true;
    //            if (Vector3.Distance(needlePos, newNeedlePos) > 0.1f)
    //            {
    //                detectedNeedles[0] = null;
    //                hasNeedle = false;
    //            }
    //        }
    //    }

    //    needle.transform.position = newNeedlePos;
    //    needle.transform.parent = this.transform;

    //}

    public void CheckNeedleProximity()
    {
        for (int i = 0; i < needles.Length; i++)
        {
            if(Vector3.Distance(needles[i].transform.position, newNeedlePos) < dist && !hasNeedle)
            {
                hasNeedle = true;
                needle = needles[i];
                needlePos = newNeedlePos;
                needle.transform.parent = this.transform;

            }
            Debug.Log(Vector3.Distance(needles[i].transform.position, newNeedlePos));
        }

        if(Vector3.Distance(needlePos, newNeedlePos) > dist)
        {
            hasNeedle = false;
            needle = null;
        }
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
}
