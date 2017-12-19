using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardController : MonoBehaviour
{

    public Button[] keyItems;
    public Text inputField;

    string[] keys = new string[] {
        "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P",
        "A", "S", "D", "F", "G", "H", "J", "K", "L",
        "Z", "X", "C", "V", "B", "N", "M"
    };

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < keys.Length - 3; i++)
        {
            keyItems[i].transform.GetChild(0).GetComponent<Text>().text = keys[i];
        }
    }

    public void KeyPressed(int keyNbr)
    {
        if (keyNbr == 26)
        {
            // Enter
        }
        else if (keyNbr == 27)
        {
            inputField.text += " ";
        }
        else if (keyNbr == 28)
        {
            inputField.text = inputField.text.Remove(inputField.text.Length - 1, 1);
        }
        else
        {
            inputField.text += keys[keyNbr];
        }
    }
}
