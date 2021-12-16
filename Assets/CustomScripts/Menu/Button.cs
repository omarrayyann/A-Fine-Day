using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

// Stores what this button has to do and asks its parent menu to go to the specified path saved in the button
public class Button : MonoBehaviour
{
    [SerializeField]
    private string text = "";
    public TextMeshPro buttonText;
    [SerializeField]
    public string path;
    public Menu parentMenu;
    [SerializeField]
    public int levelIndex = -1;

    // When it wakes up, it finds the text it draws onto
    private void Awake()
    {
        buttonText = this.gameObject.GetComponentInChildren<TextMeshPro>();
        buttonText.text = text;
    }

    // Checks if what just collided was a hand
    private bool IsHand(Collider other)
    {
        if (other.name == "Contact Fingerbone" || other.name == "Contact Palm Bone")
            return true;
        else
            return false;
    }

    // If a hand presses, perform the pressed function
    private void OnTriggerEnter(Collider other)
    {
        if (IsHand(other))
        {
            pressed();
        }
    }

    // Sends the path to its parent menu as an integer if the path is an integer; otherwise, it sends it as a string
    public void pressed()
    {
        if (levelIndex >= 0)
        {
            Levels.currentIndex = levelIndex;
        }
        bool isNumber = int.TryParse(path, out int n);
        if (isNumber)
        {
            parentMenu.moveTo(int.Parse(path));
        }
        else
        {
            parentMenu.moveTo(path);
        }
    }

    // UI Element handler
    public void setText(string str)
    {
        text = str;
        buttonText.text = text;
    }

}
