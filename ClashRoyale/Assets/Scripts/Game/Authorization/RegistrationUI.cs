using System;
using UnityEngine;
using UnityEngine.UI;

public class RegistrationUI : MonoBehaviour
{
    [SerializeField] private Registration registration;
    [SerializeField] private InputField login;
    [SerializeField] private InputField password;
    [SerializeField] private InputField confirmPassword;
    [SerializeField] private Button apply;
    [SerializeField] private Button signIn;
    [SerializeField] private GameObject authorizationCanvas;
    [SerializeField] private GameObject registrationCanvas;


    private void Awake()
    {
        login.onEndEdit.AddListener(registration.SetLogin);
        password.onEndEdit.AddListener(registration.SetPassword);
        password.onEndEdit.AddListener(registration.SetConfirmPassword);

        apply.onClick.AddListener(ApplyClick);
        signIn.onClick.AddListener(SignInClick);
        registration.Error += SignUpError;
        registration.Success += SignUpSuccess;
    }

    private void SignInClick()
    {
        authorizationCanvas.SetActive(true);
        registrationCanvas.SetActive(false);
    }

    private void SignUpSuccess()
    {
        signIn.gameObject.SetActive(true);
    }

    private void ApplyClick()
    {
        signIn.gameObject.SetActive(false);
        apply.gameObject.SetActive(false);
        registration.SignUp();
    }

    private void SignUpError()
    {
        signIn.gameObject.SetActive(true);
        apply.gameObject.SetActive(true);
    }
}
