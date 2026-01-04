using System;
using UnityEngine;
using UnityEngine.UI;

public class AuthorizationUI : MonoBehaviour
{
    [SerializeField] private Authorization authorization;
    [SerializeField] private InputField login;
    [SerializeField] private InputField password;
    [SerializeField] private Button signIn;
    [SerializeField] private Button signUp;
    [SerializeField] private GameObject authorizationCanvas;
    [SerializeField] private GameObject registrationCanvas;



    private void Awake()
    {
        login.onEndEdit.AddListener(authorization.SetLogin);
        password.onEndEdit.AddListener(authorization.SetPassword);

        signIn.onClick.AddListener(SignInClick);
        signUp.onClick.AddListener(SignUpClick);
        authorization.Error += SignInError;
    }

    private void SignUpClick()
    {
        registrationCanvas.SetActive(true);
        authorizationCanvas.SetActive(false);
    }

    private void SignInClick()
    {
        signIn.gameObject.SetActive(false);
        signUp.gameObject.SetActive(false);
        authorization.SignIn();
    }

    private void SignInError()
    {
        signIn.gameObject.SetActive(true);
        signUp.gameObject.SetActive(true);
    }
}
