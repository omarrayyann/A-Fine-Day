# A Fine Day

A Fine Day is a gamified version of stroke rehabilitation that allows patients to regain their limbs functionality in an engaging environment and therapists to remotely track patient progress. We would like to focus our efforts for this version of the game on developing games that target patients’ range-of-motion and motor skills.

We integrated VR (Oculus Rift) and hand-tracking (Ultraleap) to replicate motor skill and range-of-motion exercises in more engaging way while keeping track of the motion data for the therapist to analyze.

# Mini-Games

**1- Whack-A-Mole (WAM) Minigame**

Works on:
- Hand-eye coordination
- Range-of-Motion
- Speed

![Bitmap](https://user-images.githubusercontent.com/77675540/146438818-12cb6eb8-93eb-482e-b66a-6a3ab8da0ccc.png)

**2- Baskets Minigame**

Works on:
- Hand-eye coordination
- Range-of-Motion
- Speed
- Grasping skills
- Manipulating the environment

![image](https://user-images.githubusercontent.com/77675540/146441239-4361ca89-010b-4f6b-95c2-b823a1920db8.png)

**Velocity-Time & Acceleration-Time Graphs are generated based on the patient hand motion

![image](https://user-images.githubusercontent.com/77675540/146442410-5f27d826-8bf6-4670-8a40-37c37f3e1da4.png)

- **Software/Hardware Tools:**

Devices: Oculus Rift, Ultraleap Hand-Tracking
Software/Development Environment: Unity, Leap Motion
Assets: SDKs (Unity & Oculus Rift), free 3D models from online asset stores (Citations at the end)


Challenges:

- Figuring out which Ultraleap components/commands to use and when since there was very little documentation, was the main issue throughout the start of the project. Once we go that ball rolling everything was generally smooth.
- Refactoring code such that it's modular to allow future changes to the project.
- Targets weren’t responding to collisions from the ultraleap hand. After some research we figured out we had to add the InteractionBehaviour script
- Calibration: When the game starts, the target was always spawning very low compared to the player. After testing, we figured out that the sensor was taking time to calibrate the height. Therefore, we came up with the Calibrate script.
- We spent a lot of time trying to fix a problem with the graphing only to discover that there was a non-commented artifact from old code.
- Whenever a menu would become enabled, the player would accidentally press it because their hands were already there. We came up with the wait script for that where the menus stay unresponsive for a few seconds after spawning.


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

# User Guide:
1- Clone the repositry to your machine

2- Open the folder using Unity

3- Connect the devices (Ultraleap and Oculus Rift)

4- Build and Run the Project

# Devices Needed to Run this Project:
1- Ultraleap Hand Tracker
2- Oculus Rift VR

# Screenshots

![image](https://user-images.githubusercontent.com/77675540/146443162-c4884f3a-ee5a-4895-8f84-e55eeb3143a0.png)

![image](https://user-images.githubusercontent.com/77675540/146442661-8e3043ab-19e0-4799-985c-0eb82c4a330a.png)

Team Members 

- **[Omar Rayyan](https://github.com/omarrayyann)**

- **[Dhiyaa Al Jorf](https://github.com/DoodyShark)**
