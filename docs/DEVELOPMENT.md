# Architecture and corresponding source

Boshsu is one managed shell/window and internal native McOsu gameplay runtime. Read [architecture](ARCHITECTURE.md), [thread ownership](THREADING.md), [data layout](DATA_LAYOUT.md), [performance policy](PERFORMANCE.md) and [build/release instructions](BUILD_AND_RELEASE.md).

Download the matching `Boshsu-1.3-source.zip` and clean client from the same release. Keep its `stable-remastered`, `build` and `qualified-model` folders together. The source archive includes the required pinned native headers/library archives and root Cargo vendor configuration. Player data, credentials, build products, telemetry and private historical restoration scripts are excluded.

From `stable-remastered`, use `tools/Build.ps1 -Runtime <matching-extracted-client> -Jobs 2`. The script discovers installed Windows build tools and restores 29 exact supplied binary dependencies by checksum. Native source archives/compiler are pinned. `tools/Test-Release.ps1`, `Test-Accounts.ps1` and `Test-V02.ps1` run disposable CPU fixtures. Use fresh evidence/output folders. Never run historical migration/restoration helpers against a player's only data copy.

A fresh Git-free source extraction built native/managed outputs on the development Windows PC without prior application object caches. It reused the installed MSBuild and verified compiler toolchain; this is not clean-machine/Linux certification. Rebuilding the qualified model produced its exact sealed SHA256. Host timestamps still differ. The historical Bosh PP module remains prebuilt; its implementation source and redistribution provenance are unavailable.

The hosted workflow checks public package/source integrity and, on Windows, portable-host contracts/navigation/update plus source-compiled account/PP-provider fixtures. Check the actual Actions run before calling it passed. It does not certify full source/OpenGL builds or physical-device feel.

Version 1.3 retains existing PP edge cases and a one-judgment dense-map replay mismatch that also reproduces on sealed RC6. Review the [regression report](STABLE_REMASTERED_V13_REGRESSION.md) and [requirements scope](STABLE_REMASTERED_V13_REQUIREMENTS.md). Do not retune qualified assists or rewards incidentally. GPL/MIT/OFL and dependency notices remain; unresolved supplied-asset/module rights are not certified by source availability.
