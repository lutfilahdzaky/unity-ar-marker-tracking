# Unity AR Marker Tracking

A marker-based augmented reality application built with Unity and Vuforia Engine. The application detects a printed image target through the device camera and renders a 3D model on top of it in real time. Touch gestures allow the user to manipulate the augmented object directly on screen.

## Table of Contents

- [About](#about)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Project Structure](#project-structure)
- [Scenes](#scenes)
- [Image Target](#image-target)
- [Touch Interaction](#touch-interaction)
- [Build and Deployment](#build-and-deployment)
- [Dependencies](#dependencies)
- [License](#license)

## About

This project serves as a learning reference for building AR applications in Unity using the Vuforia Engine SDK. It demonstrates the following concepts:

- Setting up Vuforia Engine inside a Unity project.
- Creating and configuring an image target database.
- Augmenting a detected marker with a 3D model and animations.
- Handling touch input for object manipulation (drag, pinch-to-scale, twist-to-rotate).
- Navigating between a home screen and an AR camera view.

The target platform is Android. No ARFoundation, ARCore XR Plugin, or ARKit XR Plugin packages are used; AR functionality is handled entirely through Vuforia.

## Prerequisites

| Requirement | Version |
| --- | --- |
| Unity Editor | 6000.0.85f1 (Unity 6) |
| Vuforia Engine | 11.4.4 |
| Android SDK | API level 29 or higher |
| Scripting Backend | IL2CPP |
| Target Architecture | ARM64 |

A Vuforia developer account is required to obtain a license key. The key must be entered in `Assets/Resources/VuforiaConfiguration.asset` under the Vuforia Configuration inspector before building or running the project. The key is not included in this repository.

## Getting Started

1. Clone this repository.
2. Open the project root folder in Unity 6000.0.85f1 or a compatible version.
3. Unity will resolve packages automatically. The Vuforia SDK is included as a local tarball at `Packages/com.ptc.vuforia.engine-11.4.4.tgz`.
4. Open `Assets/Scenes/HomeScene.unity` to start from the main menu, or open `Assets/Scenes/CameraScene.unity` to go directly to the AR view.
5. To test on device, connect an Android phone and build through File > Build Settings.

To test image detection, print the marker image located at `Assets/Editor/Vuforia/ImageTargetTextures/TIMD/logo_tekkom_scaled.jpg` and hold it in front of the camera.

## Project Structure

```
Assets/
  Animation/
    LogoController.controller        # Animator controller for the 3D logo
  Editor/
    Migration/
      AddVuforiaEnginePackage.cs      # Vuforia package migration utility
    Vuforia/
      ImageTargetTextures/
        TIMD/
          logo_tekkom_scaled.jpg      # Preview of the marker image
  Plugins/
    CW/
      LeanTouch/                      # Touch gesture plugin (drag, pinch, twist)
      LeanCommon/                     # Selection and utility framework
      Shared/Common/                  # Shared input and editor utilities
  Resources/
    LOGO_TEKKOM.fbx                   # 3D model rendered on the marker
    VuforiaConfiguration.asset        # Vuforia license key and runtime settings
    house-solid.png                   # Home button icon
    rotate-solid.png                  # Rotation button icon
  Scenes/
    HomeScene.unity                   # Main menu (build index 0)
    CameraScene.unity                 # AR camera view (build index 1)
  Scripts/
    SceneManager.cs                   # Scene loading and application exit
  Settings/
    Mobile_RPAsset.asset              # URP settings for Android
    PC_RPAsset.asset                  # URP settings for desktop testing
  StreamingAssets/
    Vuforia/
      TIMD.xml                        # Image target database descriptor
      TIMD.dat                        # Image target database binary
Packages/
  manifest.json                       # Unity Package Manager manifest
  com.ptc.vuforia.engine-11.4.4.tgz  # Vuforia Engine SDK
ProjectSettings/
  ProjectSettings.asset               # Unity project configuration
```

Only one custom script exists in the project. All AR tracking is configured through Vuforia components in the Unity Inspector, and all gesture handling is configured through Lean Touch components. No additional scripting is needed for the core AR workflow.

## Scenes

The application contains two scenes included in the build:

### HomeScene (Build Index 0)

The landing screen of the application. It provides navigation buttons to enter the AR camera view or exit the application. Scene transitions are handled by the `SceneManager` component.

### CameraScene (Build Index 1)

The AR scene. It contains the following hierarchy:

- A Vuforia AR Camera that accesses the device camera and performs image recognition.
- An Image Target configured to detect the `logo_tekkom` marker from the TIMD database.
- The `LOGO_TEKKOM.fbx` model placed as a child of the Image Target, so it appears when the marker is detected.
- An Animator Controller (`LogoController`) attached to the model.
- Lean Touch components attached to the model for gesture-based interaction.

## Image Target

| Property | Value |
| --- | --- |
| Database name | TIMD |
| Target name | logo_tekkom |
| Target type | Image Target |
| Physical size | 0.250 m x 0.271 m |
| Max simultaneous targets | 1 |

The database files are located in `Assets/StreamingAssets/Vuforia/`. The marker image preview is at `Assets/Editor/Vuforia/ImageTargetTextures/TIMD/logo_tekkom_scaled.jpg`.

Vuforia is configured with continuous autofocus, automatic device tracker start, and ARCore as a required dependency on Android.

## Touch Interaction

Object manipulation after marker detection is provided by the Lean Touch plugin (Carlos Wilkes). The following gestures are available:

| Gesture | Behaviour | Description |
| --- | --- | --- |
| One-finger drag | LeanDragTranslate | Moves the 3D model along the surface plane |
| Two-finger pinch | LeanPinchScale | Scales the 3D model up or down |
| Two-finger twist | LeanTwistRotateAxis | Rotates the 3D model around a specified axis |

These behaviours are attached as components on the 3D model in the CameraScene and require no custom code.

## Build and Deployment

The project is configured for Android builds:

| Setting | Value |
| --- | --- |
| Scripting backend | IL2CPP |
| Target architecture | ARM64 |
| Graphics APIs | Vulkan, OpenGLES3 |
| Minimum API level | 29 (Android 10) |
| Render pipeline | Universal Render Pipeline 17.0.4 |
| Color space | Linear |

To build:

1. Open File > Build Settings in the Unity Editor.
2. Select the Android platform.
3. Connect a device or configure an emulator that supports ARCore.
4. Click Build and Run.

## Dependencies

| Package | Version | Purpose |
| --- | --- | --- |
| com.ptc.vuforia.engine | 11.4.4 | AR image target detection and tracking |
| com.unity.render-pipelines.universal | 17.0.4 | Rendering pipeline |
| com.unity.inputsystem | 1.20.0 | Input handling |
| com.unity.ugui | 2.0.0 | UI system |
| com.unity.timeline | 1.8.13 | Timeline and animation sequencing |
| com.unity.ai.navigation | 2.0.15 | AI navigation |
| com.unity.visualscripting | 1.9.12 | Visual scripting |

The Lean Touch, Lean Common, and CW Shared Common plugins are included directly in `Assets/Plugins/CW/`.

## License

This project is intended for educational purposes.
