using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Mail;
using System;

public class FormManager : MonoBehaviour
{
    public TMPro.TMP_Text emailInput;
    public TMPro.TMP_Text passwordInput;
    public Button signUpButton;
    public Button loginButton;

    void Awake()
    {
        ToggleButtonStates(false);
    }

    public void ValidatePassword()
    {
        string password = passwordInput.text;
        if (password != null && emailInput.text != null && password.Length >= 6)
        {
            ToggleButtonStates(true);
        }
        else
        {
            ToggleButtonStates(false);
        }
    }
    public void ValidateEmail()
    {
        string email = emailInput.text;
        if (email != null && IsValid(email) && passwordInput.text.Length >= 6)
        {
            Debug.Log("The toggle buttons are true");
            ToggleButtonStates(true);
        }
        else
        {
            Debug.Log("The toggle buttons are false");
            ToggleButtonStates(false);
        }
    }
    private void ToggleButtonStates(bool toState)
    {
        signUpButton.interactable = toState;
        loginButton.interactable = toState;
    }

    public bool IsValid(string emailaddress)
    {
        try
        {
            MailAddress m = new MailAddress(emailaddress);

            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
