# Ultimate Car Simulator

A PC-first car driving and parking simulator prototype built with Unity and C#.

## Prototype roadmap
1. Vehicle movement and steering
2. Follow camera
3. Simple test scene and parking area
4. Parking objectives and UI
5. Vehicle tuning, cockpit camera, and later multiplayer

## Getting started
- Install Unity Hub and a supported Unity LTS version.
- Create a 3D (URP optional) Unity project.
- Copy the scripts in `UnityProject/Assets/Scripts` into the project's `Assets/Scripts` folder.
- Create a car GameObject with a Rigidbody and colliders, then add `CarController`.
- Add `FollowCamera` to the camera and assign the car as its target.

This repository starts as a code prototype; it does not yet include Unity scenes, models, textures, or a playable build.

## Controls
- W / Up Arrow: accelerate
- S / Down Arrow: brake/reverse
- A / Left Arrow: steer left
- D / Right Arrow: steer right
- Space: handbrake
