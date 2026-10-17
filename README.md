# Boshsu

**An open-source Bosh / osu!stable-style client project, powered internally by the McOsu gameplay runtime.**

[Open-source project / GPL-3.0](LICENSE) · [GitHub Issues support](https://github.com/hsob2nd/Boshsu/issues)

McOsu-derived code retains GPL-3.0, and original project modifications retain their stated GPL-3.0-or-later license. Corresponding frontend/native source is included with the prerelease. Bundled assets, dependencies and prebuilt modules have separate terms; this is not a claim that every distributed component is open source. See [license scope and exceptions](#open-source-and-license-scope).

Boshsu combines familiar song selection and skin-controlled gameplay with local profiles, custom PP systems, Map Lab, assistance settings, progression and player customization.

**Current client: 1.2 · Windows x64 · release candidate.** Offline Standard gameplay is the tested path. Live online authentication/submission, Linux, broader hardware qualification and public redistribution licensing remain outstanding. See [release status](docs/RELEASE_STATUS.md).

## Download

- **[Portable Windows x64 beta package — Boshsu-1.2-clean.zip](https://github.com/hsob2nd/Boshsu/releases/download/v1.2.0-rc.1/Boshsu-1.2-clean.zip)**
- [Corresponding source snapshot](https://github.com/hsob2nd/Boshsu/releases/download/v1.2.0-rc.1/Boshsu-1.2-source.zip)
- [Checksums](https://github.com/hsob2nd/Boshsu/releases/download/v1.2.0-rc.1/SHA256SUMS.txt)
- [Release notes](CHANGELOG.md)

The beta is a complete portable client package, not just an executable or source download. Extract the **whole client ZIP** into a writable folder and run **Boshsu.exe**. Keep its DLLs and resource folders together. Do not run the executable from inside the ZIP. The application is built for **Windows x64 and .NET Framework 4.8**; a separate compiler or PP calculator installation is not required to play.

The clean package starts with fresh local data. It excludes the developer's profiles, scores, avatars, settings, map library, custom skins, replays and diagnostic history. It intentionally includes the same qualified assistance model and calculator modules as the personal build.

No maps or custom skins are bundled. Choose your Songs library and skin in Options. First launch opens the Main Menu in fullscreen at the primary display's native resolution; later display changes are saved.

## What is included

| Area | What it does |
| --- | --- |
| Familiar shell | Main Menu, Song Select, Mods, Options, profiles, achievements and Results, with the Bosh visual identity |
| Internal gameplay | McOsu rendering, input, timing, hit objects, audio and gameplay lifecycle inside the application's main window |
| Skin support | Gameplay elements remain controlled by the selected osu! skin, with default/missing-element fallbacks |
| Offline profiles | Editable names, avatars, local scores, local PP, best/recent plays and native local replay associations |
| Progression | Achievements, Prestige and later mastery states, calling cards, titles, fonts and cosmetic loadouts |
| Map Lab | Generate saved speed/CS/pitch variants that appear as additional difficulties under the source map set |
| PP systems | Built-in Bosh PP, optional bundled Akatsuki/CCV3 calculators, and single-JSON formula providers |
| Assists | Qualified Max Assist, Maple V1/V3, AC, Relax, runtime CS/reading controls and separate slider options |
| Presentation | Shell scale, motion/quality settings, performance presets, gameplay-only gamma and HUD options |
| Storage control | Detailed telemetry defaults Off; optional diagnostics have hard limits and retain only one application session |

## Start here

1. [Installation and first launch](docs/GETTING_STARTED.md)
2. [Features and menu guide](docs/FEATURES.md)
3. [Assists and input behavior](docs/ASSISTS.md)
4. [Map Lab](docs/MAP_LAB.md)
5. [PP systems and formulas](docs/PP_SYSTEMS.md)
6. [Performance and diagnostics](docs/PERFORMANCE_AND_STORAGE.md)
7. [Profiles, progression and backups](docs/PROFILES_AND_DATA.md)
8. [Troubleshooting](docs/TROUBLESHOOTING.md)
9. [Architecture and source build](docs/DEVELOPMENT.md)
10. [Release status, testing and licensing](docs/RELEASE_STATUS.md)

## Important distinctions

- **Local PP is not server-authoritative PP.** Bosh's custom rewards are a local/private progression model, not official osu! PP.
- **Generated map difficulty is separate from runtime assistance.** Map Lab's baked timing/CS belongs to the new difficulty; runtime CS Changer and Timewarp retain their own settings.
- **Max Assist and Maple are mutually exclusive.** AC, Relax and slider controls have their own roles. See the assistance guide before combining them.
- **High loop FPS is not an end-to-end latency measurement.** Performance profiles do not select a different assistance model or strength. No all-PC, zero-spike or zero-latency promise is made.
- **This is a release candidate, not a finished public-release certification.** Do not assume all modes, devices, online score compatibility or redistribution rights are qualified.

The legacy embedded Discord crash-reporting transport has been removed from this candidate. Error details remain local through the capped session logger; no reporting webhook is embedded in the release source or executable.

## Repository and source

This repository contains the client documentation, release information and notices. The full corresponding project/native-engine source snapshot is supplied as a **Release asset** rather than placing the large ZIP in Git history. See [development](docs/DEVELOPMENT.md) for its contents and current build limitations.

## Open source and license scope

The open-source project code and its corresponding source can be studied and used under the applicable licenses. [McOsu's GPL-3.0 text](LICENSE), original source notices and the GPL-3.0-or-later scope of project modifications are retained. McEngine and supported source-backed calculator components retain their MIT notices; bundled open fonts retain OFL notices.

The historical Bosh PP module is supplied prebuilt and its implementation source is unavailable. Supplied artwork, legacy resource assemblies and audio/helper runtimes have separate or unresolved redistribution terms. Public repository visibility does not remove those limits or make the full binary package a certified entirely open-source distribution. Read [third-party notices](THIRD_PARTY_NOTICES.md) and [release status](docs/RELEASE_STATUS.md).

## Support and repository contributions

Use [GitHub Issues](https://github.com/hsob2nd/Boshsu/issues) for bug reports and focused feature requests. Public submissions are handled through Issues; pull requests, Discussions, wiki editing and commit comments are disabled. Repository writes and releases remain owner-managed. See [the reporting guide](CONTRIBUTING.md).

Public files and release assets remain readable/downloadable, and GitHub permits public repositories to be cloned or forked. Restricting contribution channels does not make public source private.

Unused GitHub Actions are disabled. Never post passwords, tokens, private profiles or unreviewed logs in public issues.
