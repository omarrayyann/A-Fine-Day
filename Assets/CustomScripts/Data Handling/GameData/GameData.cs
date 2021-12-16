using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


// Game data acts as a record for holding a run's data
public class GameData : MonoBehaviour
{
    public bool win;
    private int runID;
    public int level;
    private HandMotionData motionData;
    private double score;
    private double timeSpent;
    
    // Contructor to create the save data for a level
    public GameData(int l, HandMotionData data, double s, double time, bool w)
    {
        win = w;
        level = l;
        motionData = data;
        score = s;
        timeSpent = time;
        runID = DataManager.gameData.Count;
    }

    // Getters:
    public HandMotionData getMotionData()
    {
        return motionData;
    }
    
    public double getScore()
    {
        return score;
    }
    public double getTimeSpent()
    {
        return timeSpent;
    }

    public int getRunID()
    {
        return runID;
    }
}
