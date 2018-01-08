using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceManager : MonoBehaviour {

    public CanvasGroup LockScreen;
    public CanvasGroup searchInterface;
    public CanvasGroup keyboard;
    public CanvasGroup patientScreen;
    public CanvasGroup patientNotFound;

    public LoadXML database;

    public PatientData[] patientList;
    public PatientData patientToTreat;

    public Text input;
    public Text id;
    public Text fullName;
    public Text allergies;
    public Text age;
    public Dropdown medicines;

    void Start()
    {
        LoadMedicine();
    }

    //public void Searchpatient()
    //{
    //    string patientName = input.text;
    //    int patientId;
    //    int.TryParse(patientName, out patientId);

    //    for (int i = 0; i < patientList.Length; i++)
    //    {
    //        if(patientList[i].patientName == patientName || patientList[i].ID == patientId)
    //        {
    //            id.text = patientList[i].ID.ToString();
    //            fullName.text = patientList[i].patientName;
    //            allergies.text = patientList[i].prescription;

    //            searchInterface.alpha = 0;
    //            keyboard.alpha = 0;
    //            keyboard.interactable = false;
    //            keyboard.blocksRaycasts = false;

    //            patientScreen.alpha = 1;
    //            patientScreen.interactable = true;
    //            patientScreen.blocksRaycasts = true;

    //            return;
    //        }
    //    }

    //    ToggleNotFound();
    //}

    public void SearchPatient()
    {
        string patientName = input.text;
        int patientId;
        int.TryParse(patientName, out patientId);

        for (int i= 0; database.medi != null && database.medi.mPatients != null && i < database.medi.mPatients.Count; i++)
        {
            if (database.medi.mPatients[i].mName == patientName || database.medi.mPatients[i].mID == patientId)
            {
                id.text = database.medi.mPatients[i].mID.ToString();
                fullName.text = database.medi.mPatients[i].mName;
                if (database.medi.mPatients[i].mAllergies != null)
                {
                    allergies.text = null;
                    foreach (string allergie in database.medi.mPatients[i].mAllergies)
                    {
                        allergies.text += allergie;
                        allergies.text += "\n";
                    }
                }
                else
                {
                    allergies.text = "None.";
                }
                age.text = database.medi.mPatients[i].mAge.ToString();

                searchInterface.alpha = 0;
                keyboard.alpha = 0;
                keyboard.interactable = false;
                keyboard.blocksRaycasts = false;

                patientScreen.alpha = 1;
                patientScreen.interactable = true;
                patientScreen.blocksRaycasts = true;

                return;
            }
        }

        ToggleNotFound();
    }

    public void LoadMedicine()
    {
        // load xml medication to dropdown

        medicines.ClearOptions();

        for (int i = 0; database.medi != null && database.medi.mMedicines != null && i < database.medi.mMedicines.Count; i++)
        {
            Dropdown.OptionData dDData = new Dropdown.OptionData();
            dDData.text = database.medi.mMedicines[i].mName + "\t" + "(" + database.medi.mMedicines[i].mQuantity + ")";
            medicines.options.Add(dDData);
        }
    }

    public void Back()
	{
        input.text = null;

        patientScreen.alpha = 0;
        patientScreen.interactable = false;
        patientScreen.blocksRaycasts = false;

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
