# DA_Game0090

Unity URP 2.5D prototype (Android) with drag-to-aim shooting, a spherecast projectile motor, and an orientation-aware camera.

## Scene setup (single sample scene)
1. Create a new URP 2.5D project and open your sample scene.
2. Create an empty `GameObject` named `GameRoot` and add the scripts from `Assets/Scripts/` to your project.
3. Camera
   - Select `Main Camera`.
   - Add **CameraOrientationController**.
   - Set the **Landscape**/**Portrait** profiles for orthographic size and camera position.
4. Player launcher
   - Create an empty `GameObject` named `Launcher` at `Z = 0` and add **PlayerAimShoot**.
   - Assign the `Aim Camera` to `Main Camera`.
   - Create a child empty `Transform` named `ProjectileSpawn` at the muzzle position and assign it.
5. Projectile prefab
   - Create a `Sphere` prefab named `Projectile`.
   - Add **ProjectileMotor**.
   - Set `Radius` to match the sphere scale and set `Collision Mask` to the layers you want to hit.
   - Drag the prefab into **PlayerAimShoot > Projectile Prefab**.
6. Targets & collisions
   - Create target objects with colliders.
   - (Optional) Add **ProjectileImpactResponse** to choose between `StickIntoTarget`, `Bounce`, or `Pierce`, and set max bounces/penetrations.
7. Input
   - Press and drag to aim on either mouse or touch. Release to fire.
codex/create-unity-urp-2.5d-prototype-for-android-72fn2a
8. HUD layouts
   - Add **UIManager** to a new `UIRoot` object in the scene.
   - Assign `HUD_Landscape` and `HUD_Portrait` prefabs from `Assets/Prefabs/`.
   - (Optional) Set `UI Parent` to a dedicated `Canvas` if you want to keep HUDs under a specific parent.
   - Ensure each prefab has a `SafeAreaRoot` child; the UI manager will resize it to fit the device safe area.
9. Element/status system
   - Add **ElementStatusController** to any target that should receive elemental buildup.
   - Populate **Status Definitions** with `Status_*` assets from `Assets/ScriptableObjects/Statuses/`.
   - Assign **Reaction Database** to `ReactionDatabase_Default` from `Assets/ScriptableObjects/Databases/`.
   - Call `ApplyElement(ElementDefinition element, float buildup)` to add buildup and trigger reactions.
=======
main

## Script overview
- `CameraOrientationController` switches between portrait/landscape camera profiles automatically.
- `PlayerAimShoot` handles drag-to-aim and spawn/launch.
- `ProjectileMotor` uses SphereCast each frame with gravity, drag, and collision responses.
- `ProjectileImpactResponse` overrides collision behavior per target.
codex/create-unity-urp-2.5d-prototype-for-android-72fn2a
- `UIManager` swaps HUD prefabs based on orientation and applies safe area padding.
- `HUDView` exposes references to HUD elements (HP bar, wave counter, pause button, upgrade prompt).
- `ElementDefinition`/`StatusDefinition` define elements and their buildup/decay behavior.
- `ReactionDefinition`/`ReactionDatabase` describe element reaction conditions and results.
- `ElementStatusController` manages buildup, decay, and reaction triggering.

## Example ScriptableObject assets
Located under `Assets/ScriptableObjects/`:
- Elements: `Element_Fire`, `Element_Ice`, `Element_Lightning`, `Element_Poison`, `Element_Water`.
- Statuses: `Status_Fire`, `Status_Ice`, `Status_Lightning`, `Status_Poison`, `Status_Water`.
- Reactions: `Reaction_ChainLightning`, `Reaction_Shatter`, `Reaction_Explode`, `Reaction_SteamBurst`, `Reaction_Overload`, `Reaction_ToxicCloud`.
- Database: `ReactionDatabase_Default`.

## Editor creation steps (custom assets)
1. Right-click in the Project window and choose **Create > DA_Game0090 > Elements > Element Definition**.
2. Set the element name/color, then repeat for each element you need.
3. Right-click and choose **Create > DA_Game0090 > Elements > Status Definition**, assign an element, and tune max buildup/decay values.
4. Right-click and choose **Create > DA_Game0090 > Elements > Reaction Definition**, assign required elements, and select the reaction result.
5. Right-click and choose **Create > DA_Game0090 > Elements > Reaction Database**, then add your reaction assets to the list.
=======
main
