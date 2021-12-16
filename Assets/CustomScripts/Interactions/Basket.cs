using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;
using System.Collections;


// Handles basket interactions. Repurposed to also be used on the ground.
public class Basket : MonoBehaviour
{
    [SerializeField]
    private string customText;
    [SerializeField]
    private TextMeshProUGUI applesCount;
    private int score = 0;
    [SerializeField]
    private Transform explosion;
    [SerializeField]
    private BasketEndGame ender;
    private HandMotionData motionData;
    [SerializeField]
    private double logRate;
    [SerializeField]
    private TextMeshProUGUI countdownText;
    private bool countdownEnded = false;
    [SerializeField]
    private TextMeshProUGUI conditionsToWin;

    // Show the winning conditions and start countdown
    private void Start()
    {
        conditionsToWin.text = "Collect " + Math.Ceiling((Levels.levels[Levels.currentIndex] as basketLevel).accuracyToPass * (Levels.levels[Levels.currentIndex] as basketLevel).numberOfTargets) + " to win";
        StartCoroutine(countDown());    
    }

 
    // Performs the countdown
     IEnumerator countDown()
    {
        countdownText.gameObject.SetActive(true);
        for (int count = 3; count > 0; count--) {
            countdownText.text = count.ToString();
            yield return new WaitForSeconds(1);
        }
        countdownText.gameObject.SetActive(false);
        countdownEnded=true;
        conditionsToWin.gameObject.SetActive(false);
        Begin();
    }
 

    // Starting a basket game
    private void Begin()
    {
        if (logRate > 0) { 
            motionData = new HandMotionData(logRate);
            motionData.StartTimer();
        }
        else
            motionData = null;
    }

    // Logs the velocities whenever logRate seconds pass
    private void Update()
    {
        if (countdownEnded)
        {
            if (logRate > 0 && DateTime.Now.Subtract(motionData.getTLastLog()).TotalSeconds > logRate)
            {
                motionData.logVelocities();
            }
        }
    }

    // Log a collition by asking the HandMotionData object to log it
    public void logCollision(Vector3 target)
    {
        motionData.logCollision(target);
    }
    
    // Ask the HandMotionData object to calculate the acceleration
    public void calculateAcceleration()
    {
        motionData.calculateAcceleration();
    }

    // Once an apple enters the basket volume do the following
    private void OnTriggerEnter(Collider collider)
    {
        // If the object that entered the basket was an apple, set it to a counted apple such that it doesn't get counted again and increment the score
        // spawn a particle system, then check if the game has ended
        if (collider.tag == "Target")
        {
            collider.tag = "Counted"; // To avoid double counting of core
            score++;
            applesCount.text = customText + score;
            GameObject exploder = ((Transform)Instantiate(explosion, collider.transform.position, Quaternion.identity)).gameObject;
            Destroy(exploder, 1.0f);
            ender.checkEnd();
        }
    }

    // Once an apple hits the ground do the following
    private void OnCollisionEnter(Collision collision)
    {
        // If the object that entered the basket was an apple, set it to a counted apple such that it doesn't get counted again and increment the numDropped score
        // spawn a particle system, then check if the game has ended
        if (collision.collider.tag == "Target")
        {
            collision.collider.tag = "Counted"; // To avoid double counting of score
            score++;
            applesCount.text = customText + score;
            GameObject exploder = ((Transform)Instantiate(explosion, collision.collider.transform.position, Quaternion.identity)).gameObject;
            Destroy(exploder, 1.0f);
            ender.checkEnd();
        }
    }

    // Getters:
    public double getLogRate()
    {
        return logRate;
    }
    public int getScore()
    {
        return score;
    }
    public List<double> getList(string list)
    {
        return motionData.getList(list);
    }
    public DateTime getStartTime()
    {
        return motionData.getStartTime();
    }

    public HandMotionData getMotionData()
    {
        return motionData;
    }
  public bool getCountdownEnded()
    {
        return countdownEnded;
    }

}