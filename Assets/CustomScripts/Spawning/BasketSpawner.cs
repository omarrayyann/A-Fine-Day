using UnityEngine;
using System.Collections.Generic;


// Handles all spawning in the basket minigame
public class BasketSpawner : Spawner
{
    [SerializeField]
    private float treeHeight;
    [SerializeField]
    private Basket basket;
    [SerializeField]
    private int numberOfTargets;

    // Loads the level parameters
    private void Start()
    {
        armLength = Levels.armLength;
        numberOfTargets = (Levels.levels[Levels.currentIndex] as basketLevel).numberOfTargets;
        difficultyPercentage = (Levels.levels[Levels.currentIndex] as basketLevel).difficultyPercentage;
    }

    // Doesn't spawn until the callibrate script tells it to do so
    public void Update(){
        if (basket.getCountdownEnded() && callibrated){
            spawn();
            callibrated = false;
        }
    }

    // Spawning script: spawns the apples in the part of a sphere defined by the radius armLength
    public override void spawn()
    {
        for (int i = 0; i < numberOfTargets; i++)
        {
            zPos = Random.Range((1-(float)difficultyPercentage)*(float)armLength, (float)armLength);
            float X2 = Mathf.Pow((float)armLength, 2) - Mathf.Pow(zPos, 2);
            xPos = Random.Range(-Mathf.Sqrt(X2), Mathf.Sqrt(X2));
            yPos = Random.Range(treeHeight, Mathf.Sqrt(X2 - Mathf.Pow(xPos, 2)));
            Instantiate(target, new Vector3(xPos, yPos, zPos) + cameraTransform.position, Quaternion.identity);
        } 
    }

    // Getter:
    public int getNumTargets()
    {
        return numberOfTargets;
    }
}
