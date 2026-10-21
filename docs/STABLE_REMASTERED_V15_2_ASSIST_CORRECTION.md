# Boshsu 1.5.2 — Max and Adaptive Assist correction

Continues 1.5.1. Adaptive now means **normal Max Assist plus optional gradual local movement learning**. It no longer substitutes the model's full requested trajectory for Max's normal blended guidance. Max strength stops at 100%, the established qualified maximum.

## Telemetry and confirmed cause

A private session review identified late AC acquisition: the delivered cursor could remain outside the configured capture radius while the requested route was already at the head. The movement/activity gate allowed assistance in the reviewed cases; no planner exception or diagnostic writer loss was recorded. Private raw traces, map names, profile identity and score history are excluded from the public source/package. This locates late acquisition but does not classify every miss as a software defect: Max deliberately requires meaningful physical movement.

Source inspection confirmed a separate mode mismatch: normal Max consumed the qualified model's blended output, whereas Adaptive consumed its full requested route, even with learning Off or no personal data. Its strength therefore did not mean the same thing as normal Max. This mismatch is corrected at one shared composition boundary. No target selector, hit window, Relax timing, PP rule or qualified model data was retuned to manufacture better scores.

Detailed diagnostics retain only one application session. A later launch cleared the earlier raw CSV in accordance with that policy. The bounded private review, source hashes and aggregate findings were preserved; it is not claimed that an exact device-packet replay can be recovered from aggregated rows.

## Shared guidance and gradual adaptation

Both modes use the same qualified blended guidance, target owner, refinement, movement budget, map clock, AC/Relax coordination and slider ownership. Adaptive with learning disabled, an empty profile or no valid learning segment is identical to normal Max at the composition boundary. There is no separate full-route mode or extra strength boost.

When explicitly enabled, learning still observes unassisted canonical mouse/tablet movement before Max/AC/Relax. It stores bounded directional curvature statistics locally. The learned delta is added at the same qualified composition intensity, without changing the baseline's forward progress or target deadline. The route's start and endpoint receive no learned displacement. At full confidence the shape adjustment is at most 6% of the raw movement chord before composition, at most 3% at Max's maximum 0.50 internal composition intensity, and remains inside the one physical-movement budget.

The profile available at map start is frozen for that map. New observations are saved after the attempt and become available to later maps. They no longer change the active learned shape while the player is playing. Directional confidence increases over 512 qualifying movements in that direction, rather than reaching full influence at 32; this is gradual statistical adaptation, not a promise of improved aim or a neural model.

A deterministic old-versus-new production-header test also reproduced lost learning at circle → slider transitions: the old code retained zero completed circle movements, while the fix retained the one valid circle movement and learned no slider samples. Entering a slider now finishes the prior circle segment instead of discarding it.

Existing learning data, consent, profile IDs and reset behavior are retained. Learning remains Off in a fresh release and requires the existing explanation and explicit opt-in. No data is uploaded. Session logs remain bounded and separate from the small persistent per-profile summary.

## 100% limit and compatibility

The strength slider ends at 100%. The retired native boost control and boost path were removed. Live selections reject values above 100%. Older 101–125% profile selections load at 100% while preserving mode, consent, refinement, slider settings and other profile data. Loading alone does not rewrite the saved manifest.

Historical gameplay/replay contexts remain readable with their original recorded settings. The native runtime caps an old context at 100% and does not recreate the removed boost. Historical score files and PP values are not recalculated or rewritten by this migration.

The ten aim-style presets, Max/Maple exclusivity, AC/Relax independence, standalone automatic sliders, movement-only AFK protection and free cursor during inactive sections remain in place. Normal Max's 100% guidance and the qualified planner are preserved.

## Qualification

The final native suite passed 149,787 checks with 6,012 production-model samples. This includes exact untrained Max/Adaptive equality, low strength, AC/Relax ownership, held device packets, repeated same-clock polls, 60–4000 callback rates, several playback rates, bounded learned curvature, fixed per-map learning and endpoint preservation. These are deterministic CPU checks, not human mouse/tablet feel certification.

All 45 qualified planner traces / 190,859 samples remain byte-identical. Existing native Max authority, Max refinement, AC, Relax, Maple, slider and AFK regression suites pass. The account/provider suite passed 3,493 checks, including legacy-strength migration without profile/history rewrites and 58 Max settings/preset checks.

Final rendered-menu, real-client lifecycle, package/update and installation receipts are recorded beside the release's saved delivery. Initial menu qualification caught obsolete encoded test labels after 29 and 32 checks; both failures are preserved. After correcting the fixture labels, final rendered navigation passed 159 assertions across 1280x720, 1024x768 and 1600x900, including all ten presets, the 100% slider endpoint, mode selection and independent slider ownership. All eleven final portable-host CPU suites passed. A seeded production-model/native-delivery test measured a bounded 7.0-unit path difference while retaining the Max baseline and physical movement requirement.

Existing dense native replay equivalence, an earlier unclassified 1.5.1 shutdown stall, live online submission, broader hardware and Linux runtime limitations are not certified by this correction. Performance figures describe CPU frame/submission timing, not end-to-end latency or a universal zero-spike guarantee. GPL/dependency notices and existing asset/source provenance limits remain intact.

## Final lifecycle and frame-time check

Eight cycles on the exact final native DLL passed 22 pause/resume pairs and 3 retries, including Standard/Adaptive, AC off/on, manual/Relax tapping and explicit synthetic learning. Strict selected-fixture replay checks passed and the process exited cleanly. 203,163 foreground CPU frames had a weighted mean of 1.000186 ms and maximum 5.5764 ms. Per-attempt p99 histogram upper bounds ranged from 1.005 to 1.015 ms. This is a capped CPU-loop regression check, including simulation-only frames, not displayed FPS or latency certification. Rare scheduling/GC spikes remain possible. The diagnostic writer drained with no dropped or failed records.

The opted-in simple-map attempt persisted 44 qualifying movements in a bounded local summary. The tiny-spacing object-heavy attempt correctly produced zero qualifying raw chords and no aggregate. Seven owned map fixtures were rotated across the eight cycles. A preceding eight-cycle candidate run also completed with a clean exit before the additional circle-to-slider learning correction.

Windows computer-use checks passed Options/Assists/Max clicks, strength dragging from 100 to 50 and back past the endpoint (capped at 100), Standard/Adaptive selection, Max/Maple exclusivity, continuous scrolling and clean exit. Learning stayed Off. These were injected Windows inputs in an isolated profile, not human mouse/tablet gameplay.
