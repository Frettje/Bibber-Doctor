using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Press : MonoBehaviour {

    public float maxClamp = 0.047f;
    public float minClamp = 0.01f;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.gameObject.transform.position = new Vector3(0, Mathf.Clamp(this.gameObject.transform.position.y, minClamp, maxClamp), 0);
	}

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Fluid")
        {
            Destroy(col.gameObject);
        }
    }
}
