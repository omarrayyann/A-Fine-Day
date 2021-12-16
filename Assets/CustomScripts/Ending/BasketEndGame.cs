using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;

// Handles the end behaviour of a BasketMinigame level
public class BasketEndGame : EndGame
{
    [SerializeField]
    private MenuGroup menuGroup;
    [SerializeField]
    private TextMeshProUGUI ApplesCaught;
    [SerializeField]
    private TextMeshProUGUI ApplesDropped;
    [SerializeField]
    private TextMeshProUGUI WinLostText;
    [SerializeField]
    private Basket basket;
    [SerializeField]
    private Basket ground;
    [SerializeField]
    BasketSpawner spawner;

    // Called onto to check if the game has ended
    public void checkEnd()
    {
        // If the sum of dropped and caught apples is equal to the total number of apples spawned, the game has ended and the score should be calculated
        if (basket.getScore() + ground.getScore() == spawner.getNumTargets())
        {
            ApplesCaught.fontSize = 0;
            ApplesDropped.fontSize = 0;
            basket.calculateAcceleration();
            countScore();
        }
    }
    public override void countScore()
    {
        // Shows the menus
        menuGroup.menus[0].gameObject.SetActive(true);
        menuGroup.menus[0].wait(3); // Asks the menu to wait before becoming interactible with so that the player doesn't accidentally press a button
        double accuracy = (double)basket.getScore() / (basket.getScore() + ground.getScore());
        // Calculating player score and determining whether they won or not
        double totalTimeSpent = DateTime.Now.Subtract(basket.getStartTime()).TotalSeconds;
        bool win = accuracy >= (Levels.levels[Levels.currentIndex] as basketLevel).accuracyToPass;
        // Displaying the appropriate buttons and messages according to winning or losing
        if (win)
        {

            if (Levels.currentIndex != Levels.levels.Length - 1)
            {

                menuGroup.menus[0].buttons[3].setText("Next Level");
                menuGroup.menus[0].buttons[3].levelIndex = Levels.currentIndex + 1;
                menuGroup.menus[0].buttons[3].path = Levels.getNextLevelType();

            }

            else
            {
                menuGroup.menus[0].buttons[3].gameObject.SetActive(false);
            }


            menuGroup.menus[0].buttons[0].setText("Play Again");
            menuGroup.menus[0].buttons[0].levelIndex = Levels.currentIndex;

            WinLostText.text = "You Won! :)";
            WinLostText.fontSize = 50;
        }
        else
        {
            menuGroup.menus[0].buttons[0].setText("Try Again");
            menuGroup.menus[0].buttons[0].levelIndex = Levels.currentIndex;
            menuGroup.menus[0].buttons[3].gameObject.SetActive(false);
            WinLostText.text = "You Lost! :(";
            WinLostText.fontSize = 50;
        }
        // Saving this run's data as a GameData object
        GameData runData = new GameData(Levels.currentIndex, basket.getMotionData(), accuracy, totalTimeSpent, win);
        DataManager.gameData.Add(runData); // Adding this run data to the DataManager
        // DataManagers draws the graphs so that the user can view them if they choose to
        DataManager.graphAccelerationData(runData.getRunID(), accelerationGraph);
        DataManager.graphVelocityData(runData.getRunID(), velocityGraph);
    }
}
