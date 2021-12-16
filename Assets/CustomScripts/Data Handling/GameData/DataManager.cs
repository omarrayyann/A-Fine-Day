using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// DataManager is a static class that stores all the gameplay data (including motion data) from when the game was played
// Currently, all this data is wiped away after the run ends but in the future all that's needed to save the data between gameplay sessions
// would be to record the DataManagers data
public static class DataManager
{
    public static List<GameData> gameData = new List<GameData>();

    // Drawing the velocity data onto the graph that was sent to it
    public static void graphVelocityData(int run, windowGraph graph)
    {
        // Getting the necessary information
        List<double> timeBetweenCollisions = gameData[run].getMotionData().getList("timeBetweenCollisions");
        List<double> leftHandVelocities = gameData[run].getMotionData().getList("leftHandVelocities");
        List<double> rightHandVelocities = gameData[run].getMotionData().getList("rightHandVelocities");
        List<float> rightHandVelocitiesFloat = new List<float>();
        List<float> leftHandVelocitiesFloat = new List<float>();

        // Converting the double lists into floats
        for (int i = 0; i < rightHandVelocities.Count; i++)
        {

            rightHandVelocitiesFloat.Add((float)rightHandVelocities[i]);

        }
        for (int i = 0; i < leftHandVelocities.Count; i++)
        {

            leftHandVelocitiesFloat.Add((float)leftHandVelocities[i]);

        }
        float totalTimeSpent = (float)gameData[run].getTimeSpent();
        List<double> timesOfRHCollisions = gameData[run].getMotionData().getList("timesOfRHCollision");
        List<double> RHVelocitiesAtCollision = gameData[run].getMotionData().getList("RHVelocitiesAtCollisions");
        List<double> timesOfLHCollision = gameData[run].getMotionData().getList("timesOfLHCollision");
        List<double> LHVelocitiesAtCollision = gameData[run].getMotionData().getList("LHVelocitiesAtCollisions");
        double logRate = gameData[run].getMotionData().getLogRate();

        // Send the data to be drawn
        graph.showGraph(rightHandVelocitiesFloat, totalTimeSpent, timesOfRHCollisions, RHVelocitiesAtCollision, 'r', logRate);
        graph.showGraph(leftHandVelocitiesFloat, totalTimeSpent, timesOfLHCollision, LHVelocitiesAtCollision, 'l', logRate);
    }

    // Drawing the acceleration data onto the graph that was sent to it
    public static void graphAccelerationData(int run, windowGraph graph)
    {
        // Getting the necessary information
        List<double> timeBetweenCollisions = gameData[run].getMotionData().getList("timeBetweenCollisions");
        List<double> leftHandAccelerations = gameData[run].getMotionData().getList("leftHandAccelerations");
        List<double> rightHandAccelerations = gameData[run].getMotionData().getList("rightHandAccelerations");
        List<float> rightHandAccelerationsFloat = new List<float>();
        List<float> leftHandAccelerationsFloat = new List<float>();

        // Converting the double lists into floats
        for (int i = 0; i < rightHandAccelerations.Count; i++)
        {

            rightHandAccelerationsFloat.Add((float)rightHandAccelerations[i]);

        }
        for (int i = 0; i < leftHandAccelerations.Count; i++)
        {

            leftHandAccelerationsFloat.Add((float)leftHandAccelerations[i]);

        }
        float totalTimeSpent = (float)gameData[run].getTimeSpent();
        List<double> timesOfRHCollisions = gameData[run].getMotionData().getList("timeBetweenCollisions");
        List<double> RHVelocitiesAtCollision = gameData[run].getMotionData().getList("RHVelocitiesAtCollisions");
        List<double> timesOfLHCollision = gameData[run].getMotionData().getList("timesOfLHCollision");
        List<double> LHVelocitiesAtCollision = gameData[run].getMotionData().getList("LHVelocitiesAtCollisions");
        double logRate = gameData[run].getMotionData().getLogRate();

        // Send the data to be drawn
        graph.showGraph(rightHandAccelerationsFloat, totalTimeSpent, timesOfRHCollisions, RHVelocitiesAtCollision, 'r', logRate);
        graph.showGraph(leftHandAccelerationsFloat, totalTimeSpent, timesOfLHCollision, LHVelocitiesAtCollision, 'l', logRate);
    }
}
