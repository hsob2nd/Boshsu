# Profiles, progression and backups

Offline profiles have a permanent internal ID and an editable display name. Scores, XP, achievements, progression, cosmetics and local-world history belong to that ID. Changing the displayed name preserves the profile rather than creating a second account.

Avatars, earned fonts/name effects, Prestige presentation, titles, calling cards and cosmetic loadouts decorate the shell/player identity. Gameplay skins are a separate system. Local rank and PP are labelled local; they are not global server statistics.

## Data locations

Paths below are relative to the extracted client folder:

| Location | Content |
| --- | --- |
| `StableRemasteredAccounts/` | Local profiles, scores, provider/world/progression state |
| `replays/` | Native local replay files and their score associations |
| `MapLab/` | Saved generated difficulties, baked audio and authoring provenance |
| `Songs/` or the configured library | Your beatmaps |
| `Skins/` | Your custom skins |
| `PPProviders/` | Installed optional provider packages |
| `BoshShell.json` and client configuration | Shell/gameplay/display preferences |
| `Logs/`, `performance/`, `assistance-diagnostics/` | Bounded, current-session diagnostic output where enabled |

Keep generated `.osu`, audio and Map Lab metadata together. Native `.ureplay` playback is a separate format from an inherited stable `.osr`; legacy replay conversion is unqualified.

## Backup

Close the client, then back up the player-data folders and your configurations. If your Songs library lives elsewhere, back that up separately. Preserve replay associations and generated source provenance when moving folders.

The clean release contains no developer profile, avatar, score history, configured map library, private skin collection or telemetry. The shared qualified assistance model is intentionally part of the clean runtime; it is not a copied player account.

Do not post backups, profiles, worlds, personal configurations or replay collections in a public GitHub issue. Provide a minimal reproduction and reviewed diagnostics instead.
