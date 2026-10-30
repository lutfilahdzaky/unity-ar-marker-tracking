# Unity AR T-Shirt

A marker-based augmented reality application built with Unity and Vuforia Engine. The application detects a printed image target (poster) through the device camera and renders a 3D animated character wearing a T-shirt on top of it in real time. Users can change the T-shirt design through on-screen buttons and manipulate the character using pinch-to-scale touch gestures powered by Lean Touch.

## Table of Contents

- [About](#about)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Scene](#scene)
- [Image Target](#image-target)
- [Features](#features)
- [Touch Interaction](#touch-interaction)
- [Build and Deployment](#build-and-deployment)
- [Dependencies](#dependencies)
- [License](#license)

## About

This project serves as a learning reference for building AR applications in Unity using the Vuforia Engine SDK. It demonstrates the following concepts:

- Setting up Vuforia Engine inside a Unity project.
- Creating and configuring an image target database.
- Augmenting a detected marker with a 3D character model and animations.
- Implementing interactive UI buttons to cycle through different T-shirt designs on the character.
- Handling touch input for object manipulation (drag and pinch-to-scale) using Lean Touch.
- Triggering character animations (dancing, idle, etc.) from the UI.

The target platform is Android. No ARFoundation, ARCore XR Plugin, or ARKit XR Plugin packages are used; AR functionality is handled entirely through Vuforia.

## Prerequisites

| Requirement        | Version                      |
| ------------------ | ---------------------------- |
| Unity Editor       | 2022.3.22f1 (LTS)           |
| Vuforia Engine     | 11.4.4                       |
| Android SDK        | API level 29 or higher       |
| Scripting Backend  | IL2CPP                       |
| Target Architecture| ARMv7                        |

A Vuforia developer account is required to obtain a license key. The key must be entered in `Assets/Resources/VuforiaConfiguration.asset` under the Vuforia Configuration inspector before building or running the project. The key is not included in this repository.

## Getting Started

1. Clone this repository and switch to the `tshirt` branch.
   ```bash
   git clone -b tshirt https://github.com/lutfilahdzaky/unity-ar-marker-tracking.git
   ```
2. Open the project root folder in Unity 2022.3.22f1 or a compatible version.
3. Unity will resolve packages automatically. The Vuforia SDK is included as a local tarball at `Packages/com.ptc.vuforia.engine-11.4.4.tgz`.
4. Open `Assets/Scenes/MainScene.unity` to load the application.
5. To test on device, connect an Android phone and build through File > Build Settings.

To test image detection, print the marker image located at `Assets/Editor/Vuforia/ImageTargetTextures/T-Shirt/poster_scaled.jpg` and hold it in front of the camera.

## Project Structure

```
Assets/
  3D/
    Idle.fbx                          # Base character model (rigged humanoid)
    AnimController.controller         # Animator controller for the character
    Animations/                       # Animation clips (Jumping Jacks, Silly Dancing,
                                      #   Tut Hip Hop Dance, Wave Hip Hop Dance, etc.)
    Materials/
      Shirt_1.mat – Shirt_5.mat       # Five different T-shirt materials
      Ch42_Hair.mat                   # Character hair material
    Textures/
      texture_tshirt1.jpg – 5.jpg     # T-shirt texture images
      (character diffuse, normal,     # Character body textures
       specular maps)
  Editor/
    Migration/
      AddVuforiaEnginePackage.cs      # Vuforia package migration utility
    Vuforia/
      ImageTargetTextures/
        T-Shirt/
          poster_scaled.jpg           # Preview of the marker image
  Plugins/
    CW/
      LeanTouch/                      # Touch gesture plugin (drag, pinch)
      LeanCommon/                     # Selection and utility framework
  Resources/
    VuforiaConfiguration.asset        # Vuforia license key and runtime settings
  Scenes/
    MainScene.unity                   # Main AR scene (single scene)
  Scripts/
    Buttons.cs                        # T-shirt style cycling, exit, and reload
    Animation.cs                      # Character animation trigger control
  StreamingAssets/
    Vuforia/
      T-Shirt.xml                     # Image target database descriptor
      T-Shirt.dat                     # Image target database binary
  UI/
    Background_AR_Tshirt.png          # App background image
    close-circle-outline.png          # Exit button icon
    idle-circle-outline.png           # Reset-to-idle button icon
    refresh-circle-outline.png        # Refresh/reload button icon
    timd-shirt-logo.png               # Brand/logo image
Packages/
  manifest.json                       # Unity Package Manager manifest
  com.ptc.vuforia.engine-11.4.4.tgz  # Vuforia Engine SDK
Build/
  release.apk                         # Pre-built Android APK
```

## Scene

The application uses a single scene:

### MainScene

The AR scene contains the following hierarchy:

- **ARCamera** — Vuforia AR Camera that accesses the device camera and performs image recognition.
- **ImageTarget** — Configured to detect the `poster` marker from the T-Shirt database.
  - **Character** — A 3D humanoid model (child of the Image Target) that appears when the marker is detected. Has a `LeanDragTranslate` component and `BoxCollider` for touch interaction.
  - **Canvas (World Space)** — Contains five buttons (Purple, White, Gray, Black, Pink) for changing the T-shirt design.
- **Canvas (Screen Space)** — Overlay UI with branding and navigation controls.
- **LeanTouch** — Touch gesture manager with `LeanTouch` and `LeanTouchSimulator` components.
- **Animation** — GameObject for controlling character animation states.
- **EventSystem** — Unity UI event system for button interactions.

## Image Target

| Property                 | Value                     |
| ------------------------ | ------------------------- |
| Database name            | T-Shirt                   |
| Target name              | poster                    |
| Target type              | Image Target              |
| Physical size            | 0.300 m × 0.424 m        |
| Max simultaneous targets | 1                         |

The database files are located in `Assets/StreamingAssets/Vuforia/`. The marker image preview is at `Assets/Editor/Vuforia/ImageTargetTextures/T-Shirt/poster_scaled.jpg`.

Vuforia is configured with continuous autofocus, automatic device tracker start, and ARCore as a required dependency on Android.

## Features

### T-Shirt Style Changing

The core feature of this application. When the AR marker is detected and the 3D character appears, users can tap the on-screen color buttons to cycle through five different T-shirt designs:

| Button   | Material    | Texture           |
| -------- | ----------- | ----------------- |
| Purple   | Shirt_1.mat | texture_tshirt1   |
| White    | Shirt_2.mat | texture_tshirt2   |
| Gray     | Shirt_3.mat | texture_tshirt3   |
| Black    | Shirt_4.mat | texture_tshirt4   |
| Pink     | Shirt_5.mat | texture_tshirt5   |

The `Buttons.cs` script handles the material swapping by cycling through an array of materials assigned to the character's renderer.

### Character Animations

Users can trigger different animation states on the character through the UI. Available animations include:

- Idle (default)
- Jumping Jacks
- Silly Dancing
- Tut Hip Hop Dance
- Wave Hip Hop Dance
- Offensive Idle

The `Animation.cs` script manages animation triggers through the `AnimController` Animator Controller. A reset-to-idle function is also available.

## Touch Interaction

Object manipulation after marker detection is provided by the Lean Touch plugin (Carlos Wilkes). The following gestures are available:

| Gesture          | Behaviour          | Description                                  |
| ---------------- | ------------------ | -------------------------------------------- |
| One-finger drag  | LeanDragTranslate  | Moves the 3D character along the surface     |
| Two-finger pinch | LeanPinchScale     | Scales the character up or down (zoom in/out)|

These behaviours are attached as components on the 3D character in the MainScene and require no custom code. The pinch-to-scale gesture allows users to make the AR character larger or smaller by pinching with two fingers.

## Build and Deployment

The project is configured for Android builds:

| Setting            | Value                  |
| ------------------ | ---------------------- |
| Scripting backend  | IL2CPP                 |
| Target architecture| ARMv7                  |
| Minimum API level  | 29 (Android 10)        |
| Color space        | Linear                 |
| Input system       | New Input System       |

A pre-built APK is available at `Build/release.apk`.

To build from source:

1. Open File > Build Settings in the Unity Editor.
2. Select the Android platform.
3. Ensure `Assets/Scenes/MainScene.unity` is included in the build.
4. Connect an Android device that supports ARCore.
5. Click Build and Run.

## Dependencies

| Package                     | Version | Purpose                                |
| --------------------------- | ------- | -------------------------------------- |
| com.ptc.vuforia.engine      | 11.4.4  | AR image target detection and tracking |
| com.unity.textmeshpro       | 3.0.6   | UI text rendering                      |
| com.unity.ugui              | 1.0.0   | UI system                              |
| com.unity.timeline          | 1.7.6   | Timeline and animation sequencing      |
| com.unity.visualscripting   | 1.9.2   | Visual scripting                       |

The Lean Touch and Lean Common plugins are included directly in `Assets/Plugins/CW/`.

## License

This project is intended for educational purposes.
