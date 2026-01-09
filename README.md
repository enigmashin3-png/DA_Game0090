# DA_Game0090

Unity URP 2.5D prototype (Android) with drag-to-aim shooting, a SphereCast-based projectile motor, and an orientation-aware camera (landscape + optional portrait).

## Scene setup (single sample scene)

1. **Project**
   - Create/open a URP project and open your sample scene.
   - Ensure gameplay happens on a 2D plane (objects at `Z = 0`).

2. **GameRoot**
   - Create an empty `GameObject` named `GameRoot`.
   - Import/keep the scripts under `Assets/Scripts/`.

3. **Camera**
   - Select `Main Camera`.
   - Add **CameraOrientationController**.
   - Assign **Landscape** and **Portrait** camera profiles (orthographic size + position).

4. **Player launcher**
   - Create an empty `GameObject` named `Launcher` at `Z = 0`.
   - Add **PlayerAimShoot**.
   - Assign:
     - `Aim Camera` → `Main Camera`
     - `Projectile Prefab` → your projectile prefab
   - Create a child empty `Transform` named `ProjectileSpawn` (muzzle position) and assign it.

5. **Trajectory predictor (aim arc)**
   - Add **TrajectoryPredictor** to `Launcher`.
   - Create a child `LineRenderer` (use a faint material/low width).
   - Assign the `LineRenderer` to **TrajectoryPredictor**.
   - Assign **TrajectoryPredictor** in **PlayerAimShoot** (so it updates while aiming).

6. **Projectile prefab**
   - Create a `Sphere` prefab named `Projectile`.
   - Add **ProjectileMotor**.
   - Set:
     - `Radius` to match the sphere scale
     - `Collision Mask` to the layers you want to hit
     - gravity/drag to taste
   - Drag the prefab into **PlayerAimShoot > Projectile Prefab**.

7. **Targets & collisions**
   - Create target objects with colliders.
   - (Optional) Add **ProjectileImpactResponse** to targets to override collision behavior:
     - `StickIntoTarget`, `Bounce`, or `Pierce`
     - set max bounces/penetrations if applicable

8. **Input**
   - Press and drag to aim (mouse or touch). Release to fire.

## UI setup (optional)

1. Create a `UIRoot` object in the scene.
2. Add **UIManager**.
3. Assign:
   - `HUD_Landscape` and `HUD_Portrait` prefabs (from `Assets/Prefabs/`)
   - (Optional) `UI Parent` to a dedicated `Canvas`
4. Ensure each HUD prefab contains a `SafeAreaRoot` child; `UIManager` will apply safe-area padding.

## Element/status system (optional)

1. Add **ElementStatusController** to any target that should receive elemental buildup.
2. Populate **Status Definitions** with `Status_*` assets from `Assets/ScriptableObjects/Statuses/`.
3. Assign **Reaction Database** to `ReactionDatabase_Default` from `Assets/ScriptableObjects/Databases/`.
4. Use:
   - `ApplyElement(ElementDefinition element, float buildup)` to add buildup and trigger reactions.

## Script overview

- `CameraOrientationController` — switches between portrait/landscape camera profiles automatically.
- `PlayerAimShoot` — handles drag-to-aim and spawn/launch.
- `ProjectileMotor` — SphereCast-based flight with gravity, drag, and collision responses.
- `TrajectoryPredictor` — LineRenderer-based aim arc using the same ballistic model as `ProjectileMotor`.
- `ProjectileImpactResponse` — per-target collision behavior overrides (stick/bounce/pierce).
- `UIManager` — swaps HUD prefabs based on orientation and applies safe-area padding.
- `HUDView` — references to HUD elements (HP bar, wave counter, pause, upgrade prompt).
- `ElementDefinition` / `StatusDefinition` — elements and status buildup/decay settings.
- `ReactionDefinition` / `ReactionDatabase` — reaction conditions and results.
- `ElementStatusController` — manages buildup, decay, and triggers reactions.

## Example ScriptableObject assets

Located under `Assets/ScriptableObjects/`:

- Elements: `Element_Fire`, `Element_Ice`, `Element_Lightning`, `Element_Poison`, `Element_Water`
- Statuses: `Status_Fire`, `Status_Ice`, `Status_Lightning`, `Status_Poison`, `Status_Water`
- Reactions: `Reaction_ChainLightning`, `Reaction_Shatter`, `Reaction_Explode`, `Reaction_SteamBurst`, `Reaction_Overload`, `Reaction_ToxicCloud`
- Database: `ReactionDatabase_Default`

## Editor creation steps (custom assets)

1. Right-click in the Project window → **Create > DA_Game0090 > Elements > Element Definition**
2. Set element properties, repeat for each element.
3. Right-click → **Create > DA_Game0090 > Elements > Status Definition**, assign an element, tune buildup/decay.
4. Right-click → **Create > DA_Game0090 > Elements > Reaction Definition**, assign required statuses/elements and outputs.
5. Right-click → **Create > DA_Game0090 > Elements > Reaction Database**, add your reaction assets to the list.
