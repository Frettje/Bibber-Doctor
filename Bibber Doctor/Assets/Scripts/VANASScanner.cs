using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VANASScanner : MonoBehaviour {

    public GameObject VANASInterface;
    public Light scanFeedback;

    void OnCollisionEnter (Collision col)
    {
        if (col.gameObject.name == "Badge")
        {
            StartCoroutine(OnScan());
            // Send ID OK
        }
    }

    IEnumerator OnScan ()
    {
        // Play sound
        scanFeedback.color = Color.green;
        yield return new WaitForSeconds(1);
        scanFeedback.color = new Color (38, 225, 240);
    }
}
