# Boshsu 1.5.1 — Max Assist integration and qualification

Continues the sealed 1.4 build. The former optional Max V2 is now **Max Assist**. Maple V1/V3 remain separate choices. PP formulas, rewards, imported calculators and historical scores are unchanged. Patch releases use `1.5.1`, `1.5.2`, etc.; earlier historical RC tags remain in the changelog.

## Correctness fix and strength

The previous V2 adapter could retain a fine-guidance offset as the qualified coarse route reached its endpoint. The combination could pass the target. A controlled curved approach reproduced 7.936 canonical units of endpoint error. Limiting only the additional refinement to the route's remaining endpoint distance reduced that case to 0.0935004 units at the original maximum. The qualified planner, target selector and model data were not retuned.

A second deterministic test showed that repeated render polls between physical device packets could release correction and weaken guidance. Max now advances its raw-motion anchor on a changed device observation, retaining the published offset through a 64 ms physical-device gap, converted through the authoritative playback rate, before a time-based fade. It never adds pull while the device is stationary. A resumed packet spends only its actual new movement; lead-ins/breaks/focus/policy resets remain immediate. The same held-packet fix prevents the new learner from undercounting raw movements. Tests compare 2–32 ms device packets with extra 1 ms polls, across refinement, AC and extended-strength combinations. This fixes polling ownership rather than tuning the planner.

Strength now uses a clear normalized scale. **100% is the previous V2 maximum**. A missing new setting preserves the previous intensity, including values below that maximum. An optional 101–125% range increases the composed guidance and the single physical-movement budget; it never passes an unsupported intensity above 0.50 into the qualified model. There is no new assistance worker or performance-profile-dependent gain.

Four controlled raw paths compared 100%, 110% and 125%. All four had lower endpoint error at the higher settings after the correction. This is a bounded engineering comparison, not proof that every player benefits from the highest setting. The default remains 100%.

| Controlled path | 100% endpoint error | 110% | 125% |
| --- | ---: | ---: | ---: |
| Curved approach | 0.0935 | 0.0859 | 0.0744 |
| Wider short approach | 50 | 45 | 37.5 |
| Overshooting raw approach | 3.8749 | 2.9774 | 1.6649 |
| Short physical movement | 85 | 76.5 | 63.75 |

Coordinates are canonical playfield units. These paths intentionally include insufficient physical movement; stronger guidance is not an acquisition guarantee.

## Settings and cleanup

Options → Assists → Max Assist is the authoritative page, also reachable through search. The old Max V1/V2 selector and the duplicate generic Max controls are removed from normal navigation. The native V1 publication branch and redundant runtime adapter instance are removed; historical serialized V1 contexts and its regression fixtures remain readable.

Primary controls are strength, target refinement and slider mode. Guided slider refinement appears only for Guided mode. Automatic mode keeps the established 100% native slider-body ownership, including AC off. AC's own automatic slider setting remains separate.

Advanced planner controls preserve path style, directness, endpoint precision, arrival timing and input matching. A qualified-model sensitivity test measured both requested trajectories and final V2 cursor output. The first four changed final guidance in the tested paths. Input matching is conditional on usable movement evidence at planning/handoff; its lack of effect in a first-note test is not evidence that it is a universal strength control. Existing values are retained.

Max and Maple remain mutually exclusive. AC and Relax keep their independent roles. With Relax off, physical tapping is required. Max keeps the meaningful-movement AFK gate; taps alone do not count. Empty lead-ins, breaks, pause, focus loss and invalid/stale target state release guidance through the established native lifetime owner.

## Adaptive Assist (experimental) and local learning

**Adaptive Assist** follows the qualified model's full requested route through the same native target owner, V3 refinement and single physical-movement budget. It does not introduce another selector, an OS cursor injector or a background aim thread. The standard mode retains its original partial composition.

Learning defaults **Off** and requires the explicit explanatory opt-in. The warning explains that personal adaptation needs raw mouse/tablet cursor telemetry captured before assistance, meaningful directed movement and enough samples; it starts untrained and improvement is not guaranteed. With learning Off, Adaptive Assist still follows the planned route but does not personally adapt. Unavailable diagnostic storage suspends collection. It records additional bounded local session diagnostics while active. It learns a small, interpretable directional curvature profile from raw canonical mouse/tablet movement captured before Max, AC and Relax. It does not train on corrected cursor output, replay input, stationary jitter, slider bodies or blocked AFK activity. It does not change target timing, hit windows, judgments or PP.

The learner time-weights observations on the map clock, uses eight direction bins, requires sufficiently long directed movements and caps contributions per run. Learned shape blends in with confidence, is limited to a small fraction of chord length and has zero displacement at the route's start and endpoint. This is statistical adaptation, not a claim of neural-network training or a fully learned personal aim model.

Each immutable local profile owns one versioned aggregate smaller than 16 KiB, plus one backup. At most 32 recent attempt IDs prevent duplicate terminal-event merges. Storage and merging run on the shell account worker after an attempt, not in the gameplay loop. Fixed-size native arrays accumulate samples without file access or per-frame serialization. Turning learning off stops collection; Reset removes that profile's aggregate and backup. Profile names, scores and PP history are unaffected.

Normal diagnostics still default Off, retain one application launch and keep their existing hard limits. Persistent learning aggregates are separate from session logs. They are local and are not uploaded.

## Aim-style presets

Ten optional starting points are available under **Aim style**: Classic Max, Direct, Flow, Balanced, Precision, Wide Jumps, Streams, Tech, Raw Intent and Profile Rhythm. Each describes its route preference. These use the existing supported style/directness/precision/arrival/input-matching controls; they do not create new targeting or hit logic. Applying one preserves strength, target refinement, slider mode, AC, Relax, rate, selected mode and learning consent. Advanced changes show Custom. Presets apply only on explicit selection; no existing style is silently replaced. Names suggest a route character, not guaranteed performance for a map category.

## Test scope

Production-model strength/learning tests passed 59,232 checks with 6,012 hosted model samples. Tests include equal map-time paths across 60–4000 callbacks/s and four map rates, no extra same-clock gain, finite output, consent, stationary/jitter exclusion, bounded sample counts, repeat-finalization, callback-independent learning and exact endpoint preservation.

All 45 original qualified trajectory files / 190,859 samples remain byte-identical. Existing Max authority, AC, Relax, Maple, automatic slider and AFK suites pass. The account/provider suite includes local movement-store isolation, restart, quota, duplicate attempts, corrupt primary/backup recovery, reset and malformed-data rejection. Existing Bosh/Akatsuki/CCV3 PP parity fixtures pass without a formula change.

Rendered navigation, desktop interaction, native lifecycle, frame pacing, clean ZIP startup and data-preserving update results are recorded in the matching final delivery evidence. Raw session telemetry and private player data are excluded from the public source/runtime packages.

Physical mouse/tablet feel across real play sessions, broader hardware/skins, Linux execution, dense replay equivalence and accepted live server submission retain their existing qualification limits. Performance measurements are CPU frame/submission timing, not end-to-end latency. No universal zero-spike guarantee is made.

## Final local runtime checks

The focused rendered-menu run passed 159 checks at 1280x720, 1024x768 and 1600x900, covering slider limits, Max/Maple exclusivity, AC-independent sliders, mode switching and the learning opt-in/decline flow. A preceding run passed 349 existing navigation checks before reaching an obsolete fixture expectation for the replaced Max toggle. The corrected focused run covers that replacement; the legacy fixture failures are retained privately.

Eight focused synthetic real-client cycles on the owned simple map passed 22 pause/resume pairs and 3 retries with clean shutdown. The recorded sample contains 200,474 foreground frames, weighted mean 1.000038 ms, maximum 5.5738 ms. The p95 and p99 histogram upper bounds were 1.005 ms in every measured attempt. These are capped CPU loop measurements, including simulation-only frames, not displayed FPS or latency certification. A new four-cycle sealed baseline also verifies active planner calls. The historical AFK-gated reference is excluded from active-assist comparison. The baseline/candidate use different synthetic raw paths and the candidate includes extra strength/mode/learning cases, so this is a practical regression check rather than an identical-input causal A/B experiment.

The earlier multi-map run passed all eight gameplay cycles but did not produce a final process-exit receipt and required stopping the isolated process. A subsequent multi-map run reproduced the existing dense replay score/counter mismatch after six completed cycles; its strict failure is retained, and it subsequently shut down cleanly. The focused simple-map run above retains strict replay checks and separately verifies the final lifecycle. One earlier shutdown stall remains unclassified; additional shutdown-stage diagnostics were added. This is a beta qualification limit, not a claim that every shutdown or dense replay is reliable.

All eleven portable CPU suites passed. The account/provider tests passed 3,487 checks, including 13 local-learning storage and 52 Max settings/preset checks. All 2,876 protected PP source files and the Maple V1/V3 implementation match the sealed source byte for byte. No saved player score was recalculated.

The final opted-in raw-learning run accumulated 88 qualified movements across two isolated profiles, each with a 44-movement summary (206 and 204 bytes). Unconsented attempts produced zero learned movements. These are synthetic input results, not human movement quality certification.

The worst 5.5738 ms measured frame coincided with Gen0/Gen1 managed collections. Its native tick was 0.033 ms; this coincidence does not prove the full cause. Rare scheduling/GC spikes remain possible.

Windows computer-use checks exercised left clicks, strength dragging, continuous wheel scrolling, scrollbar dragging, preset selection and Standard/Adaptive switching. Streams saved the expected five route values while strength stayed unchanged. Learning remained Off and no learning folder was created. The isolated window closed cleanly. These were Windows-injected inputs, not human mouse/tablet gameplay feel. A final text/status-only correction clarifies that learning is paused while Max is disabled; the gameplay DLL and control layout are unchanged.
