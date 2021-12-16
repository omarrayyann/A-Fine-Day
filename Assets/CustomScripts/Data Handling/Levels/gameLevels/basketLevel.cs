using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Holds all the parameters needed to set up a basketMinigame level
public class basketLevel: Level
{
    public int numberOfTargets;
    public double difficultyPercentage;
    public double accuracyToPass;   

    public basketLevel(int numberOfTargets, double difficultyPercentage, double accuracyToPass) {
        type = "BasketMinigame";
        this.numberOfTargets = numberOfTargets;
        this.difficultyPercentage = difficultyPercentage;
        this.accuracyToPass = accuracyToPass;
    }
}


