# VR Museum of Missing Sculptures of India

A Cost-Effective Virtual Reality Heritage Preservation Platform Using Unity and Google Cardboard

---

## 🎬 Project Demo Video
Watch the full walkthrough of the virtual museum experience:

https://github.com/manikandan171/VR_Museum_Missing_Sculptures/assets/assets/user-attachments/media/example-video-id
*(Note: Replace with your repository media URL or relative link below)*

<video src="Images/Screen Recording 2025-11-25 103650.mp4" controls width="100%"></video>

> [!TIP]
> If the video does not render in your local markdown viewer, you can directly play it at [Images/Screen Recording 2025-11-25 103650.mp4](file:///d:/hack-it-spaiens-2.0-main/Images/Screen%20Recording%202025-11-25%20103650.mp4) or check out the secondary video [Images/1121(1).mp4](file:///d:/hack-it-spaiens-2.0-main/Images/1121(1).mp4).

---

## 🖼️ Gallery & Screenshots

### 🏛️ The Main Museum Lobby (`SampleScene`)
Explore the virtual gallery hub and gaze at the "Explore" panels to enter exhibit rooms.
````carousel
![Lobby - Nataraja (Dancing Shiva) Exhibit](Images/Screenshot%20(13).png)
<!-- slide -->
![Lobby - Sultanganj Buddha Exhibit](Images/Screenshot%20(14).png)
<!-- slide -->
![Lobby - Koh-i-Noor Crown Exhibit](Images/Screenshot%20(15).png)
````

### 🗿 Interactive Exhibit Galleries
Once inside, users can trigger audio descriptions in multiple languages and read historical panels.
````carousel
![Nataraja Exhibit Room - Language & Audio Controls](Images/Screenshot%20(10).png)
<!-- slide -->
![Koh-i-Noor Room - Historical Archives](Images/Screenshot%20(11).png)
<!-- slide -->
![Koh-i-Noor Room - Detailed Crown Close-up](Images/Screenshot%20(12).png)
````

---

## 📝 Overview

The **VR Museum of Missing Sculptures of India** is an immersive virtual reality application developed using the **Unity Engine** and designed to run on Google Cardboard-compatible VR headsets. The project aims to digitally preserve, document, and showcase India's missing, stolen, damaged, or lost sculptures through an interactive virtual museum experience.

By leveraging affordable smartphone-based VR technology, this project makes cultural heritage accessible to a wider audience while promoting awareness about the importance of protecting historical artifacts and sculptures. Users can explore a virtual museum environment, view detailed 3D reconstructions of missing sculptures, and learn about their historical, cultural, and artistic significance.

---

## 🎯 Project Objectives

- **Preserve cultural heritage**: Save information about missing and lost Indian sculptures through digital visualization.
- **Provide immersive learning**: Deliver a high-quality educational experience using Virtual Reality.
- **Raise awareness**: Increase public awareness regarding cultural heritage theft and artifact trafficking.
- **Democratic accessibility**: Enable students, researchers, historians, and the general public to explore historical artifacts remotely.
- **Cost-effective deployment**: Deliver an affordable VR solution using Google Cardboard and existing consumer smartphones.

---

## ✨ Features

- **🏛️ Virtual Museum Environment**: Realistic museum architecture designed in Unity, featuring multiple exhibition halls and galleries.
- **🗿 3D Sculpture Exhibits**: Detailed 3D reconstructions of missing Indian sculptures (e.g., Nataraja, Sultanganj Buddha, Koh-i-Noor Crown) with high-quality textures and lighting.
- **🎧 Immersive VR Experience**: Stereoscopic rendering with responsive head-tracking through smartphone sensors for full 360-degree exploration.
- **📖 Educational Content**: Historical background of each sculpture, details regarding its origin, dynasty, period, and its disappearance/theft details.
- **🔊 Multilingual Audio Guides**: Gaze-triggerable audio guides with support for **English**, **Hindi (हिंदी)**, and **Tamil (தமிழ்)**.
- **📱 Cost-Effective Deployment**: Runs on standard Android smartphones using Google Cardboard, removing the barrier of expensive VR hardware.
- **🎮 User-Friendly Navigation**: A complete gaze-based interaction system and locomotion scheme that is highly accessible to first-time VR users.
- **🎙️ Offline Speech Recognition (VoiceAI)**: Integration of a native Android speech-to-text plugin built on `whisper.cpp` and `llama.cpp` for voice-based interactions.

---

## ❓ Problem Statement

India possesses one of the world's richest collections of cultural and artistic heritage. Unfortunately, numerous sculptures and artifacts have been lost, stolen, illegally trafficked, or remain missing from temples, archaeological sites, and museums.

Physical access to information about these missing treasures is limited. This project addresses that challenge by creating a virtual museum where users can explore digital representations of missing sculptures and understand their historical significance through an immersive VR experience.

---

## 🛠️ Codebase & Architecture

The project consists of three main components:

### 1. Unity VR Museum Application
- **Locomotion ([VRLookWalk.cs](file:///d:/hack-it-spaiens-2.0-main/Assets/VRLookWalk.cs))**: Walk around by tilting your head downward (below $30^\circ$ by default) to move in the direction you are facing.
- **Scene Transition ([GazeSceneLoader.cs](file:///d:/hack-it-spaiens-2.0-main/Assets/Scenes/GazeSceneLoader.cs))**: Transition from the lobby to exhibit halls and exit doors back to the lobby by looking at the "Explore" or "Exit" signs.
- **Audio Guides ([GazeLanguageButton.cs](file:///d:/hack-it-spaiens-2.0-main/Assets/Scenes/scean2/GazeLanguageButton.cs) & [LanguageAudioPlayer.cs](file:///d:/hack-it-spaiens-2.0-main/Assets/Scenes/scean2/LanguageAudioPlayer.cs))**: Select your preferred language (Tamil, Hindi, English) and play/pause/replay the historical explanation by gazing at the panels.
- **Video Presentations ([videoButton.cs](file:///d:/hack-it-spaiens-2.0-main/Assets/Video/videoButton.cs) & [LanguageVideoPlayer.cs](file:///d:/hack-it-spaiens-2.0-main/Assets/Video/LanguageVideoPlayer.cs))**: Gaze controls for playing, pausing, and replaying background documentaries on the main walls.

### 2. Native Speech Recognition Plugin (VoiceAI)
- **Native Android Plugins ([Assets/Assets/Plugins/Android/](file:///d:/hack-it-spaiens-2.0-main/Assets/Assets/Plugins/Android/))**: C++ wrapper files compiled for Android (`arm64-v8a` and `armeabi-v7a`) integrating `whisper.cpp` and `llama.cpp` for native speech recognition.
- Uses `ggml-base.bin` located in `StreamingAssets` for high-performance offline transcription on devices.

### 3. Voice-to-Text companion App ([vtt/](file:///d:/hack-it-spaiens-2.0-main/vtt/))
- A cross-platform **.NET MAUI** voice-to-text converter application supporting Windows, Android, and iOS.
- Offers recording capabilities, real-time transcription, and clipboard text sharing.

---

## 🚀 Setup & Installation

### Prerequisites
- **Unity Editor**: `2019.4.3f1`
- **Build Target**: Android (API level 21+)
- **Hardware**: Android Smartphone + Google Cardboard Headset
- **SDKs**: Google VR SDK for Unity (integrated in project packages)

### Building the Unity App
1. Clone the repository:
   ```bash
   git clone https://github.com/manikandan171/VR_Museum_Missing_Sculptures.git
   ```
2. Open the project in **Unity Hub** using version `2019.4.3f1`.
3. Open the main lobby scene at `Assets/Scenes/SampleScene.unity`.
4. Ensure the build platform is set to **Android** under `File > Build Settings`.
5. Run the native build script for the Android VoiceAI plugin (if modifying speech features):
   ```cmd
   cd Assets\Assets\Plugins\Android
   CHECK_AND_BUILD.bat
   ```
6. Build the APK and install it on your smartphone.

### Building the MAUI Companion App (`vtt`)
1. Open the project folder `vtt` in Visual Studio 2022.
2. Restore NuGet dependencies and run for your target OS:
   ```bash
   dotnet restore
   dotnet run --framework net8.0-android
   ```

---

## 📜 License
This project is open-source and licensed under the **MIT License**.
