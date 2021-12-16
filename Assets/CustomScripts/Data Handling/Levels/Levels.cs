using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Script used to create levels. Currently only allows for the manual creation of levels.
// Can be used in the future to create a custom level creator specific for each user progress
public class Levels : MonoBehaviour
{

    public static int currentIndex = 2;
    public static Level[] levels = { 
        new wackLevel(0.3, -1, 8),
        new wackLevel(0.4, 10, 5),
        new basketLevel(5, 0.25, 0.5),
        new wackLevel(0.4, 20, 15),
        new basketLevel(10, 0.5, 0.6)
        };
    public static double armLength = 0.5;

    public static string getNextLevelType()
    {
        return levels[currentIndex + 1].type;
    }

}
 