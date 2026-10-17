# Version 1.3 source and package structure

17 October 2026 — Version 1.3 RC1 optimization candidate.

The imported working layout is preserved. Current subsystem/ownership/storage/build documentation is in `docs/ARCHITECTURE.md`, `THREADING.md`, `DATA_LAYOUT.md`, `PERFORMANCE.md`, `STRUCTURE.md` and `BUILD_AND_RELEASE.md`. The stale development-44 root README now points to the current build and reports.

Supported entry points: `tools/Build.ps1`, `Test-Release.ps1`, `Test-Accounts.ps1`, `Test-V02.ps1`, `archive_source.py` and `stage_release.py`. Explicit tool paths are supported; default build jobs are bounded at two. Generated files remain under ignored build/evidence/checkpoint/artifact paths. Historical private restoration/staging scripts are retained locally but excluded from corresponding-source distribution.

The native source pin contains 948 files, including required library headers and the three GLEW/FreeType/JPEG source archives. Archive builds verify hashes without needing `.git`; checkout builds additionally verify the pinned upstream commit. Twenty-nine supplied binary dependencies are pinned and preflighted, including native content DLLs. No player data is used to restore them. Reconfiguring unchanged native source preserves all five generated files and the DLL; Ninja reports no work rather than recompiling them.

Fresh Git-free source build passed on this Windows PC using the existing verified compiler and restored pinned runtime inputs. This is not a clean-machine/Linux certification or a claim of byte-identical builds; inherited build timestamps can differ.

Clean runtime staging uses only files from a checksum-verified baseline manifest and the new application outputs. Five obsolete hotfix reports are omitted from fresh runtime packages while history remains in source/repository documentation. Actual asset/dependency DLLs are retained: resource assemblies and FFmpeg support still have live uses. No large size reduction is claimed from unsafe DLL deletion.

The release workflow is installed in the repository from `tools/ci`. The repository uses read-only workflow permissions and an allowlist limited to `actions/checkout@v7` and `actions/setup-python@v7`; public submissions remain Issues-only. A successful hosted run must be verified separately in delivery evidence. Its Linux job checks asset/member hashes, clean data separation and complete pinned source without executing downloads. A dependent Windows job runs portable-host contracts/navigation/update fixtures and source-compiled account/PP-provider fixtures from checksum-verified release inputs. It does not certify OpenGL, physical devices or a complete hosted source build.

McOsu GPL, upstream/dependency and bundled-font notices remain. Supplied asset/runtime redistribution provenance and the historical prebuilt Bosh PP implementation remain unresolved limitations; source availability alone does not clear them.
