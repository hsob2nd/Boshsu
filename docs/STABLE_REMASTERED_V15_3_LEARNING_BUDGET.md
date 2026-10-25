# Boshsu 1.5.3 — 100,000-movement Adaptive learning budget

Continues the sealed 1.5.2 client. Adaptive retains normal Max guidance and optional gradual local movement learning. Max strength remains capped at **100%**. PP formulas, rewards, scores, qualified planner data, Maple, AC, Relax and slider ownership are unchanged.

## What the cap means

The profile now retains a **total effective budget of 100,000 valid learned movements** across its eight directional statistics. This replaces the former limit of 2,048 per direction (16,384 total). A common direction can continue learning beyond 2,048 without waiting for other directions to fill.

These are completed qualifying unassisted movements, not device polls, individual coordinates or saved aim trajectories. The persistent data remains an eight-bin count/curvature summary smaller than 16 KiB plus one bounded backup. No raw cursor archive is added. Detailed session logs keep their existing independent limits and startup retention policy.

## Continued adaptation at capacity

When the next map's observations would exceed the total budget, the existing account worker proportionally reduces older directional influence, then adds every valid new observation. Largest-remainder rounding keeps the total at exactly 100,000 without allowing eight separate 100,000 limits. Each retained direction's mean is preserved while its old weight is reduced.

This is a weighted summary with gradual forgetting, **not an exact chronological queue of the last 100,000 movements**. Learning does not freeze at capacity. A full profile changes more slowly than a small profile; greater sample count is not a guarantee of better aim. The existing explicit Reset action remains available.

The active map still uses a fixed starting profile. New observations contribute to later maps, outside critical gameplay timing. No new aim worker, input owner, per-frame scan, file I/O or training pass is introduced. The map clock, raw-movement requirement, AFK protection, 512-observation directional confidence ramp, shape bounds, target deadlines and endpoints remain as in 1.5.2.

## Compatibility and presentation

Existing version-one learning summaries load unchanged. No reset, invented observations or recalculation of historical scores is performed. The managed validator and native seed boundary accept the same 100,000 total and reject oversized/invalid summaries. The per-attempt limit of 256 qualifying observations per direction remains intact.

Options → Assists → Max Assist → Adaptive shows the current count against 100,000 and explains continued adaptation. Consent remains explicit and local; a fresh clean release leaves learning Off. Selecting Standard or turning learning Off stops personal adaptation. No movement data is uploaded.

An older 1.5.2 executable cannot read expanded summaries that exceed its former directional limit. Keep the matching 1.5.3 managed/native files together; use the verified updater and retain its recovery data when changing versions.

## Qualification

The native production-model suite passed **149,793 checks** with 6,012 hosted-model samples, including empty/full seed acceptance, a 100,001 total rejection and the existing Max/Adaptive equality, clock/device-poll, movement, endpoint and learning tests. The account/provider suite passed **3,567 assertions**, including **87 learning-storage checks**. New cases cover growth beyond the former directional cap, restart at 100,000, proportional ageing, retention of new observations, continued learning in a full single direction, randomized rolling merges, invalid totals, per-attempt limits, corruption recovery, duplicate events, profile isolation and bounded file/backup size.

All eleven portable-host CPU suites passed. Eight real-client synthetic cycles passed 22 pause/resume pairs and three retries, with Standard/Adaptive, 100/50% strength, AC off/on and manual/Relax input. Both opted-in learning cycles used an explicit synthetic **100,000-movement seed through the managed-to-native boundary**. Selected fixture replay checks passed; dense native replay equivalence remains unqualified. The process exited cleanly and the native diagnostic writer drained with no dropped or failed records.

The capped run recorded a weighted CPU-loop mean of **1.000186 ms**, versus 1.000186 ms for the sealed 1.5.2 capped fixture, and a maximum of **5.7315 ms**, versus 5.5764 ms previously. These include simulation-only frames and do not certify displayed FPS, input-to-display latency, human mouse/tablet feel, all hardware or a universal zero-spike guarantee. Private session evidence is excluded from public packages.

Rendered-menu, sealed-package startup/update and protected-installation receipts are recorded in the final release status. Existing broader replay, live server, Linux runtime, device-feel and third-party asset/source provenance limits remain documented. Upstream GPL/MIT/OFL notices are retained.
