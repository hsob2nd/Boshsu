# Changelog

Development history of **Boshsu**, previously **Bosh / Stable Remastered** and **Ultimate Client**, reconstructed from saved implementation reports, correction reports and sealed checkpoints.

The latest candidate is listed first. The earlier history below runs from the project's origins forward. Dates are included where the records establish them; maintenance passes have not been assigned invented release numbers. Development checkpoints and superseded experiments are distinguished from released features.

## 1.4 RC2 — Max Assist V2, movement and queue fixes (20 October 2026)

- Kept movement-fixed Max Assist as V1 and added an optional V2 under Options → Assists → Max Assist. Existing profiles default to V1.
- V2 retains the qualified planner/target locking and adds bounded movement-relative close-target and slider refinement, diminishing-return gains and shared AC-aware geometry. One cursor owner and one movement budget prevent stacked auto-pull.
- Added per-profile version/refinement sliders, historical V1 defaults, context serialization and safe recovery. Target/head-to-body handoffs clear stale fine guidance.
- Replaced blocked shell-artwork polling with state/capacity/cancellation signals. Measured blocked-queue allocations fell from a median 212,992 bytes per 300 ms to zero; this is not a general FPS claim.
- Passed 95,366 V2 assertions, 38,349 V1 movement assertions, 45 unchanged qualified trajectory traces, 416 real-shell checks and four synthetic gameplay cycles. Matching portable ZIP startup/restart and data-preserving update/rollback passed.
- Kept Maple V1/V3, PP formulas/rewards, qualified model/data and score history unchanged. Physical-device feel, broad hardware, dense replay parity and accepted server submissions retain their documented limits.

See [V1/V2 integration and qualification](docs/STABLE_REMASTERED_V14_MOVEMENT_REPORT.md).

## 1.4 RC1 — Modes, replays, integration and GitHub issue fixes (19 October 2026)

- Added native Flashlight presentation and preserved its mod metadata.
- Bounded Maple V1 corrections by actual physical movement; preserved Max Assist/V3 guidance math and historical implementation revision data.
- Added safe Songs-folder linking and skin-copy import with cancellation, conflict preservation, immediate skin refresh and responsive navigation.
- Reconnected supplied Taiko/Catch/Mania with separate local raw-score/replay storage and explicit Standard ownership handover. Their PP remains unavailable under the owner's PP freeze.
- Added bounded OSR import, native Standard export with custom-settings companions, actual RNG seed recording and replay-only routing. Dense replay equivalence remains an open defect.
- Added Kawata public profiles, plays, map leaderboards, standings, scoped sign-in and explicit server eligibility reasons. Upload remains gated on accepted server rules/identity/replay proof.
- Added OBS scene setup export and verified actual OpenGL shared-texture recording. Other capture vendors' automatic recognition remains unqualified.
- Repaired public CI fixtures to read shipped PP provider ZIPs rather than private development paths. All 2,876 protected PP source files match 1.3 RC1; no historical scores were recalculated.

See [qualification and remaining limits](docs/STABLE_REMASTERED_V14_ISSUE_FIXES.md) and [issue dispositions](docs/GITHUB_ISSUE_WORK_REPORT.md). This release does not claim every issue or device combination is resolved. The intermediate 1.3 RC2 draft was never published; this feature release is Version 1.4.

## 1.3 RC1 — Bounded overhead, source builds and qualification (17 October 2026)

- Preserved gameplay/assist models, map-time ownership, PP/reward rules, progression, profiles and skin-controlled gameplay. No duplicate DT/NC reward was restored.
- Cached exact menu PP selection identity and live PP callbacks; moved task closure allocation behind the due-request gate. Removed measured recurring allocations without changing outputs.
- Reused identical canonical native PP preparation for eligible NM/HD rate-1 contexts. Expanded 216-state golden capture and wider 560-row matrix remain exact.
- Moved optional diagnostic disk scans/writes to one bounded FIFO worker; retained default-Off recording, newest-session retention, hard storage caps and visible write failures/drops. Added frame histograms/resource inventories.
- Documented two foreground comparison pairs. Mean CPU frame work is similar/slightly higher, with rare long frames remaining; no general FPS, memory or zero-spike win is claimed.
- Retained existing PP accuracy/HD edge cases and a captured dense-map replay mismatch reproduced identically on sealed RC6. They are not relabelled as passes or silently retuned.
- Repeated pause, raw input/F9, file-picker and 25 skin/resolution chrome checks. Copied-installation update/rollback preserves protected player files; personal installations are not automatically replaced.
- Repaired supported build/tool discovery, pinned 948 native source inputs and 29 runtime dependencies, preserved generated-file timestamps and rebuilt native/managed output from a fresh Git-free extraction. The qualified model rebuilt byte-identically; clean-machine/Linux/full-host reproducibility remains unqualified.
- Added current architecture/thread/data/build documentation, an explicit 181-section qualification ledger, single staging/source/sealing procedures and read-only release integrity/Windows CPU CI. Retained complete prior history and upstream notices.

See [the engineering report](docs/STABLE_REMASTERED_V13_REPORT.md), [performance evidence](docs/STABLE_REMASTERED_V13_PERFORMANCE.md) and [remaining qualification scope](docs/STABLE_REMASTERED_V13_REQUIREMENTS.md).

## 1.2 RC6 — Pause-menu correctness and session diagnostics (17 October 2026)

- Fixed a reproduced continuation-state bug: after Continue, reopening Pause and sending another continuation click could restart the map. The paused state now rejects continuation and clears stale confirmation.
- Verified frozen map time and score through 116 active-pattern assertions across unassisted, Maple V1, Max Assist and Maple V3 contexts, including AC/Relax combinations, repeated Escape, Continue, tapping, focus regain, retry and exit. A separate 116-assertion lead-in run passed.
- Kept limiter wait time in FPS measurements while excluding it from stall warnings. Changed native-client hitch reports from many queued lines to one compact record with lifecycle/map/GC context.
- Removed per-difficulty start/completion chatter from normal release logs; timeouts remain visible. Capped loggers stop allocating/queuing writes, and background log writes are serialized.
- Preserved 1 MiB normal-log limits, newest-session retention, detailed telemetry Off by default, assist strengths, PP rules and all player data. Manual/in-client updates and prior fixes remain included.
- This does not claim every gameplay hitch has been eliminated. The player's late-session hitches were not captured after the previous logs filled. [Evidence and limits](docs/PAUSE_SPIKE_FIX.md).

## 1.2 RC5 — Windows discovery and manual updates (17 October 2026)

- Added a stable Windows AppUserModelID before UI startup and a Boshsu window name from construction.
- Added per-user App Paths / Applications registration and a matching Start-menu shortcut on packaged-client startup.
- Added Options → Advanced → Windows integration with saved opt-out, status, repair and search access.
- Added a standalone manual-update window: double-click Boshsu.Updater.exe, choose the existing installation, downloaded clean ZIP and release checksums. Verification runs off the UI thread, preserves player data, refuses downgrades/running targets and starts a separate installer copy.
- Preserved foreign registrations and prevented an older copy from removing a newer copy's entries. Source/test hosts skip persistent registration.
- Passed 29 non-GUI Windows registration checks and the existing contract/bridge/shell/updater checks. Automatic AMD/Insights recognition and recording remain unverified; Windows registration is not a recorder-catalogue API.

See [the implementation and research report](docs/WINDOWS_DISCOVERY.md).

## 1.2 RC4 — Windows file picker cursor/focus (17 October 2026)

- Fixed Windows cursor visibility, clipping and fullscreen focus when choosing avatars or importing PP ZIP/JSON files.
- Made file pickers owned by the main window; suspended game input/render ownership during their modal loop and restored it on Select, Cancel or failure.
- Released the system cursor promptly when switching to Explorer/other desktop windows. Display/input preferences and qualified assists/PP are preserved.
- Passed 146 picker checks, real Windows avatar/PP import interactions, 105 menu/gameplay cursor checks and existing contract/layout/updater checks.

See [the correction report](docs/DESKTOP_FILE_PICKER_FIX.md).

## 1.2 RC3 — Song Select skin layers and shared Raw Input (17 October 2026)

- Removed legacy normal/hover footer buttons and other replaced chrome that could peek over the modern Song Select interface at different resolutions.
- Preserved song rows, backgrounds, gameplay skins and intentional footer skin icons.
- Unified menu/pause/gameplay raw cursor ownership, settings, sensitivity and coordinate mapping; initialized raw input before the first map.
- Corrected raw clicks and wheel ownership, duplicate button delivery, focus handoffs and the duplicate wheel subscription.
- Preserved qualified assists, PP rules and user data. Added 25 skin/resolution combinations, 105 cursor/navigation checks and matching source/patches.

See [the correction report](docs/INPUT_UI_FIX.md).

## 1.2 RC2 — Raw Input and F9 hotfix (17 October 2026)

- Fixed native Raw Input falling back to Windows cursor movement; one gameplay cursor/sensitivity owner.
- Corrected absolute-tablet primary/virtual-screen and scaled/letterboxed viewport mapping, valid zero coordinates and button-only packet handling.
- Prevented loading/pause/focus-loss movement from leaking into the next gameplay owner.
- Fixed F9 opening an unsupported legacy login-only menu; repeated presses now preserve normal navigation.
- Added Options → Updates: GitHub release checking, beta/stable preference, in-client verified downloads, cancellation and install/restart after the player clicks Update. Personal data is protected; one recovery version is kept.
- 96 real-client synthetic input/navigation checks, contract/bridge checks and qualified assist-ownership regressions passed. Physical device feel remains a user play test.
- Matched personal/clean binaries, full source snapshot, reviewable patch, tests, release manifest and integrity checksums. Profiles/settings and qualified assists/PP rules preserved.

See [the correction report](docs/RAW_INPUT_FIX.md).

## 1.2 — initial clean GitHub prerelease

- Explicit open-source identification and license-scope documentation, including the separate terms and missing source for bundled prebuilt components.
- Clearly labeled complete portable Windows x64 beta ZIP, alongside corresponding project/native source and integrity checksums.
- Issues-only contribution policy: pull requests, Discussions, wiki editing and individual commit comments disabled; public readability/cloning/forking is separate from write access.
- Clean Windows x64 package with fresh local data and the same qualified assists/features as the personal client.
- Saved Map Lab difficulties group under their source map set and restore from authoring provenance after map processing/database rebuilds.
- Generated difficulty/audio ownership remains distinct; current pitch modes receive separate generated identities.
- Independent Map Flip controls and the existing continuous settings scrolling/layout work.
- First launch uses the Main Menu, primary monitor and native fullscreen; subsequent display changes are saved.
- Qualified strong Max Assist and Maple V1/V3 behavior retained, with separate AC/Relax/slider controls and the established activity protection.
- Existing Bosh rewards and PP-provider identities retained; generated/rated state stays separate from runtime assistance.
- Detailed diagnostics default Off. Recognized previous logs clear at startup; optional recording has hard limits and does not delete player content.
- Obsolete model/host/map-preparation file traces removed without changing per-frame model logic.
- **GitHub publication correction:** removed the legacy embedded Discord error-reporting webhook and automatic crash uploads. Errors use the local capped logger.
- Added setup, feature, assist, Map Lab, PP, profile, storage, troubleshooting, development and release-status documentation.

Live online compatibility, broader platform/hardware qualification and public redistribution licensing remain outstanding. See [release status](docs/RELEASE_STATUS.md).

## Earlier development history — oldest first

### Origins: Bosh and Human Aim research — August–September 2026

- Began with analysis of the supplied Bosh/stable client, its managed gameplay/input path, native dependencies and existing Aim Correction.
- Identified the original AC capture boundary and the distinction between physical mouse/tablet input, the internal gameplay cursor and downstream hit testing.
- Established canonical playfield coordinates, target/timing observations, capture/replay tools and offline movement analysis.
- Preserved original clients, source and historical evidence before experiments. Human Aim became reference evidence for later work rather than a replacement application shell.
- Inventoried the native Bosh PP library separately from aim correction; PP calculation was not part of the cursor model.

### PersonalGolden / MaxAssist foundation — 8–9 September 2026

- Completed the isolated PersonalGolden implementation and research package through its staged model, planner, runtime and packaging gates.
- Extracted compact, versioned movement profiles from preserved captures. Corrected their timing interpretation to sampled real time instead of treating map time as real movement duration.
- Added deterministic movement-family selection, trajectory planning, geometry classification and target-arrival control, with cross-framework comparisons and an offline simulator.
- Centralized Timewarp context and exposed Timewarp, compensated Reading AR, NM/DT followpoint presentation, Song Select BPM and MaxAssist settings through the existing Bosh client.
- Expanded Reading AR to 0.0–12.0 and added fixed-angle map rotation with canonical replay-coordinate conversion.
- Integrated MaxAssist ahead of the existing AC boundary, retaining downstream correction and native hit logic.
- Added bounded asynchronous diagnostics and preservation/package checks. Engineering gates did not substitute for normal physical-device gameplay review.

### Original Bosh personal-client maintenance — September–early October 2026

- Produced separate personal and server-oriented packages sharing general compatibility fixes while retaining their distinct assistance/data policies. Historical distribution-specific strength caps were not universal Boshsu rules.
- Improved settings failure handling, immutable-state reuse, diagnostic flushing, stale correction rejection and consistent slider geometry.
- Continued work on notelock, Relax/button ownership, slider/spinner handling, map rotation, recovery, settings navigation and input/AC handoffs.
- Verified that disabling AC and custom Relax leaves physical buttons/cursor unchanged and avoids unnecessary slider/target work; MaxAssist remained independently selectable.
- Reviewed sessions for malformed data, dropped events, overlapping sliders and late capture boundaries. Findings were preserved rather than treating every miss as an assist defect.
- Paused old personal recording after the session review. Boshsu 1.2's one-session storage policy supersedes those earlier research logging defaults.

### Ultimate Client foundation and source extraction — by 4 October 2026

- Established a pinned McOsu/McEngine gameplay foundation and inventoried the supplied Bosh frontend, personal assistance, PP wrappers, replay formats and online protocol.
- Separated neutral gameplay context/results from Core, Online Compatibility and Personal Assistance implementations.
- Source-integrated the qualified personal movement model through an owned host, preserving its mathematical sources and compact dataset rather than copying old executable patches.
- Migrated Timewarp, Reading AR, followpoints, BPM display, rotation/flips, notelock and native AC/Relax/slider/spinner coordination.
- Added settings/data import, bounded diagnostics, generic local replay/context formats and regression comparisons against the installed reference.
- Initially explored separate Server/Personal targets, then consolidated the active product into **one UltimateClient** at the user's request. A separate server-only product was deferred.
- Implemented strict public API decoding and a cancellable background online worker. Public beatmap, score and ranking reads were exercised; that did not certify login, submission or authoritative awards.

### Ultimate Client's first Bosh/stable frontend — 4 October 2026

- Used the supplied Bosh source as the reference for Main Menu, Song Select, Mods, Options, Pause/Fail, Results and profile presentation.
- Bound frontend controls to existing personal/practice settings and native gameplay instead of introducing another assistance implementation.
- Used selected-skin mod icons and menu/pause/result elements with private reference fallbacks.
- Verified targeted left-click navigation, pause/continue/retry/quit, Options persistence and completed Results handoffs.
- Expanded diagnostics to identify maps, settings, runs and terminal/partial results, and improved online worker cancellation during shutdown.
- Recorded remaining gaps in grouped selection, full settings/skin parity, replay, pause capture observations and resource qualification. This was a migration checkpoint, not a finished stable replacement.

### Stable Remastered: native engine and offline accounts — initial previews through development-26

- Continued the Bosh/stable frontend with McOsu rendering, timing, audio, input and hit objects as the gameplay foundation.
- Removed the separate embedded gameplay application/window model. The frontend became the owner of one process, main window, game thread, render context, message loop, swap and BASS lifecycle.
- Added an explicit gameplay engine boundary for preparing captured context, applying settings/skin/mods, starting, pausing, retrying, stopping and returning results.
- Kept accounts, local PP, score storage and networking outside the gameplay engine. It returns gameplay facts rather than writing profile or server awards itself.
- Added Offline and Server provider boundaries, versioned local profiles, score history, best/recent plays, local ranking and replay associations.
- Located and reused the supplied Bosh PP implementation. Kept local calculated PP distinct from server-authoritative PP, with best-play weighting rather than summing every attempt.
- Made completed maps open offline Results automatically, including play PP, profile changes and green gains/red losses.
- Fixed missing slider bodies by restoring required OpenGL state at the renderer boundary.
- Corrected reverse-arrow direction from the projected slider path, including supported rotation/flip/HR transforms.
- Connected selected gameplay skins and repaired health-bar fill behavior so misses visibly reduce health under the applicable gameplay policy.
- Preserved the established MaxAssist/PersonalGolden, AC, Relax, slider and HP behavior while changing integration ownership.

### Native, display and offline stabilization — through development-36, 4 October 2026

- Unified shell/native physical and logical display transforms, viewport/scissor and pointer conversion.
- Repaired teardown ordering so native resources release before the form/context is destroyed and the host can exit its close loop cleanly.
- Fixed menu key edges, click-coordinate capture, current form/cursor-handler attachment and pause/fail backdrop presentation.
- Made the visible resume marker and accepted click position agree, fixing a boundary marker drawn at the wrong location.
- Moved Estimated PP beneath accuracy and retained selected-skin score/HUD resources.
- Added local replay eligibility, ordered key-edge/clock validation, atomic saves and explicit failure reasons. Playback cannot award another score, PP or XP.
- Added versioned offline XP/Prestige progression and per-play account/progression feedback. Version 0.2 later superseded the early progression structure with its expanded endgame design.

### Version 0.1 correctness and skinning — development-37/38, 5 October 2026

- Corrected Letterboxing being mistaken for physical desktop fill. Window mode and logical presentation became separate responsibilities.
- Repaired HUD pivots for large/full-canvas scorebars while preserving compact-bar anchors and selected-skin artwork.
- Restored Ignore Beatmap Skins/per-map override precedence for Pause/Fail.
- Fixed normal cursor polling ownership and Relax spinner lifetime integration without retuning the qualified movement model.
- Excluded waiting/loading/pause/continue/restart/fail phases from active replay recording.
- Audited the installed Bosh/MaxAssist PP wrapper and documented PP-source limitations rather than inventing an equivalent implementation.
- Reviewed user telemetry, shutdown paths, skin rendering, display modes and performance phases. Remaining replay-clock backsteps were retained as a defect for the next pass.

### Version 0.1 qualification — development-39, 6 October 2026

- Replaced per-frame audio phase-error subtraction with an elapsed-time map clock and bounded frequency correction. Hit objects, input deadlines, snapshots and replay timestamps share the authoritative attempt clock.
- Preserved clock epochs for pause, retry, seek and startup instead of rewriting bad replay timestamps to hide regressions.
- Centralized the explicit local Timewarp reward curve and kept it distinct from server PP. The later 1.0.1 correction removed duplicate DT/NC reward logic while retaining the selected Timewarp curve.
- Qualified native recording/playback against score, judgments, accuracy, combo and completion on tested fixtures.
- Exercised 50 lifecycle cycles with skins, HUD scales, pause/retry/fail/complete/replay and shutdown.
- Retained original player history and isolated QA data. The V0.1 engineering gate passed its recorded scope; full stable parity and public-release certification remained open.

### Version 0.2: progression, worlds and Map Lab — development-40, 6 October 2026

- Expanded progression to 12 normal Prestiges of 50 levels, Master 1–500, Legend 1–100, Ultimate cycles and an indefinitely advancing Complete state.
- Added permanent Legend XP bonuses, exact large-number storage, pinned progression curves and atomic per-attempt rewards.
- Added one-time Top 10 improvement rewards with safeguards against ties, recalculation, replay and displacement farming rewards.
- Separated Local, dated Server Snapshot and local Server Simulation worlds. Simulation does not submit scores or present cached standings as live global rankings.
- Added Map Lab speed/CS/preserve-pitch/shift-pitch generation, source provenance, hashes, deduplication and regeneration from original source.
- Introduced transformed object/difficulty calculation through pinned rosu-pp, shared stars/PP facts and FC PP at current accuracy with zero misses/full combo.
- Added the large achievement catalogue, four assistance tracks, marathon/endurance challenges, searchable collections and earned cosmetics.
- Expanded Results with local PP/FC, Top Plays, XP, achievements, progression transitions and account deltas.

### Version 0.2.1 corrective Map Lab and identity — development-44, 7 October 2026

- Corrected Map Lab to behave as beatmap authoring: generated timings, BPM, duration, sliders/spinners, breaks, audio and base CS belong to the generated difficulty.
- Separated rated map state from runtime CS Changer, Timewarp and assistance. Selecting/generating a variant no longer changes those runtime preferences.
- Allowed generated base CS through 10; the earlier 6.9 balance setting stopped acting as an authoring limit. HR modifies rated difficulty separately from gameplay CS assistance.
- Evaluated generated files directly instead of suppressing difficulty through the source-map baseline/transformed ratio. Baked rate does not also become a runtime Timewarp bonus.
- Corrected live Current PP to use the judged prefix rather than filling unplayed objects as perfect hits. FC remains the full map at current accuracy, zero misses and maximum combo.
- Strengthened cache identity with generated bytes, rated CS, mods, runtime rate and calculator revision.
- Added editable names against immutable profile IDs, preserving scores, PP, XP, worlds, progression and cosmetics on rename.
- Added choose/change/reset avatars, consistently full-square Song Select portraits, default fallbacks, earned Prestige styling and persistent font ownership.
- Repaired a real replay expiry-timing mismatch.
- Tried a freer cursor ownership/blending layer around MaxAssist. It was later superseded by strong-reference restoration and native ownership fixes; it is not the current assist model.

### Strong assist, audio and PP correction — development-45, 8 October 2026

- Restored the intended strong MaxAssist reference behavior and qualified model/settings.
- Fixed intro/break skips seeking audio without re-anchoring the application clock. Approved forward skips preserve the replay attempt and earlier frames.
- Preserved the completed song's natural tail in Results and released it before new preview/map ownership. Quit/fail retain their stop behavior.
- Validated generated PCM audio and expected duration before publishing preserve-pitch or pitch-shift variants.
- Removed runtime CS Changer leakage from ordinary-map stars/PP as well as generated maps.
- Canonicalized NC-only legacy bits for calculation while preserving original score/UI flags.
- Corrected component conversion/accuracy interactions that could reward worse accuracy, and rejected all-miss local PP awards.
- Reviewed existing telemetry without treating harmless recovery paths or old QA failures as current gameplay crashes.

### Final 0.2.x stabilization — development-46, 9 October 2026

- Made custom high-speed eligibility follow actual onset intervals and sustained rhythm density rather than requiring displayed BPM to state equivalent mechanical demand.
- Added section diagnostics for subdivisions, density, spacing, velocity, direction changes, sliders and rhythm transitions. Subdivision labels alone do not multiply PP.
- Kept dense low-spacing streams distinct from sustained high-spacing jumps and reading/technical patterns.
- Fixed FC accuracy inversions with stable SS component relationships rather than sorting/clamping incorrect outputs.
- Added compact 95/97/98/99/100% Mods estimates and PP Details, with background preparation, bounded caches and stale-result rejection.
- Prevented frontend-hosted preparation from starting a competing McOsu preview; unloaded previous music before replacement and coordinated Results/preview release without an artificial loading delay.
- Made live PP disposal nonblocking on the gameplay/UI owner.
- Fixed null-checksum score lookup after import and the Map Lab pitch button updating the wrong caption.
- Improved measured menu loading by draining resource work within a bounded budget while retaining conservative gameplay resource work.
- Controlled rate/CS comparisons did not justify an arbitrary new Map Lab speed multiplier or CS rebalance.

### Version 0.3: interface, identity and accessibility — development-47, 9 October 2026

- Introduced the shared Bosh design system across Main Menu, Song Select, Mods, Options, Map Lab, profiles, customization, achievements, progression, worlds and Results.
- Replaced competing legacy bottom controls with one Song Select footer, a smaller cookie, menu actions and the player card.
- Made panels fit the actual viewport and added shell scaling, motion/quality controls, keyboard navigation and universal search.
- Expanded identity with calling cards, titles, borders, earned name styles, live previews, locked-item explanations and three achievement showcase slots.
- Made Map Lab first-class in Song Select/Mods/context/search, using one generator with quick rates, saved presets, variant navigation and asynchronous PP previews.
- Retained selected-skin mod icons and gameplay skin authority over cursor, circles, sliders, numbers and hitsounds.
- Added bounded hardware-aware quality/performance profiles and background shell preparation that defers during gameplay. Presets do not select another assist strength/model.
- Preserved the qualified native gameplay and PP behavior from stabilized 0.2.x.

### Version 0.3.1: Bosh identity, animation and Results — development-48

- Added a restrained violet/cyan/magenta language, curated vaporwave/cyber materials, moving Main Menu horizon, cookie pulse/rotation and hover/press feedback.
- Animated selected calling cards, earned Prestige emblems and readable name effects using elapsed time. Off/Reduced/Low retain static or simplified presentations.
- Added three named cosmetic loadouts and filters for reward source, rarity and unlocked state. Loadouts do not change gameplay skins or world ledgers.
- Bundled Barlow/Rajdhani with OFL notices and owned font lifetimes, avoiding new shell dependence on installed Windows fonts. Linux execution remained unqualified.
- Modernized the Song Select header/search/group/sort presentation and repaired real group rebuilding.
- Made Local Performance lead with saved local PP and show accuracy/combo/mods/date and the saved play's Timewarp rate.
- Fixed quoted background filenames and stale background decodes; added persistent modern Main Menu Now Playing.
- Modernized tooltip/confirmation/FPS typography and kept compact menu/gameplay FPS presentation consistent.
- Wired Shift+Tab HUD visibility, Quick Retry/Ctrl+R, key overlay, live mouse-button/volume/limiter controls and native settings readback.
- Redesigned Results around played-map backgrounds, aspect-preserved selected-skin grade/FC art, player identity, hero PP/FC and account deltas.
- Added short Top Play, FC, achievement and progression celebrations with immediate navigation and quality/motion limits.
- Added **PP only / PP + achievements / Achievements only / Off** notifications. Removed completion reminders and the redundant red Results-ready popup; progress messages defer outside gameplay/Results.
- Reused one authoritative Assists page, improved search/grouping and modernized keybinding/skin-preview controls.

### Final 0.3.x engineering pass — development-49, 10 October 2026

- Gated native timing/profiler tools behind explicit development opt-in so ordinary shortcuts cannot accidentally activate them.
- Repaired assist callback-rate divergence around unchanged strong mathematics and distinguished render deadlines from gameplay/input observations.
- Corrected Options parent/Back/Escape routes, duplicate page reopening, wheel ownership and custom-scale parsing.
- Added versioned Map Lab preset migration/recovery, stable IDs and persistent usage.
- Fixed duplicate beatmap basenames with full-path identity, safe database recovery and rescan.
- Repaired optional metadata/null paths and startup log-directory ordering.
- Distinguished rendered FPS from update throughput and retained selected-skin spinner digits.
- Ran extended settings, menu, worker-load, replay, resource and viewport checks without claiming all-hardware latency or perfect behavior in unobserved input windows.

### Version 1.0 RC1: release-candidate qualification

- Kept the existing Windows x64 offline Standard scope and marked the candidate **RC1**, not a certified final 1.0.
- Expanded identity editing with swatches, palettes, HSV/hex, earned gradients, animation controls, bounded glows and actual-name font previews.
- Bundled additional OFL display fonts and checked actual runtime families/metrics instead of assuming fallback succeeded.
- Reduced bridge snapshot allocation and moved qualification serialization/file writes/thread enumeration to a bounded writer.
- Added phase/spike/resource diagnostics and labeled unsupported native settings/shortcuts.
- Exercised extended lifecycle, replay outcome, navigation, importer identity and sealed-binary comparisons with isolated data.
- Repaired Options categories accumulating Back history, sidebar destinations piling up unrelated pages and unfocused search reopening the page. Categories replace their page while nested details retain the correct parent.
- Removed Target Practice from normal user access after the accidental activation concern; 1.0.1 continued navigation corrections.
- Recorded remaining normal-play, online acceptance, platform and redistribution gaps rather than declaring them passed.

### Version 1.0.1: initial PP and gameplay consistency

- Made the selected `.osu` file and actual playback rate common rated inputs for ordinary, generated and imported difficulties.
- Kept rated CS separate from gameplay CS Changer and HD Remover visual-only/PP-neutral.
- Unified menu/current/FC/Results preparation, captured policy and cache identity.
- Fixed finite navigation/loading-page replacement, Shift+Tab key-up handling, avatar temporary-path overflow and duplicate lifecycle logging.
- Retained the last complete frame through native mode transitions instead of a legacy blank fade; reduced bridge serialization allocation.

### Version 1.0.1: corrective DT/NC and score-history pass

- Traced the reported near-4Ã— DT/NC contribution to a duplicate nominal DT/NM performance ratio layered over actual-rate difficulty.
- Removed the extra multiplier from menu, Current PP, FC and Results. DT/NC timing difficulty is represented once at actual rate; no fixed replacement reward was invented.
- Retained normal HD/HR effects, NC equivalence and the independent configured Timewarp curve.
- Recalculated eligible personal history under corrected captured map/mod/rate policy at the user's request, preserving results, judgments, timestamps, replay references, profile IDs, earned XP/progression/cosmetics and previous PP correction history.
- Rebuilt chronological profile deltas/accuracy/rank snapshots without manufacturing missing context for legacy records.
- Documented why removing recent inflation did not guarantee a lower profile total: some older awards used different rate-normalization/conversion rules. Bosh local PP remains custom progression PP, not official/server awards.

### Version 1.0.2: Boshsu naming, strong assist and cleanup

- Renamed the client **Boshsu** and added an original embedded application icon.
- Restored the original Bosh observation transaction around strong MaxAssist, retaining lock/jump/reset coverage.
- Removed frequent judgment JSON decoding from the bridge while preserving ordered lifecycle events.
- Added continuous visible scrolling to Options, Assists, dropdowns and keybindings.
- Fixed Timewarp value/tooltip synchronization and displayed actual Map Flip mode names.
- Re-audited saved PP against the shipped calculator. Inspected awards matched; intentional rewards were preserved rather than introducing an arbitrary cap or silently lowering progression.
- Archived obsolete symbols, launchers, backups and diagnostics outside the playable folder while retaining runtime files, maps, generated audio, scores and replays.

### Followpoint and performance hotfix after 1.0.2

- Moved defensive score-history copies and durable writes outside the presentation lock while retaining atomic commits and serialized storage.
- Used lightweight statistics/reward inputs instead of repeatedly copying full rhythm/correction histories; filtered Song Select records before copying.
- Removed forced full garbage collection/finalizer waits on every portable mode change.
- Replaced Followpoint mode's slider with explicit **NM / DT timing** buttons and live persistence/selected-state feedback.
- Applied the same scaled fade window to rendering and previous-object cutoff, preventing early disappearance at faster rates.

### Native assist timing and ownership fixes — 14 October 2026

- Confirmed that performance profiles do not set assist strength; reproduced real target-observation/ownership defects instead of adding a preset multiplier.
- Retained still-hittable late heads on resume/reset and overlapping active sliders through a precomputed lifetime index.
- Recovered newly observed eligible heads without planning toward expired deadlines; removed empty expired-owner transactions.
- Added native slider-head acknowledgment before handing the cursor to the moving body.
- Kept pending heads until native judgment or late-window expiry. Early AC/Relax hits no longer start the next trajectory before its authored deadline.
- Prevented repeated observations at one map millisecond from advancing an unhandled owner merely through planner invocation.
- Routed Slider HP and Spinner HP to separate native gates.
- Fixed queued HUD input recreating a disposed window handle during shutdown and blocked GL initialization after disposal; the corrected candidate then completed its targeted shutdown checks.
- Preserved trajectory math, dataset, intensity and AC/Relax eligibility. No extra input thread, backdated clicks or fabricated observations were added.

### Session stability foundation for 1.1 — 15 October 2026

- Moved replay encoding/durable saves to a bounded storage worker, with immutable submissions, verified completion receipts, shutdown drain and pending-reference recovery.
- Added cancellable/stale-safe background beatmap header/checksum/audio preparation; Play and preview use the same authoritative selection commit.
- Froze terminal score/map time after failure during audio slowdown; Retry establishes a new attempt.
- Improved controlled Results-loading/frame-time measurements while retaining longer-run spikes as documented limitations.

### Version 1.1: native Maple and modular PP

- Integrated supplied Maple V1/V2/V3/VL1 references with map-clock guidance and explicit cursor priority. V2/VL1 were subsequently retired; current builds expose V1 and V3 only.
- Added profile-owned settings and draggable numeric sliders, fine keyboard adjustment, continuous scrolling and persistence in the existing Assists interface.
- Made MaxAssist and Maple mutually exclusive in settings and native publication; AC/Relax retain separate cooperating roles.
- Kept V3 Power/Slider Power at 0–2 and recovered intended reference bounds elsewhere.
- Fixed V3 high-power slider overshoot/oscillation, an invisible overlay intercepting drags, excessive page rebuilds and per-frame property reflection.
- Removed deliberate yielding from active unlimited gameplay while retaining menu/background and capped pacing.
- Disabled the legacy thread-suspending stack sampler in native gameplay after investigating a large stall; passive buffered timing remains.
- Fixed stale online profile refreshes and tightened public-response parsing. Live sign-in/submission qualification stayed deferred.
- Added explicit offline PP provider identity/version/checksum provenance, per-profile/world selection and comparable totals. Import does not automatically equip or revalue history.

### Version 1.1: V1/V3 consolidation and gameplay gamma

- Retired selectable/executable V2 and VL1 without renumbering historical data. A saved retired mode does not silently become another assist.
- Corrected V1 AC-aware guidance geometry and stale offsets after physical movement stopped.
- Briefly tested V1 strength 2, then corrected its final allowed maximum to **1**. V3 retains **2** for both powers; the temporary V1 range is superseded.
- Added gameplay-only gamma 0.50–2.50 with neutral 1.00 bypass, cached render resources and no desktop gamma change. Menus/Results retain their own presentation.

### Version 1.1: Akatsuki and repository ZIP import

- Added single-JSON formulas and bounded, cancellable, atomic ZIP import with traversal/link/duplicate/size validation. Archive scripts and untrusted included binaries are not executed.
- Traced the supplied bancho.py dependency chain and integrated pinned Akatsuki as a real independent calculator, retaining upstream source/notices.
- Passed actual map bytes, mods, rate, combo and judgments to Akatsuki without adding Bosh custom strain/Timewarp reward multipliers.
- Added direct-upstream comparisons and shared live/current/FC/menu ownership.
- Kept Bosh as the default, local PP separate from server awards and different calculator systems separate in comparable profile totals.
- Documented supported pinned repository families instead of claiming arbitrary repository code can calculate PP without a compatible backend.

### Version 1.1: independent sliders and CCV3

- Fixed automatic slider bodies being coupled to AC by the old `tracking = aim && tracking` gate.
- Added separate AC, MaxAssist and per-Maple automatic slider controls, allowing full Max/Maple body tracking with AC off.
- Retained one cursor publication, authored geometry and native head/tick/tail judgments. Body control does not fabricate heads or replace tapping; Relax/physical buttons own input.
- Added the supplied CCV3 ZIP family with MIT source, a shipped trusted module and upstream comparisons. Bosh/Akatsuki remain available.
- Supported repository families import without a user-written adapter; unrelated calculators still require an implemented compatible backend.

### Version 1.1: MaxAssist empty-section ownership

- Released Max control in loading, distant lead-ins and breaks until a native-visible head/body needs assistance.
- Re-engaged from current physical input and rejected stale/invisible/expired outputs.
- Preserved strong movement/strength and native acknowledgment/deadline rules. This ownership repair does not revive the superseded freer/weaker experiment.

### Version 1.1: MaxAssist / Maple V1 activity protection

- Required meaningful raw mouse/tablet displacement to activate Max/V1, with a 1.5-second monotonic real-time movement grace period.
- Excluded taps, assisted movement, automatic sliders, Relax output and judgments from activity credit.
- Released cooperating cursor/AC/Relax/slider state on idle and reset on re-engagement, preserving active strength.
- Cleared credit on loading, focus loss, pause, retry, owner/map changes, rewind and resolution changes.
- Kept Maple V3 and standalone AC/Relax behavior separate. This is a local movement policy, not anti-cheat, proof of human play or a changed submission rule.

### Version 1.2: Map Lab persistence and final integration — 17 October 2026

- Grouped generated variants as extra difficulties under the source map set and rebuilt discovery/grouping from saved provenance after processing, database rebuilds and restart.
- Added source-checksum recovery after folder moves. Missing sources do not delete saved variants or invent official beatmap IDs.
- Kept generated map/audio/hash/score identities separate despite shared visual grouping; generated siblings correctly change preview audio ownership.
- Preserved explicit generated/opened difficulty selection through Song Select reconstruction.
- Added pitch mode to new generated identities so preserve-pitch/shift-pitch variants do not collapse to the same `.osu` hash. Existing variants/history remain untouched.
- Made Map Flip independent and retained continuous scrolling/hitbox improvements.
- Avoided duplicate slider-path sampling at a requested timestamp while preserving V3 equations, Max model/data and judgment/coordinator behavior.
- Retained Bosh/Akatsuki/CCV3 modules, DT/NC-once behavior, separate Timewarp rewards, rated/runtime CS separation and HD Remover neutrality.
- Packaged personal data separately from the fresh-user clean client; first launch uses Main Menu/native fullscreen on the primary monitor, then saves display changes.

### Version 1.2: one-session diagnostic retention — 17 October 2026

- Corrected detailed shell/gameplay diagnostics to default **Off**, including absent older preference fields. Presets do not enable recording; FPS statistics stay bounded in memory.
- Cleared recognized previous diagnostic files once at application startup, retaining one session rather than deleting on every map/retry.
- Added active-folder leasing and safe locked/unavailable cleanup handling. Unrelated files, profiles, scores, replays, maps, skins and settings remain outside deletion.
- Capped optional assistance at 64 MiB total, performance at 16 MiB, Map Lab trace at 1 MiB and each fixed normal/startup-error log at 1 MiB.
- Disabled packaged legacy research recording and removed obsolete host/model/map-preparation traces.
- Verified identical protected production model traces after removing initialization logging. Older standalone oracle assumptions that failed on both baselines remain documented, not relabeled as passes.

### Version 1.2: clean GitHub publication — 17 October 2026

- Published **v1.2.0-rc.1** with clean Windows x64 client, corresponding project/native source archive and verified SHA-256 checksums.
- Added ten client guides plus retained license/provenance files.
- Removed the inherited Discord crash-upload transport and embedded webhook from the clean candidate. Errors use the capped local logger; gameplay math and personal data were not changed.
- Verified clean first launch, saved-display restart, local error handling and uploaded archive digests.
- Kept the repository private; uploading a private prerelease did not establish public visibility or redistribution clearance.
- Expanded this changelog from the original Bosh/PersonalGolden work through the current candidate.

## History and qualification notes

Earlier entries describe their own checkpoint, including behavior later corrected. Current behavior is documented in the [features](docs/FEATURES.md), [assists](docs/ASSISTS.md), [PP systems](docs/PP_SYSTEMS.md) and [release status](docs/RELEASE_STATUS.md) guides.

The history was checked against the original Bosh/PersonalGolden reports, Ultimate migration/UI reports, Stable Remastered native/offline/skin/replay reports, version-specific 0.1–1.2 reports and the later PP, ownership, timing, slider, AFK and storage correction records. Plans and requests alone are not recorded as completed features.

Evidence includes targeted fixtures, native lifecycle runs, desktop-injected interactions and player telemetry reviews. It does not certify every legacy osu! feature, physical device/display, Linux execution, end-to-end latency or zero frame spikes. Live authenticated acceptance/awards and public redistribution clearance remain outstanding. The historical Bosh PP module is prebuilt; its implementation source is unavailable.

Private play records, names, raw logs and credentials are not included. This documentation expansion does not modify client binaries, score history, progression, settings, release checksums or the existing prerelease tag.
