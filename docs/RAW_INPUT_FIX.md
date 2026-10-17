> RC3 supersedes the raw cursor ownership described in the RC2 report. See [the current correction report](INPUT_UI_FIX.md).

# Boshsu 1.2 RC2 — Raw Input and F9 correction

Raw Input now uses the shell's real cursor-visibility state inside the native engine. Previously the borrowed environment retained the standalone engine's always-visible flag, selecting the Windows-pointer fallback instead of the raw relative path. The shell now observes the pointer during native gameplay; it does not also apply sensitivity or warp it.

Absolute tablet coordinates use the primary screen unless the packet specifies the virtual desktop. Both shell and native paths convert physical coordinates through the presentation viewport once. Absolute (0,0) is a valid sample. Button/wheel-only packets cannot reset a tablet into relative mode. Invalid Windows raw packets are rejected. Loading, paused and unfocused raw deltas cannot carry into a new gameplay owner.

F9 (and the related F8 chat path) no longer opens the legacy Bancho login-only Options state. That overlay is unavailable in the provider-based client. Repeated F9 leaves Main, Song Select, Options, gameplay, pause and return navigation usable.

The input path remains on the application's existing owner thread, consumes device packets once, and uses no frame-rate strength multiplier or new input worker. Cursor-visibility changes use a cached, allocation-free native call. Max/Maple/AC/Relax behavior and PP rewards were not retuned.

## Validation

- Managed and native Release builds passed; existing unused-field/reference warnings remain.
- Real-client fixture: 96 assertions across five cycles. Relative packet aggregation/reversal; sensitivity 1/1.5/2; absolute window/primary/virtual-desktop mapping; valid (0,0); zero-motion button packets; 720p/4:3/16:10; 2x presentation scale and centred viewport; pause, focus gating, retry, map exit/reentry; Raw Input Off; repeated F9.
- 9,736 gameplay/display contract assertions and 2,322 fixed native/managed bridge checks passed.
- Current production Max cursor-ownership, Max+AC and movement-only AFK fixtures passed. Qualified model and movement dataset are unchanged.
- Desktop-injected F9 left the rendered Main Menu usable on the left monitor. Synthetic packets exercised the production application/native boundary. Physical mouse/tablet feel and hardware latency still require a normal user play session; these tests do not certify every device.

Only application/native binaries and package metadata are updated in the personal installation. Profiles, scores, replays, maps, skins, progression and settings are preserved. Diagnostics remain Off by default with existing bounded single-session retention. RC1 remains available; RC2 supplies a matching clean client, source snapshot, patch, tests and checksums on GitHub. Existing license/platform/online qualification limits remain as documented for 1.2.

## In-client GitHub updates

Options → Updates (also universal search) checks this repository for compatible stable/beta releases. Startup checks and beta eligibility are optional. GitHub asset SHA-256, package identity, per-file hashes and allowed paths are verified before installation. Downloading and verification run outside gameplay; installation waits for the application to close, replaces application-owned files, restarts Boshsu and retains one prior application version. Profiles, scores, replays, maps, skins, configuration and custom PP providers cannot be supplied as update entries. Disk space is checked before staging/backups.

The final client also passed two update-page/live GitHub assertions (98 total runtime assertions). The installer passed 55 release-selection, checksum, metadata, path protection, user-data preservation, injected-write-failure rollback and bounded-recovery checks. These are explicit fixtures; they do not certify every Windows filesystem/device.

## Published package and full update-path check

Published `v1.2.0-rc.2` at https://github.com/hsob2nd/Boshsu/releases/tag/v1.2.0-rc.2, from documentation/patch commit `b905b4964db37faefc01a39e4634206d78a070fe`. GitHub SHA-256 digests match both archives and the checksum file.

After publication, the real client downloaded that GitHub asset, verified it, waited for its owner process to close, installed it and restarted into the normal Main Menu. This used a disposable same-release reinstall fixture; normal update discovery still rejects same-version offers and downgrades. Five protected fixture files remained identical, installed application/native/updater/model hashes matched the clean candidate, one recovery version remained and no staging directory remained.

The personal installation matches those application/native/updater/model binaries. All 138 checked personal data/settings files remained identical. Detailed current-session diagnostics remain bounded and Off by default. Physical device feel remains the user play-session check.
