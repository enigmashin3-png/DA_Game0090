# AGENTS.md — Unity project rules (DA_Game0090)

Project target:
- Unity: (fill in your exact version, e.g., 2022.3 LTS)
- Platform: Android (primary), optional portrait/landscape
- Graphics: URP (mobile-friendly), 2.5D gameplay on a 2D plane (Z locked)
- Input: Unity Input System (touch + mouse for editor testing)

Repo hygiene:
- Never commit Library/, Temp/, Logs/, UserSettings/
- Always keep .meta files
- Keep scenes/prefabs in text serialization (Force Text)

Architecture rules:
- Use namespaces: Game.*
- Keep MonoBehaviours small; prefer plain C# classes for logic.
- Combat must be data-driven:
  - Elements, Statuses, Reactions and Upgrades via ScriptableObjects
  - No hardcoded “if fire then explode” in projectile scripts

Physics & aiming:
- Projectile flight uses a deterministic motor (gravity + optional drag).
- Collision uses SphereCast (avoid tunneling).
- Predicted aiming arc uses the SAME model as the projectile motor.
- Baseline prediction stops at first hit. Extra segments (bounce/pierce) can be upgrade-gated.

Validation:
- No compile errors
- Play mode: aim + shoot works with mouse, and touch simulation.
- Performance: no per-frame allocations in aiming/prediction loops.
