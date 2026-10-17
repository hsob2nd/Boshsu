# Boshsu architecture

The desktop product is one Windows x64 process, window, render context and application loop. `frontend/Bosh/osu!win` builds the managed host, delivered as `Boshsu.exe`. `application/EngineGameMode` is implemented in `application/McOsuGameMode.cs`; it is an application state, not a child executable.

| Source location | Responsibility |
|---|---|
| `frontend/Bosh` | Supplied stable frontend, input/window framework, skin/resource fallback assemblies |
| `application/Shell` | Bosh menus, identity, customization, Results, quality and navigation |
| `contracts` | Gameplay, account, PP, progression, policy, display and presentation contracts |
| `engine` | Main-window McOsu adapter, native ABI, audio borrowing, diagnostics |
| `engine/mcosu` | Supplied McOsu gameplay and native personal input/coordinator integration |
| `personal` | Preserved managed PersonalGolden/Max Assist bridge and qualified model support |
| `application/Accounts` | Local persistence, progression orchestration, Map Lab and Bosh PP composition |
| `application/Performance` | PP provider discovery/import and calculator implementations |
| `online` | Public API provider, Bosh session/protocol and submission boundaries |
| `tools`, `tests` | Build, packaging and isolated qualification; never player data |
| `docs` | Current engineering documentation |

The externally pinned McEngine dependency remains under `build/ultimate-migration/upstream/McEngine/McEngine` relative to the workspace. Moving the imported frontend/native trees merely for naming would break many project, include and resource references. Version 1.3 keeps those working locations and documents the boundaries.

The shell captures a `StableGameplayContext`, the engine runs the map, and `StableResult` returns to shell-owned results/persistence/providers. Networking, awards and profile identity do not belong to the native gameplay engine. Local calculated PP and server-authoritative PP remain distinct types. Map Lab authored data stays separate from runtime CS Changer and Timewarp.

Textures/fonts and GL disposal belong to the render owner. Shell images use leases, background decoding and owner-thread upload. The native adapter borrows the application's surface/audio lifecycle; it does not start a second application loop. [Thread ownership](THREADING.md) and [storage ownership](DATA_LAYOUT.md) describe the constraints.
