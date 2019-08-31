using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.Networking;
using System.Text.RegularExpressions;

public class FormManager : MonoBehaviour
{
    public TMPro.TMP_InputField loginEmailInput;
    public TMPro.TMP_InputField loginPasswordInput;
    public TMPro.TMP_InputField signUpEmailInput;
    public TMPro.TMP_InputField signUpPasswordInput;
    public TMPro.TMP_InputField signUpSecurityQuestionAnswerInput;
    public TMPro.TMP_InputField signUpClassNumberInput;
    public Toggle privacyPolicyToggle;
    public Button loginSignUpButton;
    public Button loginButton;
    public Button registerSignUpButton;
    public GameObject loginScreen;
    public GameObject signUpScreen;
    public Dropdown dropdown;
    Boolean clearBoxes = false;
    Boolean disableScreen = false;
    Boolean flag = false;
    string message = "Enter your data";
    string uuid = "", email = "";
    Boolean isLoginEmailValid;
    Boolean isLoginPasswordValid;
    Boolean isSignUpEmailValid;
    Boolean isSignUpPasswordValid;
    Boolean isSecurityQuestionAnswerValid;
    Boolean isClassNumberValid;
    Boolean isPrivacyPolicyValid;

    Boolean clearSignUpBoxes;


    void Start()
    {
        loginButton.interactable = false;
        registerSignUpButton.interactable = false;
        isLoginEmailValid = false;
        isLoginPasswordValid = false;
        isSignUpEmailValid = false;
        isSignUpPasswordValid = false;
        isSecurityQuestionAnswerValid = false;
        isClassNumberValid = false;
        isPrivacyPolicyValid = false;
    }

    void Update()
    {
        if (clearSignUpBoxes)
        {
            signUpEmailInput.text = "";
            signUpPasswordInput.text = "";
            signUpSecurityQuestionAnswerInput.text = "";
            signUpClassNumberInput.text = "";
            clearSignUpBoxes = false;
        }
        if (clearBoxes)
        {
            loginPasswordInput.text = "";
            loginEmailInput.text = "";
            clearBoxes = false;
        }
        if (disableScreen)
        {
            Debug.Log("should be disabled");
            message = "";
            loginScreen.SetActive(false);
            disableScreen = false;
        }

        if (!email.Equals("") && !uuid.Equals(""))
        {
            if (flag)
            {
                Debug.Log("Setting PlayerPrefs");
                PlayerPrefs.SetString("ui", uuid);
                PlayerPrefs.SetString("email", email);
                flag = false;
            }
        }
        // Debug.Log("IN PLAYER PREFS ui --> " + PlayerPrefs.GetString("ui"));
        // Debug.Log("IN PLAYER PREFS email --> " + PlayerPrefs.GetString("email"));
    }
    /*
    Public interface that calls the private signup method that signs up a user
     */
    public void OnSignUp()
    {
        Debug.Log("Sign Up");
        signUpStudent();
    }

    /*
    Public interface that calls the private login method that logins in an existing user
     */
    public void OnLogin()
    {
        Debug.Log("Login");
        loginStudent();

    }

    /*
    Method that is called when the guest login is called
     */
    public void OnGuestLogin()
    {
        //need to implement with earl
        //make a "guest user"
        //essentially a temp user figure out what player prefs and etc need to set up
        Debug.Log("Login Guest");
    }

    public void setPlayerPrefs()
    {
        flag = true;
    }

    /*
    validates the password by checking its length
    once a password is valid allows the user to select the button
     */
    public void ValidatePasswordLogin()
    {
        string password = loginPasswordInput.text;
        Debug.Log("The login password is " + loginPasswordInput);
        if (password != null && loginEmailInput.text != null && password.Length >= 6)
            isLoginPasswordValid = true;
        else
            isLoginPasswordValid = false;
    }
    public void ValidatePasswordSignUp()
    {
        string password = signUpPasswordInput.text;
        Debug.Log("The signup password is " + password);
        if (password != null && signUpEmailInput.text != null && password.Length >= 6)
            isSignUpPasswordValid = true;
        else
            isSignUpPasswordValid = false;
    }

    /*
    validates the email by using regex patterns to detect if it follows more or less
    the formatting for a valid email. once determined it allows the user to hit the 
    login button
     */
    public void ValidateEmailLogin()
    {
        string email = loginEmailInput.text;
        Debug.Log("The login email is " + loginEmailInput);
        var regexPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
        if (email != "" && Regex.IsMatch(email, regexPattern))
            isLoginEmailValid = true;
        else
            isLoginEmailValid = false;
    }
    public void ValidateEmailSignUp()
    {
        string email = signUpEmailInput.text;
        Debug.Log("The login email is " + email);
        var regexPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";
        if (email != "" && Regex.IsMatch(email, regexPattern))
            isSignUpEmailValid = true;
        else
            isSignUpEmailValid = false;
    }
    /*
    toggles the buttons on the login/signup screen
    as interactable dependent on a couple of bools
     */
    public void ToggleButtonStatesLogin()
    {
        if (isLoginEmailValid && isLoginPasswordValid)
            loginButton.interactable = true;
        else
            loginButton.interactable = false;
    }
    public void ToggleButtonStatesSignUp()
    {
        Debug.Log("isSignUpEmailValid " + isSignUpEmailValid);
        Debug.Log("isSignUpPasswordValid " + isSignUpPasswordValid);
        Debug.Log("isSecurityQuestionAnswserValid " + isSecurityQuestionAnswerValid);
        Debug.Log("isClassNumberValid " + isClassNumberValid);
        Debug.Log("isPrivacyPolicyValid " + isPrivacyPolicyValid);
        if (isSignUpEmailValid && isSignUpPasswordValid && isSecurityQuestionAnswerValid && isClassNumberValid && isPrivacyPolicyValid)
            registerSignUpButton.interactable = true;
        else
            registerSignUpButton.interactable = false;
    }

    public void ValidateSecurityQuestion()
    {
        if (signUpSecurityQuestionAnswerInput.text != null && signUpSecurityQuestionAnswerInput.text.Length > 0)
            isSecurityQuestionAnswerValid = true;
        else
            isSecurityQuestionAnswerValid = false;
    }

    public void ValidateClassNumber()
    {
        int numericValueOfString = 0;
        bool isNumber = int.TryParse(signUpClassNumberInput.text, out numericValueOfString);
        if (signUpClassNumberInput != null && isNumber && numericValueOfString > 0)
            isClassNumberValid = true;
        else
            isClassNumberValid = false;
    }

    public void toggleButtonValueChanged()
    {
        if (privacyPolicyToggle.isOn)
            isPrivacyPolicyValid = true;
        else
            isPrivacyPolicyValid = false;
    }
    public void emptyInputBoxLogin()
    {
        clearBoxes = true;
    }
    public void emptyInputBoxSignUp()
    {
        clearSignUpBoxes = true;
    }
    private void UpdateStatus(string message)
    {
        this.message = message;
    }


    public void disableLoginScreen()
    {
        disableScreen = true;
    }

    public void setPlayerEmail(String email)
    {
        Debug.Log("SETTING PLAYER PREF FOR " + email);
        if (!PlayerPrefs.HasKey("email"))
        {
            //in this case they are loggin in for the first time 
            PlayerPrefs.SetString("email", email);
        }
        else if (PlayerPrefs.HasKey("email") && !PlayerPrefs.GetString("email").Equals(email))
        {
            //in this case they have logged in before but theyre signing in with a different account
            PlayerPrefs.DeleteAll();
            UnityWebRequest.ClearCookieCache();
            PlayerPrefs.SetString("email", email);
        }
        else
        {
            //in the case that they arent logging in for the first time or with another account
            //we dont delete progress just because theyre just signing in again
            PlayerPrefs.SetString("email", email);
        }
    }

    /*
    Signs up a user in the database 
     */
    private void signUpStudent()
    {
        Student student = new Student(signUpEmailInput.text.ToLower(), signUpPasswordInput.text.ToLower(), dropdown.options[dropdown.value].text.ToLower(), signUpSecurityQuestionAnswerInput.text.ToLower(), Int32.Parse(signUpClassNumberInput.text));
        Debug.Log("JSON FOR SIGNUP ---> " + student.toJSON());

        StartCoroutine(WebRequestManager.register(Enviorment.URL + "/api/auth/register/student", student.toJSON(), (myReturnValue) =>
         {
             /* if myReturnValue is true 
                the user was signed up succesfully so get rid of the signup screen
                and notify the user they have signed up using the toast */
             if (myReturnValue)
             {
                 Debug.Log("SIGNUP WAS SUCCESFUL WE CAN CLOSE THE WINDOW!");
                 signUpScreen.SetActive(false);
                 loginEmailInput.text = signUpEmailInput.text.ToLower();
                 loginPasswordInput.text = signUpPasswordInput.text.ToLower();
                 emptyInputBoxSignUp();
                 //notify the user
             }
             /* if myReturnValue is false then leave the sign up screen and notify the user to try again (use toast pop up) */
             else
             {
                 Debug.Log("COULDNT SIGN UP TRY AGAIN WE CANT CLOSE THE WINDOW!");
                 signUpScreen.SetActive(true);
                 emptyInputBoxSignUp();
                 //notify the user
             }
         }));
    }

    /*
    Creates a student with the given login credentials and attempts to login 
     */
    private void loginStudent()
    {
        /* creates a student object with the given inputs from the gui */
        Student student = new Student(loginEmailInput.text.ToLower(), loginPasswordInput.text.ToLower());
        /* shows the json we are going to send */
        Debug.Log("JSON FOR LOGIN ---> " + student.toJSON());
        /* calls the method we will be using to send the da */
        StartCoroutine(WebRequestManager.login(Enviorment.URL + "/api/auth/login/student", student.toJSON(), (myReturnValue) =>
         {
             /* if myReturnValue is true then close the login screen and notify the user they have been logged in (use toast pop up) */
             if (myReturnValue)
             {
                 setPlayerEmail(loginEmailInput.text.ToLower());
                 //dont bring up the login screen
                 Debug.Log("LOGIN WAS SUCCESFUL WE CAN CLOSE THE LOGIN WINDOW!");
                 loginScreen.SetActive(false);
                 emptyInputBoxLogin();
                 //notify the user
             }
             /* if myReturnValue is false then leave the login screen up and notify the user to try again (use toast pop up) */
             else
             {
                 //bring up the login screen
                 Debug.Log("COULDNT LOGIN TRY AGAIN WE CANT CLOSE THE LOGIN WINDOW!");
                 loginScreen.SetActive(true);
                 emptyInputBoxLogin();
                 //notify the user
             }
         }));
    }
}
