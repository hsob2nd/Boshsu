# Source, runtime and development evidence

Use the subsystem map in [ARCHITECTURE.md](ARCHITECTURE.md). The working imported project layout remains intact. New current documentation belongs in `docs`; generated files belong under ignored `_build`, `evidence`, `checkpoints` or external `artifacts` directories.

Historical migration/qualification scripts and reports remain preserved for reference. They are not the supported release procedure and must not be run against a live personal installation. In particular, old `development-*` staging or personal-score restoration scripts are not general-purpose update tools.

Shipping runtime packages contain application resources, dependencies, licenses and concise setup/provider/update documentation. They exclude tests, raw telemetry, screenshots, private checkpoints, user data and obsolete per-hotfix reports. Corresponding source and development evidence are separate artifacts.

Do not rewrite Git history, delete sealed checkpoints, modify upstream skin assets or remove a fallback DLL merely because its filename looks old. The supplied `osu.dll`, `osu!ui.dll` and `osu!gameplay.dll` are live fallback art/audio/resource assemblies. Native video/audio helpers must be checked through both managed references and native imports before removal.

The ignore rules exclude build products, user/runtime data and secrets without ignoring application source or owned synthetic fixtures. A privacy scan is required before publishing each source/runtime package.
