using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour {

    public CanvasGroup LockScreen;
    public CanvasGroup searchInterface;
    public CanvasGroup keyboard;
    public CanvasGroup patientID;
    public CanvasGroup patientNotFound;
    public PatientData[] patientList;
    public PatientData patientToTreat;
    public Text input;
    public Text id;
    public Text fullName;
    public Text prescription;

    public void Searchpatient()
    {
        string patientName = input.text;
        int patientId;
        int.TryParse(patientName, out patientId);

        for (int i = 0; i < patientList.Length; i++)
        {
            if(patientList[i].patientName == patientName || patientList[i].ID == patientId)
            {
                id.text = patientList[i].ID.ToString();
                fullName.text = patientList[i].patientName;
                prescription.text = patientList[i].prescription;

                searchInterface.alpha = 0;
                keyboard.alpha = 0;
                keyboard.interactable = false;
                keyboard.blocksRaycasts = false;

                patientID.alpha = 1;
                patientID.interactable = true;
                patientID.blocksRaycasts = true;

                return;
            }
        }

        ToggleNotFound();
    }

	public void Back()
	{
        input.text = null;

        patientID.alpha = 0;
        patientID.interactable = false;
        patientID.blocksRaycasts = false;

        searchInterface.alpha = 1;
        keyboard.alpha = 1;
        keyboard.interactable = true;
        keyboard.blocksRaycasts = true;
	}

    public IEnumerator Unlock()
    {
        LockScreen.transform.GetChild(1).GetComponent<Text>().text = "Welcome";
        yield return new WaitForSeconds(1);

        LockScreen.alpha = 0;

        searchInterface.alpha = 1;
        keyboard.alpha = 1;
        keyboard.interactable = true;
        keyboard.blocksRaycasts = true;
    }

    public void ToggleNotFound()
    {
        if (patientNotFound.interactable == true)
        {
            patientNotFound.alpha = 0;
            patientNotFound.interactable = false;
            patientNotFound.blocksRaycasts = false;

            searchInterface.alpha = 1;
            keyboard.alpha = 1;
            keyboard.interactable = true;
            keyboard.blocksRaycasts = true;
        }
        else
        {
            searchInterface.alpha = 0;
            keyboard.alpha = 0;
            keyboard.interactable = false;
            keyboard.blocksRaycasts = false;

            patientNotFound.alpha = 1;
            patientNotFound.interactable = true;
            patientNotFound.blocksRaycasts = true;
        }
    }
}
