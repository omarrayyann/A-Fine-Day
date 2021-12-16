using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Holds Menu objects together to facillitate the movement between menus
public class MenuGroup : MonoBehaviour
{
    [SerializeField]
    public Menu[] menus;

    // Sets the parent menuGroup of its children menus to itself
    private void Awake()
    {
        foreach (Menu b in menus)
        {
            b.menuGroup = this;
        }
    }
}
