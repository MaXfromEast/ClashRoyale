using System;
using System.Collections.Generic;
using UnityEngine;

public class Authorization : MonoBehaviour
{
    private const string LOGIN = "login";
    private const string PASSWORD = "password";
    private string login;
    private string password;
    public event Action Error;
    public void SetLogin(string login)
    {
        this.login = login;
    }

    public void SetPassword(string password)
    {
        this.password = password;
    }

    public void SignIn()
    {
        string uri = URLLibrary.MAIN + URLLibrary.AUTHORIZATION;
        if(string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
        {
            ErrorMessage("Отсутствует Логин или Пароль");
            return;
        }
        Dictionary<string, string> data = new Dictionary<string, string>()
        {
            {LOGIN, login },
            {PASSWORD, password }
        };
        Loginer.Instance.Post(uri, data, Success, ErrorMessage);
    }

    private void Success(string data)
    {
        string[] result = data.Split('|');
        if(result.Length<2 || result[0]!="ok")
        {
            ErrorMessage("Ответ сервера: " + data);
            return;
        }

        if(int.TryParse(result[1], out int id))
        {
            UserInfo.Instance.ID = id;
        }
        else
        {
            ErrorMessage($"Не удалось распарсить  \"{result[1]}\" в INT, полный ответ: {data}");
        }
    }
    private void ErrorMessage(string error)
    {
        Debug.Log(error);
        Error?.Invoke();
    }

}
