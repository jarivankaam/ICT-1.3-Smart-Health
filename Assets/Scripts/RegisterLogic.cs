using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RegisterLogic : MonoBehaviour
{
    public TMP_Text EmailException;
    public TMP_InputField EmailInput;
    public TMP_Text PasswordException;
    public TMP_InputField PasswordInput;
    public Color ValidColor;
    public Color InvalidColor;

    private string _email;
    private bool _validEmail;
    private string _password;
    private bool _validPassword;
    public async void Login(bool newUser)
    {
        if (!_validEmail || !_validPassword)
        {
            ShowMessage(EmailException, "Ongeldig e-mailadres of wachtwoord", InvalidColor);
            return;
        }

        if (newUser)
        {
            await APIClient.Instance.Register(_email, _password);
        }
        else
        {
            await APIClient.Instance.Login(_email, _password);
        }

        if(string.IsNullOrEmpty(APIClient.Instance.GetAccessToken()))
        {
            ShowMessage(EmailException, "Er is iets misgegaan bij het inloggen", InvalidColor);
            return;
        }

        ShowMessage(EmailException, "Succesvolle login", ValidColor);
        SceneManager.LoadScene("BigTimeLine");
    }

    // Form VALIDATIONS
    public void ValidateEmail()
    {
        _email = EmailInput.text.Trim();
        _validEmail = false;

        // Email validation
        if (string.IsNullOrEmpty(_email))
        {
            ShowMessage(EmailException, "E-mailadres is vereist", InvalidColor);
            return;
        }

        if (!Regex.IsMatch(_email, @"^[\w\.-]+@[\w-]+\.[a-zA-Z]{2,}$"))
        {
            ShowMessage(EmailException, "Voer een geldig e-mailadres in", InvalidColor);
            return;
        }

        ShowMessage(EmailException, "Emailadres is geldig", ValidColor);
        _validEmail = true;
    }

    public void ValidatePassword()
    {
        _password = PasswordInput.text.Trim();
        _validPassword = false;

        // Password validations
        var validations = new (Func<string, bool> condition, string errorMessage)[]
        {
            (string.IsNullOrEmpty, "Wachtwoord is vereist."),
            (password => password.Length < 10, "Wachtwoord moet minimaal 10 tekens lang zijn."),
            (password => !Regex.IsMatch(password, @"[A-Z]"), "Wachtwoord moet minstens één hoofdletter bevatten."),
            (password => !Regex.IsMatch(password, @"[a-z]"), "Wachtwoord moet minstens één kleine letter bevatten."),
            (password => !Regex.IsMatch(password, @"\d"), "Wachtwoord moet minstens één cijfer bevatten."),
            (password => !Regex.IsMatch(password, @"[\W_]"), "Wachtwoord moet minstens één speciaal teken bevatten.")
        };


        foreach (var (condition, errorMessage) in validations)
        {
            if (condition(_password))
            {
                ShowMessage(PasswordException, errorMessage, InvalidColor);
                return;
            }
        }

        // When everything is valid
        ShowMessage(PasswordException, "Wachtwoord is geldig", ValidColor);
        _validPassword = true;
    }

    private void ShowMessage(TMP_Text label, string message, Color color)
    {
        label.text = message;
        label.color = color;
    }
}
