using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Leap.Unity.Interaction;
using System;
using UnityEngine.SceneManagement;
using TMPro;


public class FindArmLength : MonoBehaviour
{
    [SerializeField]
    private Transform cameraTransform;
    private double vLast;
    private double timeSpentStationary;
    private InteractionHand leftIM;
    private InteractionHand rightIM;
    [SerializeField]
    private MenuGroup menuGroup;
    private bool wokeUp = false;
    [SerializeField]
    private TextMeshProUGUI callibrationInstructions;
    [SerializeField]
    private TextMeshProUGUI title;

    // When the script wakes up, it tries to fine the left and right InteractionHands to be able to read the velocities and positions
    private void Awake()
    {
        GameObject right = GameObject.FindGameObjectWithTag("RightHandInteraction");
        rightIM = right.GetComponent<InteractionHand>();
        GameObject left = GameObject.FindGameObjectWithTag("LeftHandInteraction");
        leftIM = left.GetComponent<InteractionHand>();
    }

    // When another scripts calls onto the WakeUp function, it starts the callibration process, hiding the title text and displaying the instructions
    public void WakeUp()
    {
        title.fontSize = 0;
        callibrationInstructions.fontSize = 50;
        vLast = rightIM.velocity.magnitude;
        timeSpentStationary = 0;
        wokeUp = true;
    }

    // If the menu scripts asks this script to wake up, it checks for how long the right arm has been held steady and once that reaches 5 seconds or more it calculates the arm length
    private void Update()
    {
        if (wokeUp)
        {
            if (Math.Abs(rightIM.velocity.magnitude) < 0.5 && Math.Abs(vLast) < 0.5)
            {
                timeSpentStationary += Time.deltaTime;
            }
            else
            {
                timeSpentStationary = 0;
            }
            if (timeSpentStationary >= 5)
            {
                measureDistance();
            }
            vLast = rightIM.velocity.magnitude;
        }
    }

    // Finds the distance of the right hand from the camera and saves it into the Levels script and ends the callibration process
    private void measureDistance()
    {
        double length = Mathf.Sqrt(Mathf.Pow(rightIM.leapHand.PalmPosition.x - cameraTransform.position.x, 2) + Mathf.Pow(rightIM.leapHand.PalmPosition.z - cameraTransform.position.z, 2) + Mathf.Pow(rightIM.leapHand.PalmPosition.z - cameraTransform.position.z, 2));
        Levels.armLength = length*0.8;
        menuGroup.menus[0].gameObject.SetActive(true);
        menuGroup.menus[0].wait(1);
        wokeUp = false;
        callibrationInstructions.fontSize = 0;
        title.fontSize = 100;
    }
}
