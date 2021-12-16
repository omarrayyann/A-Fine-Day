using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class WackAMoleEndGame : EndGame
{
    [SerializeField]
    private MenuGroup menuGroup;
    [SerializeField]
    private TextMeshProUGUI timeText;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI WinLostText;

    [SerializeField]
    private HandTargetCollision molesCollision;


    [SerializeField]
    private GameObject target;    
    
    
    public void checkEnd()
    {
        Destroy(target);
        timeText.fontSize = 0;
        scoreText.fontSize = 0;
        molesCollision.calculateAcceleration();
        countScore();
        
    }

    public override void countScore()
    {
        menuGroup.menus[0].gameObject.SetActive(true);
        menuGroup.menus[0].wait(3);

        // Hiding UI Present
        molesCollision.showScore(false);
        molesCollision.showTime(false);

        // Getting and Preparing Data
        double score = molesCollision.getScoreCount();
        double totalTimeSpent = molesCollision.getAllowedTime();
        bool win = score >= (Levels.levels[Levels.currentIndex] as wackLevel).targetsToPass;
        if (win)
        {

            if (Levels.currentIndex != Levels.levels.Length - 1){

            menuGroup.menus[0].buttons[3].setText("Next Level");
            menuGroup.menus[0].buttons[3].levelIndex = Levels.currentIndex + 1;
            menuGroup.menus[0].buttons[3].path = Levels.getNextLevelType();

            }
            else {
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
        GameData runData = new GameData(Levels.currentIndex, molesCollision.getMotionData(), score, totalTimeSpent, win);
        DataManager.gameData.Add(runData);
        DataManager.graphAccelerationData(runData.getRunID(), accelerationGraph);
        DataManager.graphVelocityData(runData.getRunID(), velocityGraph);

    }
}
