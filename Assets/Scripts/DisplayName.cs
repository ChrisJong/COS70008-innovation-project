using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayName : MonoBehaviour
{
    public Text DisplayNameText;

    // Start is called before the first frame update
    void Start()
    {
        string firstName = PlayerPrefs.GetString("first_name", "Student");
        string lastName = PlayerPrefs.GetString("last_name", "");
        string finalText = firstName;
        if(lastName != "")
        {
            finalText += " " + lastName;
        }
        DisplayNameText.text = finalText;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
