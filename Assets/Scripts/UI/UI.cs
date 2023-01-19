using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BBDD;
using UnityEngine.UI;

namespace UI{
    public class UI : MonoBehaviour
    {
        [SerializeField] private InputField usernameInput;
        [SerializeField] private InputField passwordInput;

        [SerializeField] private Button LoginButton;
        [SerializeField] private Button RegisterUserButton;

        [SerializeField] private Text errorText;

        [Header("Menus")]
        public GameObject mainMenu;
        public GameObject playMenu;

        [Header("Play menu")]
        public Button play;
        public Text user;
        public Text globalPoints;

        string menuAux = "";
        bool openMenu = false;

        void Start()
        {
            LoginButton.onClick.AddListener(() =>{
                StartCoroutine(ServiceLocator.Instance.GetService<IRequestInfo>().Login(usernameInput.text, passwordInput.text));
            });  
             RegisterUserButton.onClick.AddListener(() =>{
                StartCoroutine(ServiceLocator.Instance.GetService<IRequestInfo>().RegisterUser(usernameInput.text, passwordInput.text));
            });
            play.onClick.AddListener(() =>
            {
                StartCoroutine(ServiceLocator.Instance.GetService<IRequestInfo>().GetMaze());
            });
        }

        private void Update()
        {
            errorText.text = ServiceLocator.Instance.GetService<ErrorMessages>()._errorText;
            if (openMenu)
            {
                switch (menuAux)
                {
                    case "Login":
                        openMenu = false;
                        break;
                    case "Register":
                        openMenu = false;
                        break;

                    case "Play":
                        playMenu.SetActive(true);
                        openMenu = false;
                        user.text = "Welcome, "+ServiceLocator.Instance.GetService<IUserInfo>().GetUser();
                        globalPoints.text = "Global points: "+ ServiceLocator.Instance.GetService<IUserInfo>().GetGlobalPoints();
                        StartCoroutine(ServiceLocator.Instance.GetService<IRequestInfo>().GetLeaderboard(playMenu));
                        break;

                    case "InGame":
                        openMenu = false;
                        playMenu.SetActive(false);
                        mainMenu.SetActive(false);
                        break;
                }
            }
        }

        public void OpenSpecificMenu(string menu)
        {
            //No se cambia la variable cuando sale de aqui
            ServiceLocator.Instance.GetService<IUserInfo>().GetUserInfo();
            menuAux = menu;
            openMenu = true;
        }
    }
}
