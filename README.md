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

## Script overview
- `CameraOrientationController` switches between portrait/landscape camera profiles automatically.
- `PlayerAimShoot` handles drag-to-aim and spawn/launch.
- `ProjectileMotor` uses SphereCast each frame with gravity, drag, and collision responses.
- `ProjectileImpactResponse` overrides collision behavior per target.
