using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VANASScanner : MonoBehaviour {

    public GameObject VANASInterface;
    public Light scanFeedback;
    public AudioSource beep;
    public Color defaultColor;

    void OnCollisionEnter (Collision col)
    {
        if (col.gameObject.name == "Scanner")
        {
            StartCoroutine(OnScan());
            // Send ID OK
        }
    }

    IEnumerator OnScan ()
    {
        // Play sound
        scanFeedback.color = Color.green;
        beep.Play();
        yield return new WaitForSeconds(1);
        scanFeedback.color = defaultColor;
    }
}
