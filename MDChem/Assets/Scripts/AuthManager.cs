using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using System.Threading.Tasks;

public class AuthManager : MonoBehaviour
{

    //Firebase API variables
    public Firebase.Auth.FirebaseAuth auth;

    //Delegates
    public delegate void AuthCallBack(Task<Firebase.Auth.FirebaseUser> task, string operation);

    //Events
    public event AuthCallBack authCallBack;


    void Awake()
    {
        //initialise the firebase auth object to an actual instance
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    }


    /*
    Signs up a new user
     */
    public void SignUpNewUser(string email, string password)
    {
        Debug.Log("BeginningSignUpNewUser()");
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            Debug.Log("Calling authCallBack()");
            authCallBack(task, "sign_up");
            Debug.Log("After authCallBack()");
        });
        Debug.Log("EndingSignUpNewUser()");
    }

    /*
    Logs in an exisiting user
     */
    public void LogInNewUser(string email, string password)
    {
        Debug.Log("BeginningSignUpNewUser()");
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            Debug.Log("Calling authCallBack()");
            authCallBack(task, "login");
            Debug.Log("After authCallBack()");
        });
        Debug.Log("EndingSignUpNewUser()");
    }
}
