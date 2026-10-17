# Boshsu

**A Bosh / osu!stable-style client experience, powered internally by the McOsu gameplay runtime.**

Boshsu combines familiar song selection and skin-controlled gameplay with local profiles, custom PP systems, Map Lab, assistance settings, progression and player customization.

**Current client: 1.2 · Windows x64 · release candidate.** Offline Standard gameplay is the tested path. Live online authentication/submission, Linux, broader hardware qualification and public redistribution licensing remain outstanding. See [release status](docs/RELEASE_STATUS.md).

## Download

- [Clean Windows x64 client](https://github.com/hsob2nd/Boshsu/releases/download/v1.2.0-rc.1/Boshsu-1.2-clean.zip)
- [Corresponding source snapshot](https://github.com/hsob2nd/Boshsu/releases/download/v1.2.0-rc.1/Boshsu-1.2-source.zip)
- [Checksums](https://github.com/hsob2nd/Boshsu/releases/download/v1.2.0-rc.1/SHA256SUMS.txt)
- [Release notes](CHANGELOG.md)

Extract the **whole client ZIP** into a writable folder and run **Boshsu.exe**. Keep its DLLs and resource folders together. Do not run the executable from inside the ZIP. The application is built for **Windows x64 and .NET Framework 4.8**; a separate compiler or PP calculator installation is not required to play.

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

McOsu GPL and McEngine MIT notices are retained. Third-party components have their own terms; the root GPL text is not a blanket license for every supplied asset/runtime. Read [THIRD_PARTY_NOTICES.md](THIRD_PARTY_NOTICES.md) and [release status](docs/RELEASE_STATUS.md).

For bugs, see [CONTRIBUTING.md](CONTRIBUTING.md). Never post passwords, tokens, private profiles or unreviewed logs in public issues.
