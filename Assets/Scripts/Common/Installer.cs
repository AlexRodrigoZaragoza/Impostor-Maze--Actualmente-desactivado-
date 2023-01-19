using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BBDD;
using UI;
using Consumer;
namespace Common{
    public class Installer : MonoBehaviour
    {
        public UI.UI _ui;
        public Leaderboard _leaderboard;
        public MazeRender _mazeRender;
        public PullMessage _pullMessage;
        public GameManager _gameManager;
        public CanvasReference _canvasReference;
        public bool _newAccountCreated = false;
        public bool _getMazeIniciated = false;
        private void Awake()
        {
            var databaseConnectionAdapter = new DatabaseConnectionsAdapter();
            ServiceLocator.Instance.RegisterService<IRequestInfo>(databaseConnectionAdapter);

            var userInfo = new UserInfo();
            ServiceLocator.Instance.RegisterService<IUserInfo>(userInfo);

            var mazeInfo = new Maze();
            ServiceLocator.Instance.RegisterService<IMazeInfo>(mazeInfo);

            var messagesInfo = new Consumer.Message();
            ServiceLocator.Instance.RegisterService<IMessage>(messagesInfo);

            var errorMessages = new ErrorMessages();
            ServiceLocator.Instance.RegisterService(errorMessages);

            ServiceLocator.Instance.RegisterService(_ui);

            ServiceLocator.Instance.RegisterService<ILeaderboard>(_leaderboard);

            ServiceLocator.Instance.RegisterService(this);

            ServiceLocator.Instance.RegisterService(_mazeRender);

            ServiceLocator.Instance.RegisterService<IPullMessage>(_pullMessage);

            ServiceLocator.Instance.RegisterService(_gameManager);

            ServiceLocator.Instance.RegisterService<ICanvasReference>(_canvasReference);
        }

        private void Start()
        {
            //No recuerdo para que era esto
            ServiceLocator.Instance.GetService<IUserInfo>().Init();

        }

        public void NewAccountCreated()
        {
            _newAccountCreated = true;
        }

        public void NewAccountCreatedFinished()
        {
            _newAccountCreated = false;
        }

        public void GetMazeCompleted()
        {
            _getMazeIniciated = false;
        }

        public void GetMazeIniciated()
        {
            _getMazeIniciated = true;
        }
    }
}