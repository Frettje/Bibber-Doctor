using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VANASScanner : MonoBehaviour {

    public InterfaceManager VANASInterface;
    public Light scanFeedback;
    public AudioSource beep;
    public Color defaultColor;

    void OnCollisionEnter (Collision col)
    {
        if (col.gameObject.name == "Scanner")
        {
            StartCoroutine(OnScan());
            StartCoroutine(VANASInterface.Unlock());
        }
    }

    IEnumerator OnScan ()
    {
        scanFeedback.color = Color.green;
        beep.Play();
        yield return new WaitForSeconds(1);
        scanFeedback.color = defaultColor;
    }
}
