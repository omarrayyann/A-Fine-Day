# A Fine Day

A Fine Day is a gamified version of stroke rehabilitation that allows patients to regain their limbs functionality in an engaging environment and therapists to remotely track patient progress. We would like to focus our efforts for this version of the game on developing games that target patients’ range-of-motion and motor skills.

We integrated VR (Oculus Rift) and hand-tracking (Ultraleap) to replicate motor skill and range-of-motion exercises in more engaging way while keeping track of the motion data for the therapist to analyze

# Whack-A-Mole (WAM) Minigame

Works on:
- Hand-eye coordination
- Range-of-Motion
- Speed

![Bitmap](https://user-images.githubusercontent.com/77675540/146438818-12cb6eb8-93eb-482e-b66a-6a3ab8da0ccc.png)








**Software/Hardware Tools:**

Devices: Oculus Rift, Ultraleap Hand-Tracking
Software/Development Environment: Unity, Leap Motion
Assets: SDKs (Unity & Oculus Rift), free 3D models from online asset stores (Citations at the end)


Challenges:

- Figuring out which Ultraleap components/commands to use and when there was very little documentation.
- Calibration: When the game starts, the target was always spawning very low compared to the player. After testing, we figured out that the sensor was taking time to calibrate the height. Therefore, we came up with the Calibrate script.
- We spent a lot of time trying to fix a problem with the graphing 

Conclusion and Future Development

We were able to achieve most of our goals for this project. Starting from just setting up the environment, to creating the interaction skeletons, to creating the menus and graphings functionality, to integrating all of the functionalities together, and to finally designing the environment with beautiful assets, “A Fine Day” has gone a long way.

Ever since the beginning we kept the same goal in mind, regardless of the changes we made along the way: providing a fun and motivating environment for patients to work on their motor skills while logging their progress for the therapist to see.

Looking to the future, one can see a lot of potential for this project to grow, making it more useful for both the player and therapist.

**Future Development:**

- It would be extremely beneficial if the game generated the levels according to the user's past progress, personalizing the experience and making sure no player is lagging behind or finding the minigames extremely easy. We can see this being implemented in our solution by integrating the Levels and DataManager scripts into a LevelSpawner script that analyzes past player progress and creates progressively more difficult levels .
- Adding a “Show Past Progress” button to the main menu. We had that in mind while designing the menu scripts and even managed to get the GraphingMenu script to work in the minigames, but we didn’t have time to implement the last step of adding it to the main menu. The tools are already there though and with little work they can be added to the current solution. Additionally, we were planning on adding the axes to the current graphs but didn’t have the time to do so. A future version of this project should implement this fix.
- Adding a cartoon companion (e.g. robot or animal) to give motivational quotes to the player and explain the rules to them. This companion would provide great company for the user as they are playing such that they never feel alone or bored.
- Creating an online community with leaderboards, weekly challenges, online multiplayer functionality, etc. That would ensure the games never get boring and the patients never feel lonely while playing the games.
- Improving on the graphics: while the current graphics look generally beautiful, more can be done like adding animations and sounds to make the background elements come to life. 

**Reflection on Learning**

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


