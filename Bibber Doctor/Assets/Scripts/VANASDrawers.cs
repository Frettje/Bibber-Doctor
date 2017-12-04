using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VANASDrawers : MonoBehaviour {

    public GameObject[] drawers;

	// Use this for initialization
	void Start () {
		foreach (GameObject drawer in drawers)
        {
            drawer.GetComponent<VRTK.VRTK_InteractableObject>().isGrabbable = false;
            drawer.transform.GetChild(0).gameObject.SetActive(false);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void UnlockDrawer(int drawerNmbr)
    {
        drawers[drawerNmbr].transform.GetChild(0).gameObject.SetActive(true);
        drawers[drawerNmbr].GetComponent<VRTK.VRTK_InteractableObject>().isGrabbable = true;
    }
}
