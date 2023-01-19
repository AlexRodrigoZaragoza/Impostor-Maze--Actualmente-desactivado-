using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard_ScrollView : MonoBehaviour
{
    [SerializeField]
    private GameObject _scrollView;
    [SerializeField]
    private GameObject _userText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateLeaderboard()
    {
        Dictionary<string, int> leader = ServiceLocator.Instance.GetService<ILeaderboard>().GetInfo();
        int posY = 100;

        foreach(KeyValuePair<string, int> currentUser in leader)
        {
            GameObject newUser = Instantiate(_userText, _scrollView.transform);
            newUser.GetComponent<Text>().text = currentUser.Key + "       " + currentUser.Value;
            RectTransform rectTransform = newUser.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector3(-175, posY, 0);
            posY -= 100;
        }
    }

}
