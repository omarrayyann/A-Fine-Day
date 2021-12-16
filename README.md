# A Fine Day

A Fine Day is a gamified version of stroke rehabilitation that allows patients to regain their limbs functionality in an engaging environment and therapists to remotely track patient progress. We would like to focus our efforts for this version of the game on developing games that target patients’ range-of-motion and motor skills.

We integrated VR (Oculus Rift) and hand-tracking (Ultraleap) to replicate motor skill and range-of-motion exercises in more engaging way while keeping track of the motion data for the therapist to analyze

Whack-A-Mole (WAM) Minigame:

Basket Minigame
Works on:
Hand-eye coordination
Range-of-Motion
Speed
Has two versions:
Non-timed: less stressful version of the game of which the objective is to hit a predefined number of targets. First few levels  so the player can slowly progress.
Timed: a race against time to hit at least a certain number of targets before the time runs out.
Works on:
Hand-eye coordination
Range-of-Motion
Grasping skills
Manipulating the environment
Goal:
Place apples that randomly spawn on a tree in front of you into the basket. The player is scored according to their accuracy, i.e. how many apples the manage to place in the basket out of the total apples spawned






Project Development

Software/Hardware Tools:

Devices: Oculus Rift, Ultraleap Hand-Tracking
Software/Development Environment: Unity, Leap Motion
Assets: SDKs (Unity & Oculus Rift), free 3D models from online asset stores (Citations at the end)

Setting Up the Environment:

Download Unity Game Engine®
Set up the VR headset:
Download Oculus® Rift SDK (Software Development Kit)
In Unity: Edit -> Project Settings -> XR Plug-in Management -> Oculus ☑
Set up Ultraleap Hands:
Download Ultra Leap® SDKs: Core, Interaction, and Hand
Import the three packages into Unity (Assets -> Import Packages -> Custom Package)
The current version of the SDK has a problem in the code which is easily fixable. When the Core package is imported, an exception is thrown because some place in the code uses OnlyUserModifiable which is obsolete in the new version of Unity. It should be changed to Editable for the error to get fixed.
Hands:
Assets -> Plugins -> LeapMotion -> Core -> Prefabs: Drag the “Leap Rig” into the project
Replace the main camera in the XR Rig with the main camera  in the Leap Rig
InteractionHands:
Assets -> Plugins -> LeapMotion -> Modules -> InteractionEngine -> Prefabs: Drag the “Interaction Manager” into the Leap Rig and remove everything except the left and right interaction hands.


CALIBRATION:

1- Arm Length Measurement: Asking the patient to hold their hand Infront of them for some time to calibrate the games according to their arm length
 
 
// Finds the distance of the right hand from the camera and saves it into the Levels script and ends the calibration process
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
 

 
2- Camera Calibration: Calibrating the camera after the game starts due to a happen jump that happens once the senor corrects
 
  public bool isReady()
   {
    bool isReady = this.transform.position.x > previousPosition.x + 0.2 || this.transform.position.y > previousPosition.y + 0.2 || this.transform.position.z > previousPosition.z + 0.2;
       previousPosition = this.transform.position;
       return isReady;
   }
 

WHACK-A-MOLE-MINIGAME:

Spawning targets:

The targets were spawned using the WAMSpawner script which inherits from the abstract class Spawner. It randomly generates the targets within the range of the arm length and using a difficulty radius constant that can affect how close to the user the target will spawn. The final position was calculated using the following formula we derived:

Z-Position: Randomly generated z position within a certain percentage of armLength

X2+Y2+Z2=Arm Length 2

X2+Y2=Arm Length 2-Z2

Y2=Arm Length2-Z2-X20X2Arm Length2-Z2

X-Position: Randomly generated random between:

 between -Arm Length 2-Z2 and +Arm Length 2-Z2 from the user

Y-Position: Found by substitution in Arm Length 2-Z2-X2

 between -Arm Length 2-Z2-X2 and +Arm Length 2-Z2-X2 from the user

Relevant Code: 

  // Spawn the targets in a region of the sphere of radius armLength
   public override void spawn()
   {
       zPos = Random.Range((1-(float)difficultyPercentage)*(float)armLength, (float)armLength);
       float X2 = Mathf.Pow((float)armLength, 2) - Mathf.Pow(zPos, 2);
       xPos = Random.Range(-Mathf.Sqrt(X2), Mathf.Sqrt(X2));
       yPos = Random.Range(-Mathf.Sqrt(X2 - Mathf.Pow(xPos, 2)), Mathf.Sqrt(X2 - Mathf.Pow(xPos, 2)));
       target.transform.position = new Vector3(xPos, yPos, zPos) + cameraTransform.position;
   }
 
 
Testing the Code:
By tweaking the Spawner.cs script so as it creates new objects instead of moving a target around, we can show the sphere of possibility for the targets to spawn.

Test 1: Arm-length=1, Difficulty Radius = 0.5 (300 Spawned Targets)


Test 2: Arm-length=3, Difficulty Radius = 0.5 (1000 Spawned Targets)
 

Test 3: Arm-length=3, Difficulty Radius = 1.5 (1000 Spawned Targets)


Handling Collisions Between the Hands and Targets:

To detect the collision between any target and the hand, the target must have a rigid body added to it with the “Is Trigger” box ticked as well as an InteractionBehaviour component. Doing so allows us to use the function OnTriggerEnter(Collider other) whenever an object collides with the target. To check that it’s a hand that collided, we only need to check the name (which will either be Contact Fingerbone or Contact Palm Bone since these are the imported prefabs names). Once a collision between the leap motion and the target occurs, the target is respawned to another position within the constraints previously mentioned. Then the player’s score counter must increment, and according to whether the game is timed or not, the game should check whether the required number of targets has been hit.

Relevant Code: 
void OnTriggerEnter(Collider other)
       {
           if (countdownEnded && IsHand(other))
           {
               FindObjectOfType<AudioManager>().Play("BasketScore");
               DateTime currentTime = DateTime.Now;
               motionData.addTo("timeBetweenCollisions", DateTime.Now.Subtract(lastSpawn).TotalSeconds);
               if (motionData.getFrom("timeBetweenCollisions", motionData.getLengthOf("timeBetweenCollisions") - 1) >= 0.2)
               {
                   GameObject exploder = ((Transform)Instantiate(explosion, this.transform.position, Quaternion.identity)).gameObject;
                   Destroy(exploder, 1.0f);
                   scoreCount++;
                   motionData.logCollision(this.transform.position);
                   // Debug.Log("Current Score: " + scoreCount + "\nTimer Interval Since Last Collision: " + motionData.getFrom("timeBetweenCollisions", motionData.getLengthOf("timeBetweenCollisions") - 1);
                   scoreCountText.text = "Score Count: " + scoreCount;
                   lastSpawn = DateTime.Now;
               }
               else
               {
                   motionData.removeAtEnd("timeBetweenCollisions");
               }
               spawner.spawn();
 
           }
       }
 

Ending the Game:

Once the conditions for winning are satisfied, the run’s data should be stored and the menus displayed.

Relevant code:

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



UI Elements:

On the screen, the game displays the current score and time remaining for the timed version or the number of hits remaining for the non timed version. Additionally, it is helpful to add a a countdown timer at the start, text describing whether you won or not, the conditions to win, etc. All these UI elements are handled using TextMeshPro objects whose attributes can be altered by the other scripts.

Design Elements:

We decided a fitting theme for this minigame would be a punching gym; therefore, we looked online for assets that fit the theme. Additionally, we added a skybox to the  All the assets are cited in the end.


 

BASKETS-MINIGAME:

Spawning targets:

The targets were spawned randomly within the range of the arm length and using a difficulty radius constant that can affect how close to the user the target will spawn. The y-position should also be above a certain treeHeight (as this minigame will include picking fruit from a tree when we add the models). The final position was calculated using the following formula we derived:

Z-Position: Randomly generated between the difficulty radius and the length of the arm from the user

X2+Y2+Z2=Arm Length 2

X2+Y2=Arm Length 2-Z2

Y2=Arm Length2-Z2-X20X2Arm Length2-Z2

X-Position: Randomly generated random between:

 between -Arm Length 2-Z2 and +Arm Length 2-Z2 from the user

Y-Position: Found by substitution in Arm Length 2-Z2-X2

Between treeHeight and +Arm Length 2-Z2-X2 from the user
 

Relevant Code: 

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
 
Testing the Code:
By tweaking the Spawner.cs script so as it creates new objects instead of moving a target around, we can show the sphere of possibility for the targets to spawn.

Testing the Spawner (Arm Length = 0.5; Difficulty Radius = 0.25; TreeHeight = 0):



Testing the Spawner (Arm Length = 1; Difficulty Radius = 0.6; TreeHeight = 0.1):



Targets Behaviour:

The targets are supposed to start at rest not interacting with each other (Is Trigger = true enables that to happen) where it spawns until the player grasps it. Once the player does, the target should start responding to gravity such that if the player lets the object drop, it doesn’t stay in its place. Also, it should be able to collide again by disabling the Is Trigger property. The way we did that is by beginning with the object's gravity disabled, then, using the interactions behaviour script that comes in the Ultraleap SDK, we can create custom behaviour so that it enables the target's gravity after collision ends. Below is a screenshot of what that looks like in Unity:



Counting Score:

The basket script handles score counting. Whenever an apple is detected within its volume, it increments the score and sets the apple as counted to avoid double counting.

Relevant Code:


Script at work:


UI Elements:

On the screen, the game must display the apple picked and apples dropped. An addition to the HandTargetCollision.cs scripts does just that:



GRAPHING:

After the user plays the game, further analysis of there hand motion is created to be later presented as acceleration-time and velocity-time grapes.. The creation of these values was handled by the windowGraph script and displaying them was managed by the GraphingMenu script.

Relevant code:
public void showGraph(List<float> velocities, float totalTime, List<double> CollisionTimes, List<double> velocitiesAtCollisions, char handedness, double lr)
   {
       logRate = lr;
       int current = 0;
       graphContainer = transform.GetComponentInChildren<RectTransform>();
       // showGraph(velocities);
 
       // Measures the height of the current graph container to plot points relative to that scale
       float graphHeight = graphContainer.sizeDelta.y;
 
       float graphWidth = graphContainer.sizeDelta.x;
 
       // Maximum Possible Velocity (Y-AXIS MAX)
       // * 1.1 adds a bit more height above the graph max velocity
       float yMaximum = Mathf.Max(velocities.ToArray());
       // DIFFERENCE BETWEEN TIME INTERVALS OF VELOCITIES MEASURED
       float xSize = graphWidth / velocities.Count;
 
       GameObject lastCircleGameObject = null;
 
       // Loops throught the points
       for (int i = 0; i < velocities.Count; i++)
       {
           // xPosition is within the time intervals speicifed
 
           float xPosition = i * xSize;
 
           // yPosoiton is scaled with the graph height
           float yPosition = (velocities[i] * (graphHeight / yMaximum));
 
           GameObject circleGameObject = new GameObject();
           // places the circle at that point
 
           if (current < CollisionTimes.Count)
           {
               if (((xPosition * totalTime) / graphWidth - CollisionTimes[current]) > -logRate)
               {
                   current++;
                   circleGameObject = CreateCircle(new Vector2(xPosition, yPosition), true, handedness);
 
               }
               else
               {
                   circleGameObject = CreateCircle(new Vector2(xPosition, yPosition), false, handedness);
               }
           }
           else
           {
               circleGameObject = CreateCircle(new Vector2(xPosition, yPosition), false, handedness);
           }
 
 
           // creates a dot connection between every two consecutive circles if there was a previous circle
           if (lastCircleGameObject != null)
           {
               CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition, handedness);
           }                                                                                                                                                                 
 
           lastCircleGameObject = circleGameObject;
       }
       /*canvas.gameObject.SetActive(true);*/
 
   }
 
 
   // Creates a line connection between the two circle plots
   private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB, char hand)
   {
       GameObject gameObject = new GameObject("dotConnection", typeof(Image));
       gameObject.transform.SetParent(graphContainer, false);
       // Colors the connections lines of each hand differently
       if (hand == 'l')
       {
           gameObject.GetComponent<Image>().color = new Color(1, 0, 0, 1f);
       }
       else if (hand == 'r')
       {
           gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 1f);
       }
       RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
       Vector2 dir = (dotPositionB - dotPositionA).normalized;
       float distance = Vector2.Distance(dotPositionA, dotPositionB);
       rectTransform.anchorMin = new Vector2(0, 0);
       rectTransform.anchorMax = new Vector2(0, 0);
       rectTransform.sizeDelta = new Vector2(distance, 3f);
       rectTransform.anchoredPosition = dotPositionA + dir * distance * 0.5f;
       rectTransform.localEulerAngles = new Vector3(0, 0, (Mathf.Atan2(dir.y, dir.x) * 180 / Mathf.PI));
   }
 


Scripts at work: 



MAIN MENU (AND MENUS IN GENERAL):

All user input is handled using the MenuGroup, Menu, and Button script. Button holds the path information of a button. Menu handles the motion between menus and scenes according to the path of the button that was pressed, as well as which buttons are accessible (to lock later levels). MenuGroup handles the showing and hiding of menus. The main menu scene is constructed with the help of those scripts.

Relevant code:


Menu Scene:



DATA HANDLING:

Reports:
To be able to generate reports, the game must be able to record the data. We came up with 3 scripts that handle all that. HandMotionData records all motion related data, GameData stores the HandMotionData of a run as well score information, and finally DataManager stores all the GameData of the runs.

Relevant Code:




Results and Evaluation

To make sure everything was working properly, we both ran tests in individual components and multiple intertwined parts. To maximize testing efficiency and efficacy, we had random students try our code, and whenever we saw anything break, we’d keep note of that to fix it. A lot of the testing has been shown earlier in the code description above. 

Challenges:

Figuring out which Ultraleap components/commands to use and when there was very little documentation.
Calibration: When the game starts, the target was always spawning very low compared to the player. After testing, we figured out that the sensor was taking time to calibrate the height. Therefore, we came up with the Calibrate script.
We spent a lot of time trying to fix a problem with the graphing 

Conclusion and Future Development

Conclusion: 
We were able to achieve most of our goals for this project. Starting from just setting up the environment, to creating the interaction skeletons, to creating the menus and graphings functionality, to integrating all of the functionalities together, and to finally designing the environment with beautiful assets, “A Fine Day” has gone a long way.

Ever since the beginning we kept the same goal in mind, regardless of the changes we made along the way: providing a fun and motivating environment for patients to work on their motor skills while logging their progress for the therapist to see.

Looking to the future, one can see a lot of potential for this project to grow, making it more useful for both the player and therapist.

Future Development:


It would be extremely beneficial if the game generated the levels according to the user's past progress, personalizing the experience and making sure no player is lagging behind or finding the minigames extremely easy. We can see this being implemented in our solution by integrating the Levels and DataManager scripts into a LevelSpawner script that analyzes past player progress and creates progressively more difficult levels .
Adding a “Show Past Progress” button to the main menu. We had that in mind while designing the menu scripts and even managed to get the GraphingMenu script to work in the minigames, but we didn’t have time to implement the last step of adding it to the main menu. The tools are already there though and with little work they can be added to the current solution. Additionally, we were planning on adding the axes to the current graphs but didn’t have the time to do so. A future version of this project should implement this fix.
Adding a cartoon companion (e.g. robot or animal) to give motivational quotes to the player and explain the rules to them. This companion would provide great company for the user as they are playing such that they never feel alone or bored.
Creating an online community with leaderboards, weekly challenges, online multiplayer functionality, etc. That would ensure the games never get boring and the patients never feel lonely while playing the games.
Adding more minigames such as:
Air-hockey
Placing objects in precise positions
Pat-a-cake
Fishing
Improving on the graphics: while the current graphics look generally beautiful, more can be done like adding animations and sounds to make the background elements come to life. 

Reflection on Learning 

At the end of this semester-long project, we take a moment to reflect on the skills we gained along the way, the mistakes we made, and what we learned from them.
Working with C# and Unity: 
We applied our previous programming skills (in Java, C++, and Swift) to learn a new programming language.
Working with Unity was an extremely rewarding process. Speaking for myself (as Dhiyaa) I’ve always wanted to make a game, inspired by my uncle who is a game developer. I never found the motivation to learn unity however, no matter how many times I tried. It was really nice to finally get myself to finish a whole game after overcoming the difficult part of actually starting to work.
Learning to operate new devices and working with few resources:
It was really nice to learn how to work with external devices (Oculus Rift and Ultraleap)
Most of the Oculus-related work was handled by unity. 
The Ultraleap however involved a lot more work. We’d often have to refer to the documentation, scour the internet forums, and even search through the scripts to find answers for our issues.
Listening to user feedback:
Whenever we got the chance, we would offer our fellow students a chance to play the game to listen to their feedback (thankfully there was never a shortage of volunteers). We’d always tried to work with the feedback we received to perform the edits within our capabilities to make the experience as natural as possible.
The W-A-M minigame was always timed, but people kept mentioning how stressful that would be as a first level.
We had to rework our code for the basket script because of some problems with the hands blocking the VR sensor. A lot of people complained about how when the hand accidentally covered the sensor, the leap hands would perform a sudden huge shift in vertical position. While this wouldn’t really break the logic of the game—the apple would still be counted—the apples started acting weirdly after they were counted as a point. To make sure that doesn’t ruin the experience, we rewrote our logic for counting the score to make it as non distracting as possible (unfortunately there was no way around the actual problem as the sensor would automatically cause the jump to occur).
Collaborating on Unity
When we first started working on A Fine Day, we were limited to work at times where all of us were free making it really unproductive. Later on in the project, we started using Unity Collaborate which allowed us to easily save and sync the project while each of us worked on it in their own timing.
Losing a teammate along the way (not literally losing him he’s fine):
As the rebranding of our team suggests (The ODD Team -> The DO Team), one of our team members left the group, turning our team into a two-member group.
It was difficult to adjust given how ambitious our goals were. However, over time, we managed to figure out a process that worked for us and managed to split the work evenly such that the workload stayed generally manageable. Naturally, we had to scrap some elements from the final product to wrap up in time, but we think it’s safe to say that we delivered in spite of this major setback.



Final Project Report Template [Project Title]
Team Members
 
 
 
1. 	Introduction
 
This section introduces the problem statement and provides background about the project (why this is important, how it can be useful). It can also have the project objectives.
 
 
2. 	Project Development
 
This section describes the various software/devices that are utilized in this project and how the project is built. You may also describe any additional assets used in the project (such as 3D models, images, videos, audio, etc.). Snapshots of various elements/functionalities of the project may also be included here. Explanation of any code you developed should also be included in this section.
 
3. 	Results and Evaluation
 
This section describes how you evaluated the functionalities of this software. Explanations of challenges, errors, and debugging can also be included in this section.
 
4. 	Conclusion and Future work
 
This section presents a summary of the project achievements and a short discussion on potential future developments.
 
5. 	Reflection on Learning
 
This section summarizes the learning outcome of this project (what you have learned as you developed this project).

