using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GenerateXMLOnStart : MonoBehaviour {
    [SerializeField]
    public GameObject mParentOfMenuItems;
    public GameObject mPatientUIPrefab;
    public GameObject mMedicineUIPrefab;
    public GameObject mDeliveryToolsUIPrefab;
    public GameObject mDeliveryMethodUIPrefab;
    public GameObject mCabinetUIPrefab;
    public GameObject mDrawerUIPrefab;
    public GameObject mScenarioUIPrefab;
    public Text mFileField;
    public Button mGenericButtonPrefab;

    public MedicalAppData medi = new MedicalAppData();

    
    //TODO: patient, cabinet, cabinetdrawer
    
    // Use this for initialization
    void Start ()
    { 
        RefreshMediDataUI(); 
    }

    public void readXml()
    {
        //clear medi
        medi = new MedicalAppData();
        //load new medi
        bool succes = MedicalAppData.ReadFromFile(mFileField.text, out medi);
        if(succes)
        {
            Debug.Log(mFileField.text + "was loaded");
        }
        else
        {
            Debug.Log("failed to load " + mFileField.text);
        }
        RefreshMediDataUI();
    }

    public void WriteXml()
    {
        bool succes = MedicalAppData.WriteToFile(ref medi, mFileField.text);
        if (succes)
        {
            Debug.Log(mFileField.text + "was written");
        }
        else
        {
            Debug.Log("failed to write " + mFileField.text);
        }
    }

    

    public void RefreshMediDataUI()
    {
    //clear parent UI of all medi elements
        while (mParentOfMenuItems.transform.childCount > 0)
        {
            DestroyImmediate(mParentOfMenuItems.transform.GetChild(0).gameObject);
        }
        //create patients Menu button
        GameObject instance = GameObject.Instantiate(mGenericButtonPrefab.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
        instance.transform.SetParent(mParentOfMenuItems.transform, false);
        instance.GetComponentsInChildren<Text>()[0].text = " Patients";
        instance.GetComponent<Button>().onClick.AddListener(AddPatient);
        //create patient panels
        for (int i = 0; medi!= null && medi.mPatients!= null && i < medi.mPatients.Count; i++)
        {
            instance = GameObject.Instantiate(mPatientUIPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            instance.transform.SetParent(mParentOfMenuItems.transform, false);
            Dropdown[] dropdowns;
            //get 2 dropdowns for sex and type
            dropdowns = instance.GetComponentsInChildren<Dropdown>();
            dropdowns[1].value = (int)medi.mPatients[i].mSex;
            dropdowns[1].onValueChanged.AddListener(delegate { DataChanged("patient",i); });
            dropdowns[0].value = (int)medi.mPatients[i].mType;
            dropdowns[0].onValueChanged.AddListener(delegate { DataChanged("patient",i);});
            //get name field
            InputField[] nameField = instance.GetComponentsInChildren<InputField>();
            nameField[0].text = medi.mPatients[i].mName;
            nameField[0].onEndEdit.AddListener(delegate { DataChanged("patient", i); });
            nameField[1].text = medi.mPatients[i].mAge.ToString();
            nameField[1].onEndEdit.AddListener(delegate { DataChanged("patient", i); });
            nameField[2].text = medi.mPatients[i].mWeight.ToString();
            nameField[2].onEndEdit.AddListener(delegate { DataChanged("patient", i); });
        }

        //create medicines Menu button
        instance = GameObject.Instantiate(mGenericButtonPrefab.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
        instance.transform.SetParent(mParentOfMenuItems.transform, false);
        instance.GetComponentsInChildren<Text>()[0].text = " Medicines";
        instance.GetComponent<Button>().onClick.AddListener(AddMedicine);
        //create medicines panels
        for (int i = 0; medi != null && medi.mMedicines != null && i < medi.mMedicines.Count; i++)
        {
            instance = GameObject.Instantiate(mMedicineUIPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            instance.transform.SetParent(mParentOfMenuItems.transform, false);
            Component[] dropdowns;
            //fill 2 dropdowns for quantity and unit
            dropdowns = instance.GetComponentsInChildren<Dropdown>();
            ((Dropdown)dropdowns[0]).value = (int)medi.mMedicines[i].mUnit;
            ((Dropdown)dropdowns[0]).onValueChanged.AddListener(delegate { DataChanged("medicine",i);});
            ((Dropdown)dropdowns[1]).value = (int)medi.mMedicines[i].mPackage;
            ((Dropdown)dropdowns[1]).onValueChanged.AddListener(delegate { DataChanged("medicine",i);});
            //fill txt fields
            InputField[] nameField = instance.GetComponentsInChildren<InputField>();
            nameField[0].text = medi.mMedicines[i].mName;
            nameField[0].onEndEdit.AddListener(delegate { DataChanged("medicine",i);});
            nameField[1].text = medi.mMedicines[i].mQuantity.ToString();
            nameField[1].onEndEdit.AddListener(delegate { DataChanged("medicine", i); });
            nameField[2].text = medi.mMedicines[i].mPointsOfAttention;
            nameField[2].onEndEdit.AddListener(delegate { DataChanged("medicine", i); });
        }

        //create deliverytools Menu button
        instance = GameObject.Instantiate(mGenericButtonPrefab.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
        instance.transform.SetParent(mParentOfMenuItems.transform, false);
        instance.GetComponentsInChildren<Text>()[0].text = " Delivery Tools";
        instance.GetComponent<Button>().onClick.AddListener(AddTool);
        //create deliverytool panels
        for (int i = 0; medi != null && medi.mTools != null && i < medi.mTools.Count; i++)
        {
            instance = GameObject.Instantiate(mDeliveryToolsUIPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            instance.transform.SetParent(mParentOfMenuItems.transform, false);
            //fill txt field
            InputField[] nameField = instance.GetComponentsInChildren<InputField>();
            nameField[0].text = medi.mTools[i].mName;
            nameField[0].onEndEdit.AddListener(delegate { DataChanged("deliverytool",i);});
        }

        //create deliverymethods Menu button
        instance = GameObject.Instantiate(mGenericButtonPrefab.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
        instance.transform.SetParent(mParentOfMenuItems.transform, false);
        instance.GetComponentsInChildren<Text>()[0].text = " Delivery Methods";
        instance.GetComponent<Button>().onClick.AddListener(AddMethod);
        //create deliverymethod panels
        for (int i = 0; medi != null && medi.mTools != null && i < medi.mMethods.Count; i++)
        {
            instance = GameObject.Instantiate(mDeliveryMethodUIPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            instance.transform.SetParent(mParentOfMenuItems.transform, false);
         
            //fill txt field
            InputField[] nameField = instance.GetComponentsInChildren<InputField>();
            nameField[0].text = medi.mMethods[i].mName;
            nameField[0].onEndEdit.AddListener(delegate { DataChanged("deliverymethod", i); });
            nameField[1].text = "";
            for (int j = 0; medi.mMethods != null && medi.mMethods[i].mTools != null &&  j < medi.mMethods[i].mTools.Count; j++)
            {
                nameField[1].text += medi.mMethods[i].mTools[j].ToString() + "#";
            }      
            if (nameField.Length >= 1 && nameField[1].text.Length > 1)
            {
                nameField[1].text = nameField[1].text.Substring(0, nameField[1].text.Length - 1);
            }   
            nameField[1].onEndEdit.AddListener(delegate { DataChanged("deliverymethod", i); });
        }

       
        //create drawer Menu button
        instance = GameObject.Instantiate(mGenericButtonPrefab.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
        instance.transform.SetParent(mParentOfMenuItems.transform, false);
        instance.GetComponentsInChildren<Text>()[0].text = " Drawers";
        instance.GetComponent<Button>().onClick.AddListener(AddDrawer);
        //drawer panels
        for (int j = 0; medi != null && medi.mDrawers != null && j < medi.mDrawers.Count; j++)
        {
            GameObject drawerInstance;
            drawerInstance = GameObject.Instantiate(mDrawerUIPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            drawerInstance.transform.SetParent(mParentOfMenuItems.transform, false);

            //fill txt field
            InputField[] nameFields = drawerInstance.GetComponentsInChildren<InputField>();
            nameFields[0].text = "";//for the medicines in the drawer
            nameFields[0].onEndEdit.AddListener(delegate { DataChanged("drawer", j); });
            for (int k = 0; k < medi.mDrawers[j].mMedicines.Count; k++)
            {
                nameFields[0].text += medi.mDrawers[j].mMedicines[k].ToString();
                nameFields[0].text += "#";
            }
            if (nameFields[0].text.Length > 1)
            {
                nameFields[0].text = nameFields[0].text.Substring(0, nameFields[0].text.Length - 1);
            }
            nameFields[1].text = "";//for the tools in the drawer
            nameFields[1].onEndEdit.AddListener(delegate { DataChanged("drawer", j); });
            for (int k = 0; k < medi.mDrawers[j].mDeliveryTools.Count; k++)
            {
                nameFields[1].text += medi.mDrawers[j].mDeliveryTools[k].ToString();
                nameFields[1].text += "#";
            }
            if (nameFields[1].text.Length > 1)
            {
                nameFields[1].text = nameFields[1].text.Substring(0, nameFields[1].text.Length - 1);
            }
            //set is locked
            //fill txt field
            Toggle[] toggleField = drawerInstance.GetComponentsInChildren<Toggle>();
            toggleField[0].isOn = medi.mDrawers[j].mIsLocked;//for the medicines in the drawer
            toggleField[0].onValueChanged.AddListener(delegate { DataChanged("drawer", j); });
        }

        //create Cabinets Menu button
        instance = GameObject.Instantiate(mGenericButtonPrefab.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
        instance.transform.SetParent(mParentOfMenuItems.transform, false);
        instance.GetComponentsInChildren<Text>()[0].text = " Cabinet";
        instance.GetComponent<Button>().onClick.AddListener(AddCabinet);
        //create cabinet panels
        for (int i = 0; medi != null && medi.mCabinets != null && i < medi.mCabinets.Count; i++)
        {
            instance = GameObject.Instantiate(mCabinetUIPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            instance.transform.SetParent(mParentOfMenuItems.transform, false);
            //fill txt field
           
            InputField[] nameFields = instance.GetComponentsInChildren<InputField>();
            nameFields[0].text = "";
            nameFields[0].onEndEdit.AddListener(delegate { DataChanged("cabinet", i); });
            for (int k = 0; k < medi.mCabinets[i].mDrawers.Count; k++)
            {
                nameFields[0].text += medi.mCabinets[i].mDrawers[k].ToString();
                nameFields[0].text += "#";
            }
            if (nameFields[0].text.Length > 1)
            {
                nameFields[0].text = nameFields[0].text.Substring(0, nameFields[0].text.Length - 1);
            }
               
        }
        //create scenario Menu button
        instance = GameObject.Instantiate(mGenericButtonPrefab.gameObject, new Vector3(0, 0, 0), Quaternion.identity);
        instance.transform.SetParent(mParentOfMenuItems.transform, false);
        instance.GetComponentsInChildren<Text>()[0].text = " Scenarios";
        instance.GetComponent<Button>().onClick.AddListener(AddScenario);

        for (int i = 0; medi != null && medi.mScenarios != null && i < medi.mScenarios.Count; i++)
        {
            instance = GameObject.Instantiate(mScenarioUIPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            instance.transform.SetParent(mParentOfMenuItems.transform, false);

            //fill txt field
            InputField[] nameField = instance.GetComponentsInChildren<InputField>();
            nameField[0].text = medi.mScenarios[i].mName;
            nameField[0].onEndEdit.AddListener(delegate { DataChanged("scenario", i); });
            nameField[1].text = medi.mScenarios[i].mPatientID.ToString();
            nameField[1].onEndEdit.AddListener(delegate { DataChanged("scenario", i); });
            nameField[2].text = medi.mScenarios[i].mCabinetID.ToString();
            nameField[2].onEndEdit.AddListener(delegate { DataChanged("scenario", i); });
            nameField[3].text = medi.mScenarios[i].mMedicineID.ToString();
            nameField[3].onEndEdit.AddListener(delegate { DataChanged("scenario", i); });
            nameField[4].text = medi.mScenarios[i].mDeliveryMethod.ToString();
            nameField[4].onEndEdit.AddListener(delegate { DataChanged("scenario", i); });

        }
    }

    public void AddPatient()
    {
        medi.mPatients.Add(new Patient());
        RefreshMediDataUI();
    }

    public void AddMedicine()
    {
        medi.mMedicines.Add(new Medicine());
        RefreshMediDataUI();
    }

    public void AddTool()
    {
        medi.mTools.Add(new DeliveryTool());
        RefreshMediDataUI();
    }
    public void AddMethod()
    {
        medi.mMethods.Add(new DeliveryMethod());
        RefreshMediDataUI();
    }
    public void AddCabinet()
    {
        medi.mCabinets.Add(new Cabinet());
        RefreshMediDataUI();
    }
    public void AddDrawer()
    {
        medi.mDrawers.Add(new CabinetDrawer());
        RefreshMediDataUI();     
    }
    public void AddScenario()
    {
        medi.mScenarios.Add(new Scenario());
        RefreshMediDataUI();
    }

    public void DataChanged(string type, int index)
    {
       switch(type)
        {
            case "patient":
                {           
                    GameObject[] patientPanels = GameObject.FindGameObjectsWithTag("Patient");
                    for (int i = 0; medi != null && medi.mPatients != null && i < medi.mPatients.Count; i++)
                    {
                        
                        GameObject instance = patientPanels[i];
                        //get name field
                        InputField[] nameField = instance.GetComponentsInChildren<InputField>();
                        medi.mPatients[i].mName = nameField[0].text ;
                        medi.mPatients[i].mAge = Int32.Parse(nameField[1].text);
                        medi.mPatients[i].mAge = Int32.Parse(nameField[2].text);
                        Dropdown[] dropdowns;
                        //get 2 dropdowns for sex and type
                        dropdowns = instance.GetComponentsInChildren<Dropdown>();
                        medi.mPatients[i].mID = i;
                        medi.mPatients[i].mType = (PatientType)dropdowns[0].value;
                        medi.mPatients[i].mSex = (Sex)dropdowns[1].value;                     
                    }
                    break;
                }
            case "medicine":
                {
                    GameObject[] medicinePanels = GameObject.FindGameObjectsWithTag("Medicine");
                    for (int i = 0; medi != null && medi.mMedicines != null && i < medi.mMedicines.Count; i++)
                    {
                        Dropdown[] dropdowns;
                        //fill 2 dropdowns for quantity and unit
                        dropdowns = medicinePanels[i].GetComponentsInChildren<Dropdown>();
                        medi.mMedicines[i].mID = i;
                        medi.mMedicines[i].mUnit = (Unit)dropdowns[0].value ;
                        medi.mMedicines[i].mPackage = (Package)dropdowns[1].value ;
                        //fill txt fields
                        InputField[] nameField = medicinePanels[i].GetComponentsInChildren<InputField>();
                        medi.mMedicines[i].mName = nameField[0].text;
                        medi.mMedicines[i].mQuantity = Int32.Parse(nameField[1].text);
                       medi.mMedicines[i].mPointsOfAttention = nameField[2].text;
                    }
                    break;
                }
            case "deliverytool":
                {
                    GameObject[] deliveryToolPanels = GameObject.FindGameObjectsWithTag("DeliveryTool");
                    for (int i = 0; medi != null && medi.mTools != null && i < medi.mTools.Count; i++)
                    {
                        InputField[] nameField = deliveryToolPanels[i].GetComponentsInChildren<InputField>();
                        medi.mTools[i].mID = i;
                        medi.mTools[i].mName = nameField[0].text;
                    }
                    break;
                }
            case "deliverymethod":
                {
                    GameObject[] deliveryMethodPanels = GameObject.FindGameObjectsWithTag("DeliveryMethod");
                    for (int i = 0; medi != null && medi.mTools != null && i < medi.mMethods.Count; i++)
                    {
                        //fill txt field
                        InputField[] nameField = deliveryMethodPanels[i].GetComponentsInChildren<InputField>();
                        medi.mMethods[i].mID = i;
                        medi.mMethods[i].mName = nameField[0].text;
                        string[] toolIDs = nameField[1].text.Split('#');
                        for (int j = 0; j < toolIDs.Length; j++)
                        {
                            medi.mMethods[i].mTools.Add(Int32.Parse(toolIDs[j]));
                        }
                    }
                    break;
                }
            case "drawer":
                {
                    GameObject[] drawerPanels = GameObject.FindGameObjectsWithTag("CabinetDrawer");
                    for (int j = 0; medi.mDrawers != null &&  j < medi.mDrawers.Count; j++)
                    {
                        InputField[] nameFields = drawerPanels[j].GetComponentsInChildren<InputField>();
                        medi.mDrawers[j].mID = j;
                        medi.mDrawers[j].mMedicines.Clear();
                        string[] medIDs = nameFields[0].text.Split('#');
                        for (int k = 0; k < medIDs.Length; k++)
                        {
                            medi.mDrawers[j].mMedicines.Add(Int32.Parse(medIDs[k]));
                        }

                        string[] toolIDs = nameFields[1].text.Split('#');
                        medi.mDrawers[j].mDeliveryTools.Clear();
                        for (int k = 0; k < toolIDs.Length; k++)
                        {
                            medi.mDrawers[j].mDeliveryTools.Add(Int32.Parse(toolIDs[k]));
                        }
                        Toggle[] toggleField = drawerPanels[j].GetComponentsInChildren<Toggle>();
                        medi.mDrawers[j].mIsLocked = toggleField[0].isOn;
                        }
                    break;
                }

            case "cabinet":
                {
                    GameObject[] cabinetPanels = GameObject.FindGameObjectsWithTag("Cabinet");
                    //create cabinet panels
                    for (int i = 0; medi != null && medi.mCabinets != null && i < medi.mCabinets.Count; i++)
                    {
                        InputField[] nameField = cabinetPanels[i].GetComponentsInChildren<InputField>();
                        medi.mCabinets[i].mID = i;
                        string[] drawerIDs = nameField[0].text.Split('#');
                        medi.mCabinets[i].mDrawers.Clear();
                        for (int k = 0; k < drawerIDs.Length; k++)
                        {
                            medi.mCabinets[i].mDrawers.Add(Int32.Parse(drawerIDs[k]));
                        }       
                    }
                 break;
                }
            case "scenario":
                {
                    GameObject[] scenarioPanels = GameObject.FindGameObjectsWithTag("Scenario");

                    for (int i = 0; medi != null && medi.mScenarios != null && i < medi.mScenarios.Count; i++)
                    {
                       
                        //fill txt field
                        InputField[] nameField = scenarioPanels[i].GetComponentsInChildren<InputField>();
                        medi.mScenarios[i].mName = nameField[0].text;
                        medi.mScenarios[i].mPatientID = Int32.Parse(nameField[1].text);
                        medi.mScenarios[i].mCabinetID = Int32.Parse(nameField[2].text);
                        medi.mScenarios[i].mMedicineID = Int32.Parse(nameField[3].text);
                        medi.mScenarios[i].mDeliveryMethod = Int32.Parse(nameField[4].text);
                    }
                    break;

                }
        }
    }
}
