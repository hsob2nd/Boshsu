# Boshsu 1.4 RC1 — GitHub issue fixes

This feature candidate continues sealed 1.3 RC1. The owner designated it Version 1.4 before publication; the intermediate 1.3 RC2 staging was never published. It does not change PP formulas, rewards or stored player scores. It does not install over a running personal client. Clean/source packages exclude private profiles, maps, custom skins, scores, replays, credentials and telemetry.

Implemented: native Flashlight presentation and mod metadata; Maple V1 correction bounded by actual physical movement; safe Songs-folder linking and skin-copy import; supplied Taiko/Catch/Mania routing, local raw scores and replay archive; bounded standard OSR exchange with native/custom-settings companions; Kawata public API/provider/account presentation and a fail-closed submission eligibility framework; scoped account transport; OBS capture setup export and responsive recording pages. Max Assist/V3 guidance math is preserved.

Validation before packaging includes 20,315 Maple assertions, 37 Flashlight assertions, 109 additional-mode storage/routing checks, 33 actual supplied-mode lifecycle/replay/handover checks, 24 folder filesystem checks, 54 real-shell folder checks, 37 new OSR exchange checks, 26 Kawata provider/policy checks, live public Kawata requests and actual OBS OpenGL capture. The responsive replay/Kawata pages initially passed 36 checks; the final expanded menu and package results are recorded separately below when completed. Synthetic no-input fixtures do not certify physical tablet/keyboard gameplay.

Remaining: full dense native replay parity, actual Kawata sign-in and accepted score/replay proof, verified server assist limits, vendor-specific automatic detection, wider physical-device/mode/skin qualification, Linux and broader hardware qualification. Existing PP edge cases remain unchanged under the owner's explicit PP freeze. Issues 3/7 and launch troubleshooting 9/10 were excluded by the owner. Issues are not closed merely because a partial implementation exists.

See docs/REPLAY_EXCHANGE.md, docs/ADDITIONAL_MODES.md, docs/KAWATA_INTEGRATION.md, docs/CAPTURE_SETUP.md and GITHUB_ISSUE_WORK_REPORT.md for scope and limits. Earlier sealed reports remain historical baseline evidence.

## Final candidate checks

- All ten portable-host CPU suites passed: 28,542 assertions. The separate account/provider run passed 3,422 assertions, including 37 OSR exchange and 26 Kawata checks. Public CI execution remains separate evidence.
- 2,876 protected PP source files match published RC1 byte for byte. No saved scores were recalculated. Qualified model binaries/data were retained.
- Final replay/Kawata/recording menu navigation: 54 real-client assertions passed across 720p, 4:3 and 900p at 100/150% logical scale. Physical input is not certified by programmatic control invocation.
- Native V4 recording: 17 format assertions passed and three actual recordings retained their captured map seeds. Two real-client cycles passed six pause/resume pairs and a retry. A 217-hit play replayed with identical counters, combo and score.
- That actual 217-hit play was then exported to OSR plus companions, imported with byte-identical canonical input and replayed through the real client again. Counters/combo/score matched and no extra history or PP was awarded. Export metadata excluded profile IDs, source paths and PP-provider identity.
- OBS 32.2.2 OpenGL shared-texture capture succeeded on the qualification RX6600. The recording output 1,112 frames with zero encoding skips and one OBS rendering stall. This limited run is not universal vendor/encoder or zero-spike certification.
- Implemented Kawata public profile/standings/best/recent/map leaderboard requests passed against the live server without credentials or submission. Authentication acceptance and score upload remain explicitly unqualified.
- The version 1.4 managed build succeeded. All ten portable-host suites were rerun against the complete 1.4 runtime and passed 28,542 assertions. Executable metadata, updater identity and shell branding use v1.4.0-rc.1; the protocol build remains unchanged.
