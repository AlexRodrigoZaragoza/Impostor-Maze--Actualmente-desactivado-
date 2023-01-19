using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMessage
{
    public void SetInfo(int id, string message, int user,string position);
    public void GetInfo(int index);
}
