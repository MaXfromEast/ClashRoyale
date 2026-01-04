using System;
using System.Collections.Generic;
using UnityEngine;

public class Registration : MonoBehaviour
{
    private const string LOGIN = "login";
    private const string PASSWORD = "password";
    private string login;
    private string password;
    private string confirmPassword;
    public event Action Error;
    public event Action Success;
    public void SetLogin(string login)
    {
        this.login = login;
    }

    public void SetPassword(string password)
    {
        this.password = password;
    }

    public void SetConfirmPassword(string password)
    {
        this.confirmPassword = password;
    }
    public void SignUp()
    {
        string uri = URLLibrary.MAIN + URLLibrary.REGISTRATION;
        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password)|| string.IsNullOrEmpty(confirmPassword))
        {
            ErrorMessage("Отсутствует Логин или Пароль");
            return;
        }

        if(password != confirmPassword)
        {
            ErrorMessage("Пароль не подтвержден");
            return;
        }

        Dictionary<string, string> data = new Dictionary<string, string>()
        {
            {LOGIN, login },
            {PASSWORD, password }
        };
        Loginer.Instance.Post(uri, data, SuccessMessage, ErrorMessage);
    }

    private void SuccessMessage(string data)
    {
        if (data != "ok")
        {
            ErrorMessage("Ответ сервера: " + data);
            return;
        }

        Debug.Log("Успешная регистрация");
        Success?.Invoke();
    }
    private void ErrorMessage(string error)
    {
        Debug.Log(error);
        Error?.Invoke();
    }

}
