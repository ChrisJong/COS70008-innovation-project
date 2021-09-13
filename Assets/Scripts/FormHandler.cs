using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FormHandler : MonoBehaviour
{
    public InputField FirstNameInputField;
    public InputField LastNameInputField;

    public void setName()
    {
        PlayerPrefs.SetString("first_name", FirstNameInputField.text);
        PlayerPrefs.SetString("last_name", LastNameInputField.text);
        SceneManager.LoadScene("Main");
    }
}
