# Unity prototype setup

## 1. Create a project
Create a new 3D Unity project on a PC using Unity Hub. This repository currently contains starter scripts, not a complete Unity project or playable executable.

## 2. Import scripts
Copy the `Assets/Scripts` folder from this repository's `UnityProject` folder into your Unity project's `Assets` folder.

## 3. Make a quick test track
1. Add a Plane and scale it to (10, 1, 10).
2. Add a Cube for a car body; scale it to (1.8, 0.7, 4).
3. Add a Rigidbody to the car root. Keep a Box Collider or add child colliders.
4. Add `CarController` to the car root.
5. Place the car slightly above the plane.
6. Add `FollowCamera` to the Main Camera and drag the car root into its Target field.
7. Press Play.

## Controls
- W / Up Arrow: accelerate
- S / Down Arrow: brake and reverse
- A / Left Arrow: steer left
- D / Right Arrow: steer right
- Space: handbrake

## Notes
- This starter controller targets Unity 6's Rigidbody API (`linearVelocity`, `linearDamping`). For older Unity versions, change `linearVelocity` to `velocity` and `linearDamping` to `drag`.
- The controller is an arcade prototype, not realistic wheel/suspension simulation yet.
- To support the old Input Manager, set Active Input Handling to Input Manager (Old) or Both in Player Settings.
