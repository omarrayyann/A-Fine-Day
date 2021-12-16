using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds all the parameters needed to set up a WAMMinigame level
public class wackLevel: Level
{
    public double difficultyPercentage;
    public double allowedTime;
    public double targetsToPass;


    public wackLevel(double difficultyPercentage, double allowedTime, double targetsToPass){
        type = "WAMMinigame";
        this.difficultyPercentage = difficultyPercentage;
        this.allowedTime = allowedTime;
        this.targetsToPass = targetsToPass;
    }
}



