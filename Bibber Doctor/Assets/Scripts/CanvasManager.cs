using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour {

    public Image img;
    public Text txt;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void Search()
    {
        
    }

    public void Activate()
    {
        img.enabled = true;
        txt.enabled = true;
        Debug.Log("clicked");
    }

}
