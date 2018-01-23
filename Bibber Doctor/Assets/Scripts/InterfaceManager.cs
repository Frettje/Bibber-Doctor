using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class InterfaceManager : MonoBehaviour {

    public CanvasGroup LockScreen;
    public CanvasGroup searchInterface;
    public CanvasGroup keyboard;
    public CanvasGroup patientScreen;
    public CanvasGroup orderedScreen;
    public CanvasGroup patientNotFound;

    public Text input;
    public Text id;
    public Text age;
    public Text fullName;
    public Text allergies;
    public DropDownList medicines;
    public GameObject openDrawerError;

    public Text orderedText;

    public LoadXML database;
    public VANASDrawers drawers;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        LoadMedicine();
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
        medicines.Items.Clear();

        for (int i = 0; database.medi != null && database.medi.mMedicines != null && i < database.medi.mMedicines.Count; i++)
        {
            DropDownListItem listItem = new DropDownListItem(database.medi.mMedicines[i].mName + "\t" + "(" + database.medi.mMedicines[i].mQuantity + ")", i.ToString());
            medicines.Items.Add(listItem);
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

    public void Order()
    {
        if (drawers.CheckForOpenDrawers())
        {
            StartCoroutine(OpenDrawerError());
        }
        else
        {
            Medicine selectedMedicine = database.medi.mMedicines[int.Parse(medicines.SelectedItem.ID)];
            bool locationFound = false;
            int drawerId = 0;

            for (int i = 0; database.medi != null && database.medi.mDrawers != null && !locationFound && i < database.medi.mDrawers.Count; i++)
            {
                for (int j = 0; database.medi.mDrawers[i].mMedicines != null && !locationFound && j < database.medi.mDrawers[i].mMedicines.Count; j++)
                {
                    if (database.medi.mDrawers[i].mMedicines[j] == selectedMedicine.mID)
                    {
                        drawerId = i;
                        locationFound = true;
                        Debug.Log("found");
                    }
                }
            }

            orderedText.text = selectedMedicine.mName + " ordered, opening drawer " + drawerId + "\nPlease stand clear.";

            drawers.ToggleLock(drawerId, true);

            patientScreen.alpha = 0;
            patientScreen.interactable = false;
            patientScreen.blocksRaycasts = false;

            orderedScreen.alpha = 1;
            orderedScreen.interactable = true;
            orderedScreen.blocksRaycasts = true;

            StartCoroutine(CloseOrderedScreen(drawerId));
        }
    }

    public IEnumerator CloseOrderedScreen(int drawerId)
    {
        yield return new WaitForSeconds(5f);

        drawers.CheckIfDrawerLockable(drawerId);

        orderedScreen.alpha = 0;
        orderedScreen.interactable = false;
        orderedScreen.blocksRaycasts = false;

        patientScreen.alpha = 1;
        patientScreen.interactable = true;
        patientScreen.blocksRaycasts = true;
    }

    public IEnumerator OpenDrawerError()
    {
        openDrawerError.SetActive(true);
        yield return new WaitForSeconds(2f);
        openDrawerError.SetActive(false);
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
