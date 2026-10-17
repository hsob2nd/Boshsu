# Third-party notices

This repository derives from [McOsu](https://github.com/McKay42/McOsu),
copyright PG/McKay and upstream contributors. Its GPL-3.0 LICENSE and original
source notices are retained. New Ultimate modifications are licensed GPL-3.0-or-later.
Upstream code is not claimed as newly authored Ultimate code.

The pinned [McEngine](https://github.com/McKay42/McEngine) is an external MIT
dependency, copyright (c) 2011–2024 PG. Its license is reproduced in
`licenses/McEngine-MIT.txt`. Obtain its exact corresponding source with
the remote and commit in `upstream-lock.json`. `../tools/Build-Engine.ps1`
verifies the existing external checkout. A clean-machine bootstrap is still pending.

| Dependency/scope | Evidence | Distribution treatment |
|---|---|---|
| BASS / BASS FX | Pinned engine headers/import libraries and runtime DLLs; Un4seen Developments and BASS FX authors | Proprietary/add-on terms. [BASS licensing](https://www.un4seen.com/bass.html) distinguishes non-commercial and commercial use. GPL linking/distribution compatibility is not established by free-use permission. Public packaging stays blocked until resolved or a compatible source audio backend is validated. |
| FreeType | Rebuilt x64 from the upstream bundled source archive, hash in ../evidence/NATIVE_DEPENDENCIES.json | Retain upstream license/notice and satisfy the chosen licensing option; distribution audit remains pending. |
| GLEW/OpenGL extension headers | Rebuilt x64 from the upstream bundled source archive; hash in ../evidence/NATIVE_DEPENDENCIES.json and header copyright blocks | Retain all contributor notices and license texts; distribution audit remains pending. |
| JPEG/stb/miniz/LZMA/other embedded utilities | Pinned engine/src/Util and libraries/libjpeg | Original source headers are retained in the engine dependency; inventory exact used files and full notices before a source/binary release. |
| MinGW GCC/runtime | Pinned WinLibs x86_64 POSIX/SEH/UCRT GCC 16.2.0 toolchain; archive hash in upstream-lock.json and ../evidence/NATIVE_DEPENDENCIES.json | Build tooling and copied runtime DLLs. Runtime distribution must retain applicable licenses and runtime exceptions/source obligations. |
| Windows system libraries | Windows SDK/system imports | System dependencies; not copied into packages. |
| nlohmann JSON 3.12.0 | Official source header; release SHA-256 verified in evidence/JSON_DEPENDENCY.json | MIT license retained in third_party/nlohmann/LICENSE.MIT; header included for the public server API decoder. |
| rosu-pp 4.0.1 and pinned Rust dependencies | `transformed-pp/Cargo.lock`, `rust-toolchain.toml`, complete offline `vendor` source and upstream licenses | MIT rosu-pp difficulty/performance implementation adapted through an original C ABI. Dependency licenses remain with each vendored package. Rust 1.90 build tooling is private project tooling, not an installed system requirement. |
| Supplied osu!trainer / SoundStretch helper | Read-only inspection of the user-supplied trainer 1.8.0; `../evidence/v02/SUPPLIED_TRAINER_IL.txt`; copied helper hash in private preview receipt | Original Map Lab transformation code. Supplied SoundStretch executable is used as a private audio helper; it is not treated as newly authored code. The trainer updater/client process/memory reader is not used. Resolve exact helper source/license and redistribution terms before public packaging. |
| Optional engine SDKs | Steamworks, OpenVR, Bullet, ENet, curl, SDL and other folders | Not a blanket license grant. Disabled features must not cause their unnecessary SDKs or binaries to be included in runtime packages. |
| Local McOsu fonts/materials/shaders/models | Installed Steam app 607260, build 24968027 | Copied only for isolated development smoke. No Steam executable used. Asset redistribution permissions and font/skin credits must be established before packaging. |
| Existing Bosh/Golden code and datasets | User workspace reference implementations and import receipts | Supplied source, compiled Personal model, private qualified movement dataset, configuration and selected skin are copied into isolated private previews. Old executables/IL patchers are not imported. Preserve provenance/notices and resolve distribution scope before release. |
| Stable default skin images/samples | Supplied Bosh osu!gameplay/Resources and osu!ui/Resources, per-file hashes in preview/STABLE_SKIN_ASSETS.json | Used for native default and missing-element fallback in private previews. Source asset notices/distribution scope remain part of the release audit. |

This is an initial evidence inventory, not a completed third-party compliance
certification. Public binary/source packaging is gated on a full exact-dependency
and asset audit, corresponding-source completeness and license compatibility.
