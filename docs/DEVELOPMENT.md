# Architecture and source

## Ownership

Boshsu's managed Bosh/stable shell owns startup, navigation, settings, accounts, profiles, Results, PP/provider work and application shutdown. The native McOsu subsystem borrows the application's window, render context and audio lifecycle. It does not launch a separate gameplay application or own a second message/swap loop.

The explicit gameplay contract prepares a map/settings/skin/policy context and receives snapshots, judgments and a captured result. Providers decide what to save or calculate afterward. The engine does not own server login, rankings, account storage or authoritative PP.

Gameplay/assist decisions use gameplay time; background work uses bounded/coalesced workers and stale-request handling. Performance profiles primarily tune shell/preparation budgets. OS/input scheduling still exists and no universal latency guarantee is implied.

## Source archive

Download `Boshsu-1.2-source.zip` from the same Release as the client. It contains the supplied frontend, application/contracts/providers, modified McOsu sources, McEngine source at the expected layout, managed qualified-model source, calculator adapters, tests, build tools and retained notices. The qualified movement dataset is supplied for the authorized shared model.

Personal accounts, score history, configurations, telemetry and private replay collections are excluded. The former embedded error-reporting webhook is removed before this GitHub publication. The historical Bosh PP calculator remains a supplied prebuilt module; its implementation source is unavailable and redistribution certification is outstanding.

## Build limitations

The snapshot is **not yet a qualified one-command clean-machine build**. Existing scripts retain workspace/toolchain assumptions and dependency bootstrap work remains. Read `SOURCE_README.txt`, `stable-remastered/CMakeLists.txt`, the project files and pinned manifests before adapting paths.

The current development setup uses Windows x64, .NET Framework 4.8/MSBuild, CMake/Ninja/MinGW and pinned native dependencies. Rust-based calculators have their own locked toolchains/dependencies. The model assembly has a source-owned build helper under `engine/tools/build_personal_model.py`.

Do not claim Linux or a new compiler/platform is qualified merely because a build succeeds. Preserve GPL/MIT/OFL and dependency notices; confirm the redistribution status of supplied binaries/assets before public repackaging.

## Validation

The snapshot includes contract, shell/layout, account/PP, native ownership, replay/storage, assist and Map Lab fixtures. Run the checks relevant to a change using disposable test folders. Never run automatic qualification against a player's only copy of their data.

The old standalone movement oracle contains superseded unacknowledged-head assumptions and fails on both original/current production wrappers. Current acknowledged native ownership fixtures and direct before/after production traces are the relevant evidence for the logging-only model change; do not relabel the legacy failures as passes.

Do not retune the qualified Max/Maple behavior or PP rewards as an incidental UI/performance change. A change should identify its actual map/input/account owner and include a reproducible failing case. See [CONTRIBUTING.md](../CONTRIBUTING.md).
