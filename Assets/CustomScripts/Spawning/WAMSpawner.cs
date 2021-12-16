using UnityEngine;

// Handles the spawning for the WAM minigame
public class WAMSpawner : Spawner
{
    // Load the level parameters
    private void Start()
    {
        armLength = Levels.armLength;
        difficultyPercentage = (Levels.levels[Levels.currentIndex] as wackLevel).difficultyPercentage;
    }

    // Don't spawn until the VR headset has been callibrated
    private void Update()
    {
        if (callibrated)
        {
            spawn();
            callibrated = false;
        }
    }

    // Spawn the targets in a region of the sphere of radius armLength
    public override void spawn()
    {
        zPos = Random.Range((1-(float)difficultyPercentage)*(float)armLength, (float)armLength);
        float X2 = Mathf.Pow((float)armLength, 2) - Mathf.Pow(zPos, 2);
        xPos = Random.Range(-Mathf.Sqrt(X2), Mathf.Sqrt(X2));
        yPos = Random.Range(-Mathf.Sqrt(X2 - Mathf.Pow(xPos, 2)), Mathf.Sqrt(X2 - Mathf.Pow(xPos, 2)));
        target.transform.position = new Vector3(xPos, yPos, zPos) + cameraTransform.position;
    }
}
