# Ultimate Car Simulator — Unity 3D

This is the Unity/C# game project, not the temporary HTML browser prototype.

## Target
- Unity 6 (6000.0 LTS or newer compatible Unity 6 release)
- Desktop first: Windows/Linux
- Third-person driving camera
- WheelCollider-based vehicle physics
- Keyboard driving, reset, pause HUD, parking trigger hooks

## Controls
- W / Up: accelerate
- S / Down: reverse
- A / Left and D / Right: steer
- Space: handbrake
- Left Ctrl: service brake
- R: reset vehicle to configured spawn
- Esc: pause

## Current source modules
- `Assets/Scripts/Vehicle/VehicleController.cs`: vehicle physics and wheel visuals
- `Assets/Scripts/Vehicle/KeyboardVehicleInput.cs`: keyboard adapter
- `Assets/Scripts/Camera/VehicleCamera.cs`: smooth chase camera
- `Assets/Scripts/UI/DrivingHUD.cs`: speed/gear HUD and pause
- `Assets/Scripts/World/ParkingZone.cs`: slow-stop parking completion trigger
- `Assets/Scripts/World/VehicleResetZone.cs`: recovery trigger

## Important
Scripts are committed to GitHub, but a complete Unity scene, licensed 3D vehicle assets, lighting, audio, and a built executable still need to be assembled and tested in the Unity Editor. Do not describe this repository as a finished playable PC build until that build has been generated and tested.

## Setup
See [SETUP.md](SETUP.md).
