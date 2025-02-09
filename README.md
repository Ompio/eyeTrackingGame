🎮 Eye-Tracking Game with Tobii & Unity

📌 Project Description

This project is a desktop-based game developed in Unity that utilizes Tobii eye-tracking technology. The gameplay mechanics are based on tracking the player's gaze, allowing interaction through eye movements.

For detailed game mechanics and design, please refer to the PDF document describing the engineering thesis.

🛠 Required Software

Operating System: Windows 10/11

Unity Version: 2021.3 LTS or later

Tobii SDK: Tobii Unity SDK for Desktop

Hardware: Compatible Tobii eye-tracking device (e.g., Tobii Eye Tracker 5)

🚀 Installation & Setup

1️⃣ Install Unity

Download and install Unity Hub.

Install Unity 2021.3 LTS (or newer) via Unity Hub.

2️⃣ Install Tobii SDK

Download Tobii Unity SDK for Desktop.

Import the package into Unity:

Open Unity.

Go to Window → Package Manager.

Click + (top-right corner) → Add package from disk....

Select the downloaded .unitypackage file and click Import.

3️⃣ Configure Unity

Open Edit → Project Settings → Player.

Ensure that Tobii SDK for Desktop is properly integrated.

Add a Tobii Initializer object to your scene:

In Hierarchy, right-click and select GameObject → Tobii Initializer.

Ensure the necessary eye-tracking components are attached.

4️⃣ Connect & Calibrate Tobii Device

Connect your Tobii eye tracker to your PC.

Open the Tobii Experience App and complete the calibration process.

Check if the device is properly detected.

5️⃣ Run the Game

Open Unity and load the project.

Press Play to test the game.

Ensure gaze tracking is functioning as expected.

📜 Additional Information

For more details about the game’s mechanics, structure, and purpose, refer to the PDF document describing the engineering thesis.

If you encounter any issues with Tobii SDK integration, refer to the official Tobii Developer Documentation or check Unity console logs for error messages.

