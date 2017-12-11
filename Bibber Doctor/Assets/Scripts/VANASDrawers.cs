using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class VANASDrawers : MonoBehaviour {

    public GameObject[] drawers;
    public GameObject[] contents;

    public float openingMin = 0.07F;
    public float openingMax = 0.26f;
    public float openingIncrement;

    bool unlock = true;

    void Start () {
		foreach (GameObject drawer in drawers)
        {
            // Turns off the light of each drawer and locks them in closed position.

            drawer.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            drawer.GetComponent<VRTK_InteractableObject>().enabled = false;
            drawer.transform.GetChild(1).gameObject.SetActive(false);
        }
	}

    private void Update()       // For testing
    {
        int drawerNmbr = 0;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleLock(drawerNmbr, unlock);
            unlock = !unlock;
        }
    }                           //

    public void ToggleLock(int drawerNmbr, bool unlock)
    {
        // Toggles between drawers locked and unlocked, light on and off

        if (unlock)
        {
            drawers[drawerNmbr].GetComponent<Rigidbody>().constraints &= ~RigidbodyConstraints.FreezePositionZ;
        }
        else
        {
            drawers[drawerNmbr].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }

        drawers[drawerNmbr].transform.GetChild(1).gameObject.SetActive(unlock);
        drawers[drawerNmbr].GetComponent<VRTK_InteractableObject>().enabled = unlock;
        contents[drawerNmbr].SetActive(unlock);

        // Calculate opening by number of items in contents

        //drawers[drawerNmbr].GetComponent<ConfigurableJoint>().linearLimit.limit = openingMin;
    }

    public bool CheckForOpenDrawers()
    {
        foreach (GameObject drawer in drawers)
        {
            if (drawer.transform.GetChild(1).gameObject.activeSelf == true)
            {
                return true;
            }
        }
        return false;
    }
}
