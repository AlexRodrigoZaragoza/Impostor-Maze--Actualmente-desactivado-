using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUserInfo 
{
    public void SetInfo(int id, string nick,string password, int current_points, int global_points);
    public void Init();
    public void GetUserInfo();
    public string GetUser();
    public int GetId();
    public string GetGlobalPoints();
    public int GetCurrentPoints();
    public void SetGlobalPoints(int value);


















}
