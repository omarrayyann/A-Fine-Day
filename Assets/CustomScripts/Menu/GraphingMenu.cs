using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Graphing menu is a specific type of menu that handles showing and hiding graphs. Later on can be made to inherit from the Menu class to avoid 
// adding two scripts to the menu gameObject
public class GraphingMenu : MonoBehaviour
{
    private windowGraph[] graphs;
    [SerializeField]
    private GameObject canvas;
    public int currentIndex = 0;
    public int numGraphs;

    // When the scene is loaded, try to find all the available windowGraphs
    private void Awake()
    {
        graphs = canvas.GetComponentsInChildren<windowGraph>();
        Debug.Log("Found " + graphs.Length + " WindowGraphs");
        numGraphs = graphs.Length;
    }

    // manageGraphs hides all the graphs or shows the one that it currently is on
    public void manageGraphs()
    {
        if (this.isActiveAndEnabled)
        {
            canvas.SetActive(true);
            foreach(windowGraph graph in graphs)
            {
                graph.gameObject.SetActive(false);
            }
            graphs[currentIndex].gameObject.SetActive(true);

        }
        else
        {
            canvas.SetActive(false);
        }
    }

    // Hides all the graphs
    public void hideAllGraphs()
    {
        canvas.SetActive(false);
    }
}
