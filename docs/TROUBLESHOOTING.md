# Troubleshooting

| Symptom | Check |
| --- | --- |
| No maps in Song Select | The clean package is empty. Configure the correct Songs library and let indexing finish. |
| Missing DLL/startup error | Extract the entire ZIP, keep the resource folders together, and verify the Windows x64/.NET Framework 4.8 requirement. Do not launch from inside the archive. |
| Wrong monitor/window size | Change the display options; later settings are saved. Native fullscreen is the first-launch default rather than a forced reset every time. |
| Skin element falls back | Check the selected skin and its supported filename/element. Missing assets can use the provided fallback. |
| No music/hitsounds | Check the selected output device, master/music/effects volumes and skin sample settings. |
| Assist does not tap | Relax is independent. With Relax off, physical taps are required. |
| Max and Maple will not enable together | This is intentional: choose one cursor assist. AC, Relax and slider controls have separate roles. |
| Max/V1 releases assistance while idle | Movement-only AFK protection expects meaningful mouse/tablet movement; taps alone do not extend the grace. |
| Sliders differ across setups | Check automatic slider-body controls and the separate AC authority option, as well as Relax/manual tapping. |
| Map Lab seems to change Timewarp/CS | Check the generated rated values separately from runtime Timewarp and CS Changer. Generation must not overwrite those runtime preferences. |
| PP differs from another client/server | Check provider identity, world, generated map hash, rated CS, mods and actual runtime rate. Bosh custom PP is not official/server PP. |
| An arbitrary PP ZIP is unsupported | Only validated source families share the bundled adapters. Use a supported package or the documented formula API. |
| Old detailed logs disappeared | Only one application session is retained. Copy reviewed logs before launching again if you need them for a report. |
| Storage still grows with logs Off | Saved scores, replays and generated audio are separate player content. Check those libraries. |
| Online sign-in/submission is unreliable | Live authentication/submission remains unqualified in this candidate. Offline profiles/local PP are the tested path. |

## Filing a useful issue

Include client version, OS, GPU/driver, resolution/window mode, selected skin, map/difficulty identity, mods/rates, cursor assist, AC/Relax and slider settings. Describe the observed behavior and the fewest steps that reproduce it.

For timing/assist issues, distinguish cursor movement, target switching, taps and slider bodies. For PP issues, include provider and rated/generated state. Avoid treating a high total or a miss count as proof of a particular cause.

Do not include passwords, tokens, private account configs or unreviewed full logs. See [CONTRIBUTING.md](../CONTRIBUTING.md).
