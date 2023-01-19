using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : IMazeInfo
{
    public int _id{get; private set;}
    public int _seed{get; private set;}
    public bool _completed;
    public void SetInfo(int id, int seed)
    {
        _id = id;
        _seed = seed;
    }

    public void GetInfo()
    {
        Debug.Log("Maze id: "+_id+" seed: "+_seed);
    }

    public int getSeed()
    {
        return _seed;
    }
    
    public int getId()
    {
        return _id;
    }

    public bool getCompleted()
    {
        return _completed;
    }

    public void setCompleted(bool isCompleted)
    {
        _completed = isCompleted;
    }

}
