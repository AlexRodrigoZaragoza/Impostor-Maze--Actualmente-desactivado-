using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;
using BBDD;

public class Menus : MonoBehaviour
{
    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject accountMenu, logInMenu,playMenu;

    [Header("Main Menu")]
    public Button logIn;
    public Button createAccount;

    [Header("Create Account Menu")]
    public Button confirmAccount;
    public Button returnToMenu1;
    public InputField createUsername, createPassword;
    public Text creationError;

    [Header("Log In Menu")]
    public Button access;
    public Button returnToMenu2;
    public InputField username, password;
    public Text logInError;

    [Header("Play menu")]
    public Button play;
    public Text user;
    public Text globalPoints;

    string usernameData, passwordData;

    void Start()
    {
        // Main Menu

        createAccount.onClick.AddListener(delegate { OpenMenu(accountMenu, true); });
        logIn.onClick.AddListener(delegate { OpenMenu(logInMenu, true); });


        // Create Account Menu

        returnToMenu1.onClick.AddListener(delegate { OpenMenu(accountMenu, false); });
        confirmAccount.onClick.AddListener(() => {
            StartCoroutine(ServiceLocator.Instance.GetService<IRequestInfo>().RegisterUser(createUsername.text, createPassword.text));
        });
        confirmAccount.onClick.AddListener(delegate { Credentials(); });
        // Log In Menu

        returnToMenu2.onClick.AddListener(delegate { OpenMenu(logInMenu, false); });
        access.onClick.AddListener(delegate { OpenMenu(playMenu, true); });
        access.onClick.AddListener(() => {
            StartCoroutine(ServiceLocator.Instance.GetService<IRequestInfo>().Login(username.text, password.text));
        });
        access.onClick.AddListener(delegate { Credentials(); });
        // Play Menu

    }

    private void Update()
    {

        if (ServiceLocator.Instance.GetService<Common.Installer>()._newAccountCreated)
        {
            StartCoroutine(ServiceLocator.Instance.GetService<IRequestInfo>().Login(createUsername.text, createPassword.text));
            ServiceLocator.Instance.GetService<Common.Installer>().NewAccountCreatedFinished();
        }

        //globalPoints.text = ServiceLocator.Instance.GetService<UserInfo>().GetGlobalPoints();
        //user.text = ServiceLocator.Instance.GetService<UserInfo>().GetUser();
    }

    void OpenMenu(GameObject menu, bool open)
    {
        if (ServiceLocator.Instance.GetService<ErrorMessages>()._errorText == "")
        {
            menu.SetActive(open);
        }
        else
        {
            print("Error de mensaje no abro siguiente menu");
        }
       
    }

    void Credentials()
    {
        if (ServiceLocator.Instance.GetService<ErrorMessages>()._errorText == "")
        {
            user.text = ServiceLocator.Instance.GetService<IUserInfo>().GetUser();
            globalPoints.text = ServiceLocator.Instance.GetService<IUserInfo>().GetGlobalPoints();
        }
        else
        {
            print("Error de mensaje no asigno credenciales");
        }
    }
}
