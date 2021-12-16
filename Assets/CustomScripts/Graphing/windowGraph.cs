using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class windowGraph : MonoBehaviour
{

    // YOU ADD A SPRITE HERE AS A 2D UI OBJECT
    [SerializeField]
    private Sprite leftSprite;
    [SerializeField]
    private Sprite rightSprite;
    [SerializeField]
    private Canvas canvas;


    /*private List<float> toPlot;
    private float xInterval;*/
    private double logRate;
    private RectTransform graphContainer;

    // Creates a circe point and places it in the required position (Graphing window in this case) 
    private GameObject CreateCircle(Vector2 anchoredPosition, bool show, char handedness)
    {
        GameObject point = new GameObject("point", typeof(Image));
        RectTransform rectTransform = new RectTransform();

        if (handedness == 'l')
        {
            point.transform.SetParent(graphContainer, false);
            point.GetComponent<Image>().sprite = leftSprite;
            rectTransform = point.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = anchoredPosition;
            if (show)
            {
                rectTransform.sizeDelta = new Vector2(40, 40);
            }
            else
            {
                rectTransform.sizeDelta = new Vector2(0, 0);
            }
        }
        else
        {
            point.transform.SetParent(graphContainer, false);
            point.GetComponent<Image>().sprite = rightSprite;
            rectTransform = point.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = anchoredPosition;
            if (show)
            {
                rectTransform.sizeDelta = new Vector2(40, 40);
            }
            else
            {
                rectTransform.sizeDelta = new Vector2(0, 0);
            }
        }
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return point;
    }

    // Called to show graph of given data
    public void showGraph(List<float> velocities, float totalTime, List<double> CollisionTimes, List<double> velocitiesAtCollisions, char handedness, double lr)
    {
        logRate = lr;
        int current = 0;
        graphContainer = transform.GetComponentInChildren<RectTransform>();
        // showGraph(velocities);

        // Measures the height of the current graph container to plot points relative to that scale
        float graphHeight = graphContainer.sizeDelta.y;

        float graphWidth = graphContainer.sizeDelta.x;

        // Maximum Possible Velocity (Y-AXIS MAX)
        // * 1.1 adds a bit more height above the graph max velocity
        float yMaximum = Mathf.Max(velocities.ToArray());
        // DIFFERENCE BETWEEN TIME INTERVALS OF VELOCITIES MEASURED
        float xSize = graphWidth / velocities.Count;

        GameObject lastCircleGameObject = null;

        // Loops throught the points
        for (int i = 0; i < velocities.Count; i++)
        {
            // xPosition is within the time intervals speicifed

            float xPosition = i * xSize;

            // yPosoiton is scaled with the graph height
            float yPosition = (velocities[i] * (graphHeight / yMaximum));

            GameObject circleGameObject = new GameObject();
            // places the circle at that point

            if (current < CollisionTimes.Count)
            {
                if (((xPosition * totalTime) / graphWidth - CollisionTimes[current]) > -logRate)
                {
                    current++;
                    circleGameObject = CreateCircle(new Vector2(xPosition, yPosition), true, handedness);

                }
                else
                {
                    circleGameObject = CreateCircle(new Vector2(xPosition, yPosition), false, handedness);
                }
            }
            else
            {
                circleGameObject = CreateCircle(new Vector2(xPosition, yPosition), false, handedness);
            }


            // creates a dot connection between every two consecutive circles if there was a previous circle
            if (lastCircleGameObject != null)
            {
                CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition, handedness);
            }                                                                                                                                                                  

            lastCircleGameObject = circleGameObject;
        }
        /*canvas.gameObject.SetActive(true);*/

    }


    // Creates a line connection between the two circle plots
    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB, char hand)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        // Colors the connections lines of each hand differently
        if (hand == 'l')
        {
            gameObject.GetComponent<Image>().color = new Color(1, 0, 0, 1f);
        }
        else if (hand == 'r')
        {
            gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
        }
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * 0.5f;
        rectTransform.localEulerAngles = new Vector3(0, 0, (Mathf.Atan2(dir.y, dir.x) * 180 / Mathf.PI));
    }

}



