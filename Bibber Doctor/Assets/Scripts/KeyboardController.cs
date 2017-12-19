using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardController : MonoBehaviour
{

    public Button[] keyObjects;
    public Text inputField;
    public Color pressedColor;
    int boardId = 0;

    string[] keys = new string[] {
        "q", "w", "e", "r", "t", "y", "u", "i", "o", "p",
        "a", "s", "d", "f", "g", "h", "j", "k", "l",
        "UP", "z", "x", "c", "v", "b", "n", "m"
    };

    string[] cappedKeys = new string[] {
        "Q", "W", "E", "R", "T", "Y", "U", "I", "O", "P",
        "A", "S", "D", "F", "G", "H", "J", "K", "L",
        "UP", "Z", "X", "C", "V", "B", "N", "M"
    };

    string[] altKeys = new string[] {
        "1","2","3","4","5","6","7","8","9","0",
        "@","#","£","_","&","-","+","(",")",
        "*","\"","'",":",";","/","!","?"
    };

    void Start()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            keyObjects[i].transform.GetChild(0).GetComponent<Text>().text = keys[i];
            keyObjects[i].GetComponent<Image>().color = Color.white;
        }
    }

    public void KeyPressed(int keyNbr)
    {
        if (keyNbr == 28)
        {
            inputField.text += " ";
        }
        else if (keyNbr == 29)
        {
            inputField.text = inputField.text.Remove(inputField.text.Length - 1, 1);
        }
        else
        {
            switch(boardId)
            {
                case 0:
                    inputField.text += keys[keyNbr];
                    return;
                case 1:
                    inputField.text += cappedKeys[keyNbr];
                    return;
                case 2:
                    inputField.text += altKeys[keyNbr];
                    return;
            }
        }
    }

    public void CapsKey()
    {
        switch(boardId)
        {
            case 0:         // set capped keys
                keyObjects[19].transform.GetChild(0).gameObject.SetActive(false);
                keyObjects[19].transform.GetChild(1).gameObject.SetActive(true);
                keyObjects[19].GetComponent<Image>().color = pressedColor;

                keyObjects[27].transform.GetChild(0).GetComponent<Text>().text = "123\n?!#";

                boardId = 1;
                ChangeKeys(cappedKeys);
                return;
            case 1:         // set normal keys
                keyObjects[19].transform.GetChild(0).gameObject.SetActive(false);
                keyObjects[19].transform.GetChild(1).gameObject.SetActive(true);
                keyObjects[19].GetComponent<Image>().color = Color.white;

                keyObjects[27].transform.GetChild(0).GetComponent<Text>().text = "123\n?!#";

                boardId = 0;
                ChangeKeys(keys);
                return;
            case 2:         // use alt version of cap key
                KeyPressed(19);
                return;
        }
    }

    public void ToggleAltKeys()
    {
        if (boardId == 2)
        {
            keyObjects[19].transform.GetChild(0).gameObject.SetActive(false);
            keyObjects[19].transform.GetChild(1).gameObject.SetActive(true);
            keyObjects[19].GetComponent<Image>().color = Color.white;

            keyObjects[27].transform.GetChild(0).GetComponent<Text>().text = "123\n?!#";

            boardId = 0;
            ChangeKeys(keys);
        }
        else
        {
            keyObjects[19].transform.GetChild(0).gameObject.SetActive(true);
            keyObjects[19].transform.GetChild(1).gameObject.SetActive(false);
            keyObjects[19].GetComponent<Image>().color = Color.white;

            keyObjects[27].transform.GetChild(0).GetComponent<Text>().text = "abc";

            boardId = 2;
            ChangeKeys(altKeys);
        }
    }

    void ChangeKeys(string[] keys)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            keyObjects[i].transform.GetChild(0).GetComponent<Text>().text = keys[i];
        }
    }
}
