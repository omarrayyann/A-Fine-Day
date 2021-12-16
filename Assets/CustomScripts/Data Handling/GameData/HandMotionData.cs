using System.Collections;
using Leap.Unity.Interaction;
using System.Collections.Generic;
using UnityEngine;
using System;
using Leap;


// Handles all motion data calculations and storing
public class HandMotionData : MonoBehaviour
{
    [SerializeField]
    private double logRate;
    private InteractionHand leftIM;
    private InteractionHand rightIM;
    private List<double> timeBetweenCollisions = new List<double>();
    private List<double> timesOfRHCollision = new List<double>();
    private List<double> timesOfLHCollision = new List<double>();
    private List<double> RHVelocitiesAtCollisions = new List<double>();
    private List<double> LHVelocitiesAtCollisions = new List<double>();
    private List<double> leftHandVelocities = new List<double>();
    private List<double> rightHandVelocities = new List<double>();
    private List<double> leftHandAccelerations = new List<double>();
    private List<double> rightHandAccelerations = new List<double>();
    private DateTime tLastLog;
    public DateTime startTime;
    public DateTime timeAtLastFrame;

    // Default constructor for a HandMotionData object with no specified logRate
    public HandMotionData()
    {
        // Finding the InterationHands in the scene to read the hand data
        GameObject right = GameObject.FindGameObjectWithTag("RightHandInteraction");
        rightIM = right.GetComponent<InteractionHand>();
        GameObject left = GameObject.FindGameObjectWithTag("LeftHandInteraction");
        leftIM = left.GetComponent<InteractionHand>();
        logRate = 0.01;
    }

    // Non-default constructor for a HandMotionData object with logRate rate
    public HandMotionData(double rate)
    {
        // Finding the InterationHands in the scene to read the hand data
        GameObject right = GameObject.FindGameObjectWithTag("RightHandInteraction");
        rightIM = right.GetComponent<InteractionHand>();
        GameObject left = GameObject.FindGameObjectWithTag("LeftHandInteraction");
        leftIM = left.GetComponent<InteractionHand>();
        logRate = rate;
    }

    // Getters:
    public DateTime getTLastLog()
    {
        return tLastLog;
    }
    public double getLogRate()
    {
        return logRate;
    }
    public DateTime getStartTime()
    {
        return startTime;
    }

    // Start the timer to record the play time
    public void StartTimer()
    {

        startTime = DateTime.Now;
        timeAtLastFrame = DateTime.Now;
        logVelocities();

    }


    // Function for logging velocities. That is performed every logRate seconds
    public void logVelocities()
    {
        tLastLog = DateTime.Now;
        leftHandVelocities.Add(leftIM.leapHand.PalmVelocity.Magnitude);
        rightHandVelocities.Add(rightIM.leapHand.PalmVelocity.Magnitude);
    }

    // Function for logging a collision. Determines which hand was closer to the target at the moment of the collision and thus was the had that collided
    public void logCollision(Vector3 target)
    {
        if (calculateDistance(rightIM.leapHand.PalmPosition, target) < calculateDistance(leftIM.leapHand.PalmPosition, target))
        {
            RHVelocitiesAtCollisions.Add(rightIM.leapHand.PalmVelocity.Magnitude);
            timesOfRHCollision.Add(DateTime.Now.Subtract(startTime).TotalSeconds);
        }
        else
        {
            LHVelocitiesAtCollisions.Add(leftIM.leapHand.PalmVelocity.Magnitude);
            timesOfLHCollision.Add(DateTime.Now.Subtract(startTime).TotalSeconds);
        }
    }

    // Calculates the acceleration from velocity data by simple slope calculation
    public void calculateAcceleration()
    {
        double prevRight = 0;
        double prevLeft = 0;
        int frequencyOfCalc = 20;
        if (rightHandVelocities.Count > 0)
            prevRight = rightHandVelocities[0];
        if (leftHandVelocities.Count > 0)
            prevLeft = leftHandVelocities[0];
        for (int i = 1, j = 0; i < rightHandVelocities.Count; i+=frequencyOfCalc, j++)
        {
            double diff = Math.Abs(rightHandVelocities[i] - prevRight);
            prevRight = rightHandVelocities[i];
            rightHandAccelerations.Add(diff / (frequencyOfCalc * logRate));
        }
        for (int i = 1, j = 0; i < leftHandVelocities.Count; i+=frequencyOfCalc, j++)
        {
            double diff = Math.Abs(leftHandVelocities[i] - prevLeft);
            prevLeft = leftHandVelocities[i];
            leftHandAccelerations.Add(diff / (frequencyOfCalc * logRate));
        }
    }

    // Using distance formula to find the distance between a hand an a target
    private static double calculateDistance(Vector hand, Vector3 target)
    {
        return Mathf.Sqrt(Mathf.Pow(hand.x - target.x, 2) + Mathf.Pow(hand.y - target.y, 2) + Mathf.Pow(hand.z - target.z, 2));
    }

    // Adds a specific value at the specified index from any of the list parameters of this class
    public void addTo(string list, double data)
    {
        switch (list)
        {
            case "timeBetweenCollisions":
                timeBetweenCollisions.Add(data);
                break;
            case "timesOfRHCollision":
                timesOfRHCollision.Add(data);
                break;
            case "timesOfLHCollision":
                timesOfLHCollision.Add(data);
                break;
            case "RHVelocitiesAtCollisions":
                RHVelocitiesAtCollisions.Add(data);
                break;
            case "LHVelocitiesAtCollisions":
                LHVelocitiesAtCollisions.Add(data);
                break;
            case "leftHandVelocities":
                leftHandVelocities.Add(data);
                break;
            case "rightHandVelocities":
                rightHandVelocities.Add(data);
                break;
            case "leftHandAccelerations":
                leftHandAccelerations.Add(data);
                break;
            case "rightHandAccelerations":
                rightHandAccelerations.Add(data);
                break;
            default:
                Debug.Log("Invalid request");
                break;
        }
    }

    // Removes the last element from any of the list parameters of this class
    public void removeAtEnd(string list)
    {
        switch (list)
        {
            case "timeBetweenCollisions":
                timeBetweenCollisions.RemoveAt(timeBetweenCollisions.Count - 1);
                break;
            case "timesOfRHCollision":
                timesOfRHCollision.RemoveAt(timesOfRHCollision.Count - 1);
                break;
            case "timesOfLHCollision":
                timesOfLHCollision.RemoveAt(timesOfLHCollision.Count - 1);
                break;
            case "RHVelocitiesAtCollisions":
                RHVelocitiesAtCollisions.RemoveAt(RHVelocitiesAtCollisions.Count - 1);
                break;
            case "LHVelocitiesAtCollisions":
                LHVelocitiesAtCollisions.RemoveAt(LHVelocitiesAtCollisions.Count - 1);
                break;
            case "leftHandVelocities":
                leftHandVelocities.RemoveAt(leftHandVelocities.Count - 1);
                break;
            case "rightHandVelocities":
                rightHandVelocities.RemoveAt(rightHandVelocities.Count - 1);
                break;
            case "leftHandAccelerations":
                leftHandAccelerations.RemoveAt(leftHandAccelerations.Count - 1);
                break;
            case "rightHandAccelerations":
                rightHandAccelerations.RemoveAt(rightHandAccelerations.Count - 1);
                break;
            default:
                Debug.Log("Invalid request");
                break;
        }
    }

    // Returns the value at the requested index from any of the list parameters of this class
    public double getFrom(string list, int index)
    {
        switch (list)
        {
            case "timeBetweenCollisions":
                return timeBetweenCollisions[index];
            case "timesOfRHCollision":
                return timesOfRHCollision[index];
            case "timesOfLHCollision":
                return timesOfLHCollision[index];
            case "RHVelocitiesAtCollisions":
                return RHVelocitiesAtCollisions[index];
            case "LHVelocitiesAtCollisions":
                return LHVelocitiesAtCollisions[index];
            case "leftHandVelocities":
                return leftHandVelocities[index];
            case "rightHandVelocities":
                return rightHandVelocities[index];
            case "leftHandAccelerations":
                return leftHandAccelerations[index];
            case "rightHandAccelerations":
                return rightHandAccelerations[index];
            default:
                Debug.Log("Invalid request");
                return 0;
        }
    }

    // Returns the any of the list parameters of this class
    public List<double> getList(string list)
    {
        switch (list)
        {
            case "timeBetweenCollisions":
                return timeBetweenCollisions;
            case "timesOfRHCollision":
                return timesOfRHCollision;
            case "timesOfLHCollision":
                return timesOfLHCollision;
            case "RHVelocitiesAtCollisions":
                return RHVelocitiesAtCollisions;
            case "LHVelocitiesAtCollisions":
                return LHVelocitiesAtCollisions;
            case "leftHandVelocities":
                return leftHandVelocities;
            case "rightHandVelocities":
                return rightHandVelocities;
            case "leftHandAccelerations":
                return leftHandAccelerations;
            case "rightHandAccelerations":
                return rightHandAccelerations;
            default:
                Debug.Log("Invalid request");
                return null;
        }
    }

    // Returns the length of any of the list parameters of this class
    public int getLengthOf(string list)
    {
        switch (list)
        {
            case "timeBetweenCollisions":
                return timeBetweenCollisions.Count;
            case "timesOfRHCollision":
                return timesOfRHCollision.Count;
            case "timesOfLHCollision":
                return timesOfLHCollision.Count;
            case "RHVelocitiesAtCollisions":
                return RHVelocitiesAtCollisions.Count;
            case "LHVelocitiesAtCollisions":
                return LHVelocitiesAtCollisions.Count;
            case "leftHandVelocities":
                return leftHandVelocities.Count;
            case "rightHandVelocities":
                return rightHandVelocities.Count;
            case "leftHandAccelerations":
                return leftHandAccelerations.Count;
            case "rightHandAccelerations":
                return rightHandAccelerations.Count;
            default:
                Debug.Log("Invalid request");
                return 0;
        }
    }

}
