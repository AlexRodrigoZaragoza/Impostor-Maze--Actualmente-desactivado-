using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BBDD;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    int mazeId;
    bool isCompleted;
    public GameObject deadMenu;
    public Text deadMessageText;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator UpdateGame(bool isEndGame)
    {
        yield return StartCoroutine(setMazeId());
        yield return StartCoroutine(IsMazeFinised());
        if (isCompleted && isEndGame || isCompleted && !isEndGame)
        {
            //te lo has pasado pero alguien antes tb
            deadMenu.SetActive(true);
            string message = "No llegaste el primero...";
            float time = 6.0f;
            yield return StartCoroutine(DeadMessage(message, time));
            int current_GlobalPoints = int.Parse(ServiceLocator.Instance.GetService<IUserInfo>().GetGlobalPoints());
            int current_Points = ServiceLocator.Instance.GetService<IUserInfo>().GetCurrentPoints();
            string user_name = ServiceLocator.Instance.GetService<IUserInfo>().GetUser();
            current_GlobalPoints += 250;
            ServiceLocator.Instance.GetService<IUserInfo>().SetGlobalPoints(current_GlobalPoints);
            print(ServiceLocator.Instance.GetService<IUserInfo>().GetUser());
            yield return StartCoroutine(ServiceLocator.Instance.GetService<IRequestInfo>().UpdateUser(user_name, current_Points, current_GlobalPoints));
            deadMenu.SetActive(false);
            yield return StartCoroutine(ServiceLocator.Instance.GetService<IRequestInfo>().GetMaze());
            ServiceLocator.Instance.GetService<MazeRender>().StartNewMaze();
        }
        else if (!isCompleted && isEndGame)
        {
            //Te lo has pasado el primero
            int current_GlobalPoints = int.Parse(ServiceLocator.Instance.GetService<IUserInfo>().GetGlobalPoints());
            int current_Points = ServiceLocator.Instance.GetService<IUserInfo>().GetCurrentPoints();
            string user_name = "";
            user_name = ServiceLocator.Instance.GetService<IUserInfo>().GetUser();
            current_GlobalPoints += 500;
            ServiceLocator.Instance.GetService<IUserInfo>().SetGlobalPoints(current_GlobalPoints);
            yield return StartCoroutine(ServiceLocator.Instance.GetService<IRequestInfo>().UpdateUser(user_name, current_Points, current_GlobalPoints));
            yield return StartCoroutine(ServiceLocator.Instance.GetService<IRequestInfo>().UpdateMaze(mazeId));
            ServiceLocator.Instance.GetService<MazeRender>().setSeed(ServiceLocator.Instance.GetService<MazeRender>().GenerateNewSeed());
            yield return StartCoroutine(ServiceLocator.Instance.GetService<IRequestInfo>().CreateMaze(ServiceLocator.Instance.GetService<MazeRender>().getSeed()));
            yield return StartCoroutine(ServiceLocator.Instance.GetService<IRequestInfo>().GetMaze());
            ServiceLocator.Instance.GetService<MazeRender>().StartNewMaze();
            //Destroy(ServiceLocator.Instance.GetService<MazeRender>().getPlayerTransform().gameObject);
        }
        else if (!isCompleted && !isEndGame)
        {
            //CONTINUA
            //Vaciar lista se�ales
            ServiceLocator.Instance.GetService<IPullMessage>().clearPullList();
            //Bajar se�ales
            yield return StartCoroutine(ServiceLocator.Instance.GetService<IRequestInfo>().GetMessages(mazeId));
            //Subir se�ales
            foreach (Consumer.Message nextMessage in ServiceLocator.Instance.GetService<IPullMessage>().getPushList())
            {
                yield return StartCoroutine(ServiceLocator.Instance.GetService<IRequestInfo>().CreateMessages(nextMessage._msg, nextMessage._user, nextMessage._position, mazeId));
            }
            ServiceLocator.Instance.GetService<IPullMessage>().clearPushList();
            ServiceLocator.Instance.GetService<IPullMessage>().updateMessages();
        }
    }

    public IEnumerator playerDead()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Controller>().SetIsDead(true);
        yield return UpdateGame(false);
        string message = "";
        float time = 0.0f;
        deadMenu.SetActive(true);
 
        if (!isCompleted)
        {
            message = "Has muerto";
            time = 3.0f;
            yield return StartCoroutine(DeadMessage(message, time));
            Transform playerT = player.transform;
            playerT.position = ServiceLocator.Instance.GetService<MazeRender>().getStartPosition();
        }
        
        player.GetComponent<Controller>().SetIsDead(false);
        deadMenu.SetActive(false);

    }

    IEnumerator DeadMessage(string message, float seconds)
    {
        for (int i = 0; i < seconds; ++i)
        {
            deadMessageText.text = message + " " + (seconds - i);
            yield return new WaitForSeconds(1f);
        }

    }

    public IEnumerator IsMazeFinised()
    {
        yield return StartCoroutine(
            ServiceLocator.Instance.GetService<IRequestInfo>().AskIfMazeFinished(mazeId)
        );
        isCompleted = ServiceLocator.Instance.GetService<IMazeInfo>().getCompleted();
        yield return 0;
    }

    public IEnumerator setMazeId()
    {
        mazeId = ServiceLocator.Instance.GetService<IMazeInfo>().getId();
        yield return 0;
    }

    public void setIsCompleted(bool isCompleted)
    {
        this.isCompleted = isCompleted;
    }
}
