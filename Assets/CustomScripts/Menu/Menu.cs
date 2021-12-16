using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Handles the motion between scenes and menus
public class Menu : MonoBehaviour
{
    public MenuGroup menuGroup;
    public Button[] buttons;
    [SerializeField]
    private bool activeOnStart;
    [SerializeField]
    private Material locked;

    // Set all the children buttons' parentMenus to this menu 
    private void Awake()
    {
        foreach (Button b in buttons)
        {
            b.parentMenu = this;
        }
    }

    // Make all the locked stages to be playable
    private void Start()
    {
        foreach (Button b in buttons)
        {
            bool unlocked = b.levelIndex == 0;
            if (b.levelIndex > 0)
            {
                for (int i = 0; i < DataManager.gameData.Count && !unlocked; i++)
                {
                    if (DataManager.gameData[i].level == b.levelIndex - 1 && DataManager.gameData[i].win)
                    {
                        unlocked = true;
                    }
                }
                if (!unlocked)
                {
                    b.gameObject.GetComponent<MeshRenderer>().material = locked;
                }
                b.GetComponent<Collider>().enabled = unlocked;
            }
        }
        gameObject.SetActive(activeOnStart);
    }

    // Makes the menus wait before they are pressable to avoid accidental pressing if the menu spawns on the users hand
    IEnumerator waiter(int waitTime)
    {
        foreach (Button b in buttons)
        {
            b.GetComponent<Collider>().enabled = false;
        }
        yield return new WaitForSeconds(waitTime);
        foreach (Button b in buttons)
        {
            b.GetComponent<Collider>().enabled = true;
        }
        lockLevels();

    }

    // Locks all the levels that haven't been unlocked
    private void lockLevels()
    {
        foreach (Button b in buttons)
        {
            bool unlocked = b.levelIndex == 0;
            if (b.levelIndex > 0)
            {
                for (int i = 0; i < DataManager.gameData.Count && !unlocked; i++)
                {
                    if (DataManager.gameData[i].level == b.levelIndex - 1 && DataManager.gameData[i].win)
                    {
                        unlocked = true;
                    }
                }
                if (!unlocked)
                {
                    b.gameObject.GetComponent<MeshRenderer>().material = locked;
                }
                b.GetComponent<Collider>().enabled = unlocked;
            }
        }
    }

    // Uses the waiter to make a menu wait for the specified duration
    public void wait(int waitTime)
    {
        StartCoroutine(waiter(waitTime));
    }

    // Handles motion to other scenes and scripts
    public void moveTo(string path)
    {
        switch (path)
        {
            case "BasketMinigame":
                SceneManager.LoadScene("BasketGame");
                break;
            case "WAMMinigame":
                SceneManager.LoadScene("Whack-a-mole");
                break;
            case "Data":
                break;
            case "Main Menu":
                SceneManager.LoadScene("Main Menu");
                break;
            case "Callibrate":
                GameObject.FindGameObjectWithTag("Callibrator").GetComponent<FindArmLength>().WakeUp();
                gameObject.SetActive(false);
                break;
            case "GraphingButton+":
                {
                    bool isGraphingMenu = gameObject.TryGetComponent<GraphingMenu>(out GraphingMenu graphingMenu);
                    if (isGraphingMenu)
                    {
                        if (graphingMenu.currentIndex < graphingMenu.numGraphs - 1)
                            graphingMenu.currentIndex++;
                        graphingMenu.manageGraphs();
                    }
                    break;
                }
            case "GraphingButton-":
                {
                    bool isGraphingMenu = gameObject.TryGetComponent<GraphingMenu>(out GraphingMenu graphingMenu);
                    if (isGraphingMenu)
                    {
                        if (graphingMenu.currentIndex != 0)
                            graphingMenu.currentIndex--;
                        graphingMenu.manageGraphs();
                    }
                    break;
                }
            default:
                Debug.Log("Undefined command");
                break;
        }
    }

    // Handles motion to other menus
    public void moveTo(int menuNumber)
    {
        bool wasGraphingMenu = gameObject.TryGetComponent<GraphingMenu>(out GraphingMenu initialGraphingMenu);
        if (wasGraphingMenu)
        {
            initialGraphingMenu.hideAllGraphs();
        }
        menuGroup.menus[menuNumber].gameObject.SetActive(true);
        this.gameObject.SetActive(false);
        menuGroup.menus[menuNumber].wait(1);
        bool isGraphingMenu = menuGroup.menus[menuNumber].gameObject.TryGetComponent<GraphingMenu>(out GraphingMenu graphingMenu);
        if (isGraphingMenu)
        {
            graphingMenu.manageGraphs();
        }
    }
}
