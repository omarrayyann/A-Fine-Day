using System.Collections.Generic;
using UnityEngine;/*
using System.Globalization;*/
using System;/*
using UnityEngine.UI;
using Leap.Unity.Interaction;*/
using TMPro;
using System.Collections;


// Handles all WAM game interactions
public class HandTargetCollision : MonoBehaviour
{


    [SerializeField]
    private Transform explosion;
    [SerializeField]
    private TextMeshProUGUI scoreCountText;
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private TextMeshProUGUI countdownText;
    [SerializeField]
    private Spawner spawner;
    private int scoreCount;
    private double allowedTime;
    [SerializeField]
    private bool showTotalTimeRemaining;
    [SerializeField]
    private double timeBetweenVelocityMeasurements;
    [SerializeField]
    private WackAMoleEndGame ender;
    private bool ended = false;
    [SerializeField]
    private double logRate;
    private DateTime endTime;
    public DateTime lastSpawn;
    private bool countdownEnded = false;
    [SerializeField]
    private TextMeshProUGUI conditionsToWin;
    public bool timed = true;
    public bool gotFirst = false;
    private HandMotionData motionData;
    private double targetsToWin;


    // Displays the winning conditions. If the allowed time is -1, that means it's a non-timed level. To avoid creating new logic for non-timed
    // levels, the time is just set as a very large value (100 hours). After that, it starts the countdown
    private void Start()
    {
        targetsToWin = (Levels.levels[Levels.currentIndex] as wackLevel).targetsToPass;
        allowedTime = (Levels.levels[Levels.currentIndex] as wackLevel).allowedTime;

        if (allowedTime == -1)
        {
            allowedTime = 360000;
            timeText.gameObject.SetActive(false);
            timed = false;
            conditionsToWin.text = "Hit " + targetsToWin.ToString()  + " to win";
        }
        else {
            conditionsToWin.text = "Hit " + targetsToWin.ToString() + " in " + allowedTime.ToString() + " seconds to win";
        }
        StartCoroutine(countDown());

        
    }
    
    // Performs the countdown
     IEnumerator countDown()
    {            
        for (int count = 3; count > 0; count--) {
            countdownText.text = count.ToString();
            countdownText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
        }
        countdownEnded = true;
        countdownText.gameObject.SetActive(false);
        conditionsToWin.gameObject.SetActive(false);
        Begin();
    }


 
    // Starts the WAM game
    private void Begin()
    {
        motionData = new HandMotionData(logRate);
        motionData.StartTimer();
        endTime = DateTime.Now.AddSeconds(allowedTime);
        lastSpawn = DateTime.Now;
        if (timed)
        {
            scoreCountText.text = "Score Count: 0";
        }
        else {
            scoreCountText.text = "Hit to Win: " + ((int)targetsToWin -scoreCount);
        }
        motionData.logVelocities();
    }

    // Once the time ends, show it on the timer text
    private void timeEnded()
    {
        timeText.text = "Time Over";
        timeText.color = new Color(255, 0, 0, 255);
        ender.checkEnd();

    }

    // Asks the HandMotionData object to log velocities every logRate seconds and shows remaining time. 
    private void Update()
    {
        if (countdownEnded)
        {
            if (DateTime.Now.Subtract(motionData.getTLastLog()).TotalSeconds > logRate)
            {
                motionData.logVelocities();
            }

            if (showTotalTimeRemaining)
            {
                double timeLeft = endTime.Subtract(DateTime.Now).TotalSeconds;
                if (timeLeft >= 0)
                {
                    timeText.text = "Time Remaining: " + timeLeft.ToString("0.00");
                }
                else if (ended == false)
                {
                    timeEnded();
                    ended = true;
                }
            }
            else
            {
                timeText.text = "Time: " + DateTime.Now.Subtract(lastSpawn).TotalSeconds.ToString("0.00"); ;
            }
        }

    }

    // Calculates acceleration via its HandMotionData object
    public void calculateAcceleration()
    {
        motionData.calculateAcceleration();
    }

    // Checks if what just collided was a hand
    private bool IsHand(Collider other)
    {
        if (other.name == "Contact Fingerbone" || other.name == "Contact Palm Bone")
            return true;
        else
            return false;
    }

    // Once a collision happens, increment the score, and check if the game has ended for if the current game mode is not timed
    void OnTriggerEnter(Collider other)
    {
        if (countdownEnded && IsHand(other))
        {
            FindObjectOfType<AudioManager>().Play("BasketScore");
            DateTime currentTime = DateTime.Now;
            motionData.addTo("timeBetweenCollisions", DateTime.Now.Subtract(lastSpawn).TotalSeconds);
            if (motionData.getFrom("timeBetweenCollisions", motionData.getLengthOf("timeBetweenCollisions") - 1) >= 0.2)
            {
                GameObject exploder = ((Transform)Instantiate(explosion, this.transform.position, Quaternion.identity)).gameObject;
                Destroy(exploder, 1.0f);
                motionData.logCollision(this.transform.position);
                scoreCount++;
                if (scoreCount >= targetsToWin && timed == false)
                {
                    allowedTime = DateTime.Now.Subtract(motionData.startTime).TotalSeconds + 0.1;
                    ender.checkEnd();
                }
                // Debug.Log("Current Score: " + scoreCount + "\nTimer Interval Since Last Collision: " + motionData.getFrom("timeBetweenCollisions", motionData.getLengthOf("timeBetweenCollisions") - 1);
                if (timed)
                {
                    scoreCountText.text = "Score Count: " + scoreCount;
                }
                else {
                    scoreCountText.text = "Hit to Win: " + ((int)targetsToWin - scoreCount);
                }
                lastSpawn = DateTime.Now;
            }
            else
            {
                motionData.removeAtEnd("timeBetweenCollisions");
            }
            spawner.spawn();

        }
    }

    // Getters:
    public double getLogRate()
    {
        return logRate;
    }

    public double getAllowedTime()
    {
        return allowedTime;
    }

    public HandMotionData getMotionData()
    {
        return motionData;
    }
    public double getScoreCount()
    {
        return scoreCount;
    }
    public bool getTimed()
    {
        return timed;
    }

    // UI Element handlers
    public void showScore(bool show){
        if(show){
            scoreCountText.gameObject.SetActive(true);
        }
        else{
            scoreCountText.gameObject.SetActive(false); 
        }
    }
     public void showTime(bool show){
        if(show){
            timeText.gameObject.SetActive(true);
        }
        else{
            timeText.gameObject.SetActive(false); 
        }
    }

}
