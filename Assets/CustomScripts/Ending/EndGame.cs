using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Abstract class that whose children handle all end game behaviour like counting score, saving data, and drawing graphs
public abstract class EndGame : MonoBehaviour
{
    protected bool win;
    [SerializeField]
    protected windowGraph accelerationGraph = new windowGraph();
    [SerializeField]
    protected windowGraph velocityGraph = new windowGraph();

    public abstract void countScore();
}
