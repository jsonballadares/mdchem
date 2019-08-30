using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using System.Net.Mail;
using System;
using System.Threading.Tasks;

public class FormManager : MonoBehaviour
{
    public TMPro.TMP_InputField loginEmailInput;
    public TMPro.TMP_InputField loginPasswordInput;
    public TMPro.TMP_InputField emailInput;
    public TMPro.TMP_InputField passwordInput;
    public TMPro.TMP_InputField securityQuestionInput;
    public TMPro.TMP_InputField securityQuestionAnswerInput;
    public TMPro.TMP_InputField classNumberInput;
    public Button signUpButton;
    public Button loginButton;
    public AuthManager authManager;
    public GameObject statusText;
    public GameObject loginScreen;
    string message = "Enter your data";
    Boolean clearBoxes = false;
    Boolean disableScreen = false;
    Boolean flag = false;

    string uuid = "", email = "";

    void Awake()
    {

        ToggleButtonStates(false);
        //HandleAuthCallback will be subscribing to authCallBack event
        authManager.authCallBack += HandleAuthCallback;

    }

    void Start()
    {
        
    }

    void Update()
    {
        ToggleButtonStates(true);
        statusText.GetComponent<TMPro.TMP_Text>().text = message;
        if (clearBoxes)
        {
            passwordInput.text = "";
            emailInput.text = "";
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
    public void OnSignUp()
    {
        Debug.Log("Sign Up");
        signUpStudent();
        //authManager.SignUpNewUser(emailInput.text, passwordInput.text);

    }

    public void OnLogin()
    {
        Debug.Log("Login");
        loginStudent();
        //authManager.LogInNewUser(emailInput.text, passwordInput.text);
    }

    public void OnGuestLogin()
    {
        Debug.Log("Login Guest");
        authManager.LogInNewUser("guest@guest.com", "Guestpassword");
    }

    void HandleAuthCallback(Task<Firebase.Auth.FirebaseUser> task, string operation)
    {
        if (operation.Equals("sign_up"))
        {
            Debug.Log("Signing UP");

        }
        else if (operation.Equals("login"))
        {

        }

    }

    void OnDestroy()
    {
        Debug.Log("OnDestroy was called");
        authManager.authCallBack -= HandleAuthCallback;
    }

    public void setPlayerPrefs()
    {
        flag = true;
    }

    public void ValidatePassword()
    {
        string password = passwordInput.text;
        if (password != null && emailInput.text != null && password.Length >= 6)
        {
           // ToggleButtonStates(true);
        }
        else
        {
            //ToggleButtonStates(false);
        }
    }
    public void ValidateEmail()
    {
        string email = emailInput.text;
        Debug.Log("The email is " + email);
        var regexPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

        if (email != "" && Regex.IsMatch(email, regexPattern) && passwordInput.text.Length >= 6)
        {
           // ToggleButtonStates(true);
        }
        else
        {
           // ToggleButtonStates(false);
        }
    }
    private void ToggleButtonStates(bool toState)
    {
        signUpButton.interactable = toState;
        loginButton.interactable = toState;
    }
    public void emptyInputBox()
    {
        clearBoxes = true;
    }
    private void UpdateStatus(string message)
    {
        this.message = message;
    }

    public void disableLoginScreen()
    {
        disableScreen = true;
    }

    public void signUpStudent()
    {
        // Debug.Log("email -> " + emailInput.text);
        // Debug.Log("password -> " + passwordInput.text);
        // Debug.Log("security question -> " + securityQuestionInput.text);
        // Debug.Log("security question answer -> " + securityQuestionAnswerInput.text);
        // Debug.Log("class number -> " + classNumberInput.text);
        Student student = new Student(emailInput.text, passwordInput.text, securityQuestionInput.text, securityQuestionAnswerInput.text, Int32.Parse(classNumberInput.text));
        Debug.Log(student.toJSON());
        StartCoroutine(WebRequestManager.register(Enviorment.URL + "/api/auth/register/student", student.toJSON()));
    }

    public void loginStudent()
    {
        //Student student = new Student(emailInput.text, passwordInput.text);
        Student student = new Student("jackson@gmail.com", "Jason1337");
        Debug.Log(student.toJSON());
        StartCoroutine(WebRequestManager.login(Enviorment.URL + "/api/auth/login/student", student.toJSON()));
    }
}
