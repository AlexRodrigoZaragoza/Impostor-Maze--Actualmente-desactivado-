using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILeaderboard
{
    public void SetInfo(string nombre, int globalPoints);
    public Dictionary<string, int> GetInfo();
}
