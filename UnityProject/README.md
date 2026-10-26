# Ultimate Car Simulator — Unity 3D

Main game direction: Unity 6 + C#, desktop PC first. This repository is a growing source project; it is not yet a finished, tested executable.

## Gameplay systems committed
- WheelCollider car physics, keyboard controls, reset and chase camera
- Speed/gear HUD and pause controls
- Parking zone, recovery zone and spawn point
- Waypoint traffic AI and reusable waypoint path
- Mission objective hooks and local player progression (credits, completed missions, best speed)
- Game manager for pause, restart, menu and mission completion

## Controls
- W / Up: accelerate
- S / Down: reverse
- A / Left and D / Right: steer
- Space: handbrake
- Left Ctrl: service brake
- R: reset to configured spawn
- Esc: pause

## Important status
Scripts are in GitHub, but the project still needs a Unity scene, 3D vehicle/environment assets, configured prefabs, audio, lighting, UI wiring, and editor/build testing. Those steps cannot be truthfully marked complete until assembled and tested in Unity Editor.

See [SETUP.md](SETUP.md) for scene assembly notes.
