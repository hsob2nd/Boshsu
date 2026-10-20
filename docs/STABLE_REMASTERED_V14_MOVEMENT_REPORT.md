# Boshsu 1.4 RC2 — Max Assist V1/V2 and background scheduling

This patch continues 1.4 RC1. It applies the existing Maple V1 physical-movement correction budget around Max Assist's qualified model. Max intensity remains 0–0.50, with 0.50 representing its full budget. The model, training data, target/deadline selection, AC, Relax, independent automatic slider authority and PP formulas/rewards are retained.

Additional Max offset is bounded by actual canonical mouse/tablet displacement, scaled by the existing intensity. Repeated polls within one map millisecond cannot accumulate extra pull. Stationary/tiny movement releases the old offset using the same map-time resynchronization principle as V1. Map ownership, retry, pause, invalid data, activity release and long clock gaps clear the budget state. The wrapper allocates no objects, performs no I/O and creates no worker. It runs in the existing authoritative gameplay transaction. No FPS or CPU-thread-count multiplier was added.

Maple V1 and V3 implementation files are unchanged. Automatic slider bodies continue through their existing explicit owner after movement guidance; the Max movement cap does not limit their separate full-authority path.

## Optional Max Assist V2

Options → Assists → Max Assist now offers V1 and V2. Existing profiles and historical contexts default to V1. The choice and two V2 refinement sliders (0–2) are saved per immutable local profile and captured in gameplay/replay context. Changing version leaves the original planner settings and V2 tuning intact. Invalid stored selections fall back to V1 without rewriting history; edits are blocked during gameplay.

V2 keeps the same qualified model, target/deadline ownership, main intensity, activity protection and native AC/Relax/slider coordinator. It adds bounded movement-relative refinement toward that single target, a diminishing-return gain curve, softer correction near the valid hit region and separate authored-slider-path refinement. No secondary Maple target selector runs. Additional refinement is capped radially; it never clamps the original Max plan to that small radius. Coarse movement and fine correction share one physical displacement budget, rather than stacking two unrestricted cursor outputs.

Refinement integrates observed raw/planned positions on the map millisecond clock. Repeated same-clock polling cannot acquire more guidance. A stopped cursor releases guidance; map, clock, policy, activity, settings and ownership resets discard stale state. The implementation creates no worker, writes no files and reads no CPU-thread-count or render-FPS gain. Automatic sliders remain their separate 100% native body authority with AC on or off; the slider refinement setting controls guided behavior. It does not replace that ownership switch.

The first V2 fixture found an engagement-edge rounding difference between interpolated paths at different callback rates. A sub-nanounit FOV comparison tolerance corrected that boundary. The corrected fixture passed 95,366 assertions, including 224 equal map-clock paths, 25,110 actual qualified-planner/coordinator samples and 6,416 slider-body samples. Twenty-nine additional settings/persistence checks cover historical defaults, supported ranges, context serialization, isolation, rename/restart and corrupt-selection recovery. These are deterministic/synthetic checks, not physical-device feel certification.

The shell artwork scheduler previously created a 20 ms polling delay for every blocked job during gameplay. It now waits for actual mode, capacity, settings or cancellation signals. Queue/concurrency limits, cancellation, error visibility and shutdown behavior remain. Already-running decode jobs may finish; new ones wait during gameplay. No frame-loop lock or per-frame scheduler callback was introduced.

Three isolated comparisons with 48 blocked jobs measured 196,608–221,184 allocated bytes per 300 ms in the old implementation, versus zero in all three candidate samples. Median old allocation was 212,992 bytes. Both implementations passed 18 admission/cancellation/failure/shutdown assertions each run. This measures queued-artwork overhead; it does not certify an FPS gain or eliminate GPU/OS stalls.

The latest personal session logs were retained privately before update. They recorded no handled/hard-crash errors and a clean shutdown. The largest logged gameplay-mode stall was a 2,110.83 ms first surface attachment while Loading; the largest logged Playing stall was 16.71 ms, dominated by drawing. Those transition/GPU stalls are not relabelled as fixed. Slow-frame logs alone cannot establish full-session percentiles or mean FPS.

## Qualification

- Max wrapper: 38,349 assertions, 17,676 bounded samples, 84 equal straight-path comparisons at 60–4,000 Hz and multiple runtime rates, plus 8,370 real qualified-planner/coordinator observations.
- Qualified underlying model: 45 original trajectory files / 190,859 samples remain byte-identical. Model and data hashes match 1.4 RC1.
- Maple regression: 20,315 assertions, 1,120 invariance cases, 168 captures from outside the circle. No V1/V3 source change.
- Independent slider regression: 1,001,752 assertions and 530,880 body samples. Existing AC/Relax/activity/ownership fixtures passed.
- All eleven release CPU suites passed, including the new production-source worker queue fixture (28,560 assertions total).
- Native/managed builds passed on the development Windows toolchain. Runtime/package checks and hosted delivery are recorded separately after completion.

Physical mouse/tablet feel, all hardware, accepted Kawata submissions and the previously documented dense replay defect remain outside this evidence. Existing PP edge cases and historical score awards are unchanged.

## Final candidate checks

The V2 menu run passed 416 checks across 1280×720, 1024×768 and 1600×900. It exercised actual rendered version choices, independent refinement sliders, original controls, Maple/Max exclusivity, scroll/back navigation and captured gameplay settings. Screens were reviewed at native viewport size. This uses application-owned scripted controls, not physical mouse/tablet testing.

Four real-client synthetic V2 cycles completed with AC off/on and manual/Relax input: 12 pause/resume pairs, 2 retries, clean return and shutdown. The capped sample contained 102,299 measured frames; weighted mean 1.0000 ms and maximum 5.4026 ms. Per-attempt p95 upper bounds: [1.0050000000000001, 1.0050000000000001, 1.0050000000000001, 1.0050000000000001, 1.0050000000000001, 1.0050000000000001, 1.0050000000000001]; p99 upper bounds: [1.01, 1.0050000000000001, 1.0050000000000001, 1.0050000000000001, 1.0050000000000001, 1.0050000000000001, 1.0050000000000001]. These include fixture pacing/transitions and are not end-to-end latency measurements. They do not establish a universal performance gain. The earlier equal two-cycle RC1/V1 comparison remains separate.

The final native target-handoff fixture also ensures that fine-offset smoothing does not interpolate a previous object's plan into a newly selected target or a slider head into its body. Max remains the sole target owner. The model/data, V1 wrapper, Maple V1/V3 source and all 2,876 protected PP files are unchanged. The account/provider suite passed 3,451 assertions, including the 29 new selection/persistence checks. All eleven portable-host CPU suites passed (28,560 assertions). Matching package sealing, update/rollback and publication are separate delivery checks.
