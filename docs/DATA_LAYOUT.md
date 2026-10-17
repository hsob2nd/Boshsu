# Portable data layout and backup

Application files are identified by `BOSHSU_FILES.json`; the updater replaces only verified application-owned files. Player data is preserved independently. Version 1.3 retains the existing layout and schema rather than renaming authored/history paths.

| Location relative to the client | Ownership |
|---|---|
| `StableRemasteredAccounts/profiles.json` and backups | Immutable profile IDs, editable identity and selected settings |
| `StableRemasteredAccounts/scores` | Individual versioned scores, historical local PP, progression associations |
| Other `StableRemasteredAccounts` content | Avatars, local worlds, snapshot/simulation data; inspect exact types before clearing |
| `MapLab`, `MapLabPresets.json` | Authored generated maps/audio/metadata and presets; never cache |
| `PPProviders` | User-selected PP systems and imported repositories; never cache |
| `replays` | Local replay recordings associated with scores; never cache |
| `Songs`, `Skins` or configured external libraries | Player content; updates do not rewrite it |
| `Cache` | Derived legacy cache; identify a cache writer before deleting unfamiliar content |
| `Logs` | Bounded normal logs and application-exit evidence |
| `performance`, `assistance-diagnostics` | Explicit optional diagnostic evidence, current launch only |
| `Updates` | Download/staging/recovery data owned by the updater |
| `BoshShell.json`, `osu!.*.cfg`, `V02Balance.json` | Existing portable settings; retained where the current client expects them |

Server snapshots may support local simulation and score provenance. Do not classify the entire account directory as regenerable server cache. Historical PP awards and authored Map Lab maps must survive cache operations.

Close the client before backing up. Copy the complete portable folder, including settings, account records, Map Lab, custom providers and replays. External song/skin libraries require their own backup. Verify hashes and test recovery with a copy, never the live account. Files containing credentials or personal history belong outside public source/runtime packages.

Normal logs have a 1 MiB per-file cap. Optional performance evidence has a 16 MiB folder cap; assistance evidence has its existing 64 MiB limit. Recognized previous-session files are removed on the next launch, including the diagnostic writer status. No recursive cleanup follows links into player content.
