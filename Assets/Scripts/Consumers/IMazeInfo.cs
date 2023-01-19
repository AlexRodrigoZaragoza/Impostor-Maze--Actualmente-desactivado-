using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMazeInfo
{
    public void SetInfo(int id, int seed);
    public void GetInfo();

    public int getSeed();
    public int getId();
    public bool getCompleted();
    public void setCompleted(bool isCompleted);
}
