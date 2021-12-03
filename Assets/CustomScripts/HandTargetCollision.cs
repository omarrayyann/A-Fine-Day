using System.Collections.Generic;
using UnityEngine;
using System.Globalization;
using System;
using UnityEngine.UI;
using Leap.Unity.Interaction;
using TMPro;

public class HandTargetCollision : MonoBehaviour
{



    [SerializeField]
    public InteractionHand leftHandInteractionManager;
    [SerializeField]
    public InteractionHand rightHandInteractionManager;
    [SerializeField]
    public TextMeshProUGUI scoreCountText;
    public TextMeshProUGUI timeText;
    [SerializeField]
    private Spawner spawner;
    public int scoreCount;
    public List<double> timeBetweenCollisions = new List<double>();
    [SerializeField]
    public double allowedTime;
    [SerializeField]
    public bool showTotalTimeRemaining;
  
    public Vector3 positionAtLastFrame;
    public DateTime timeAtLastFrame;

    public bool gotFirst = false;
    public DateTime lastSpawn;
    public DateTime endTime;


    private void Start()
    {
        timeAtLastFrame = DateTime.Now;
        lastSpawn = DateTime.Now;
        endTime = DateTime.Now.AddSeconds(allowedTime);
        scoreCountText.text = "Score Count: 0";

    }


    private void Update()
    {

        float leftHandVelocity = leftHandInteractionManager.leapHand.PalmVelocity.Magnitude;
        float rightHandVelocity = rightHandInteractionManager.leapHand.PalmVelocity.Magnitude;

        Debug.Log("Left Hand Velocity: " + leftHandVelocity + " Right Hand Velocity: " + rightHandVelocity);


        if (showTotalTimeRemaining) {
        double timeLeft = endTime.Subtract(DateTime.Now).TotalSeconds;
            if (timeLeft>=0){
            timeText.text = "Time Remaining: " + timeLeft.ToString("0.00");}
            else{
                // Time Ended
                 timeText.text = "Time Over";
                 timeText.color = new Color(255, 0, 0, 255);
                 // Flash Text


            }
        }
        else {
            timeText.text = "Time: " + DateTime.Now.Subtract(lastSpawn).TotalSeconds.ToString("0.00"); ;
        }


    }

    private bool IsHand(Collider other)
    {
        if (other.name == "Contact Fingerbone" || other.name == "Contact Palm Bone") 
            return true;
        else
            return false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (IsHand(other))
        {
            Debug.Log("A hand collided");
            scoreCount++;
            DateTime currentTime = DateTime.Now;
            timeBetweenCollisions.Add(DateTime.Now.Subtract(lastSpawn).TotalSeconds);
            Debug.Log("Current Score: " + scoreCount + "\nTimer Interval Since Last Collision: " + timeBetweenCollisions[timeBetweenCollisions.Count-1]);
            scoreCountText.text = "Score Count: " + scoreCount;
            lastSpawn = DateTime.Now;
            spawner.spawn();
            
        }
    }
}
