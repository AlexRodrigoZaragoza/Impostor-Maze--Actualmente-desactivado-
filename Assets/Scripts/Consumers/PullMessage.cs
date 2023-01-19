using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Consumer;
using System.Globalization;

public class PullMessage : MonoBehaviour, IPullMessage
{

    public List<Consumer.Message> pullMessages = new List<Consumer.Message>();
    public List<Consumer.Message> pushMessages = new List<Consumer.Message>();
    public GameObject singGO;
    public GameObject _messagesParent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Consumer.Message newMessage in pushMessages)
        {
            Debug.Log("MENSAJE -->" + newMessage._msg);
        }
    }

    public void addToList(Consumer.Message newMessage)
    {
        pullMessages.Add(newMessage);
    }

    public void updateMessages()
    {
        foreach(Consumer.Message newMessage in pullMessages)
        {
            Vector4 currentPosition = splitPositio(newMessage._position);
            GameObject newSing = Instantiate(singGO,new Vector3(currentPosition.x, currentPosition.y, currentPosition.z), Quaternion.Euler(0.0f,currentPosition.w,0.0f));
            newSing.transform.SetParent(_messagesParent.transform);
            Message messageScript = newSing.GetComponent<Message>();
            messageScript.id = newMessage._id;
            messageScript.message = newMessage._msg;
        }
    }

    public void updatePushMessages(Consumer.Message newMessage)
    {//SE GUARDAN DATOS HACIA LA BASE DE DATOS
        pushMessages.Add(newMessage);
    }

    private Vector4 splitPositio(string stringV3)
    {
        Vector4 newVector;
        string[] coordenadas = stringV3.Split(',');
        newVector = new Vector4(float.Parse(coordenadas[0], CultureInfo.InvariantCulture), 
        float.Parse(coordenadas[1], CultureInfo.InvariantCulture), float.Parse(coordenadas[2], CultureInfo.InvariantCulture), float.Parse(coordenadas[3], CultureInfo.InvariantCulture));
        return newVector;
    }

    public void clearPullList()
    {
        for (int i = 0; i < _messagesParent.transform.childCount; i++)
        {
            Destroy(_messagesParent.transform.GetChild(i).gameObject);
        }
        pullMessages.Clear();
    }
    public void clearPushList()
    {
        pushMessages.Clear();
    }

    public List<Consumer.Message> getPushList()
    {
        return pushMessages;
    }

}
