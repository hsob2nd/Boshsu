# Boshsu 1.2 RC6 — Pause correctness and session diagnostics

17 October 2026. Continue from RC5; no client rebuild, assist retuning or PP recalculation.

## Player-session findings

The installed personal client was RC5. Its retained performance log contained 1,218 slow-frame records: 11 marked Play, 1,050 Song Select, 133 Main Menu and 24 Results. The largest two Play-labelled stalls, 930.26 ms and 224.11 ms, coincided with first engine attachment/map preparation rather than steady gameplay. Retained in-map examples reached 10.28 ms in the Draw phase, which includes native rendering/present. Those records cannot distinguish CPU work, GPU/driver waits and external contention.

Both ordinary logs hit their existing 1 MiB caps roughly six minutes into the longer session. Thousands of per-difficulty start/completion messages filled the runtime file, while repeated limiter waits were being reported as multi-line slow frames. Later gameplay was not captured. Detailed native diagnostics were Off. This limits causal conclusions: the pause defect below is reproduced, but the exact cause of the player's late-session hitch is not proven.

Recent completed scores retained PP/progression results with no pending calculation or PP error in the inspected records. One completed play had an ineligible replay clock/start boundary; that is retained as a separate observation, not silently converted into a valid replay or assumed to explain the pause bug. Private logs, profile identifiers, map paths and score data remain local and are excluded from public packages.

## Reproduced pause defect and correction

The shipped build failed this real-client sequence: pause, choose Continue, reopen Pause while continuation confirmation is pending, then send another continuation click. The native continuation branch could accept the click despite the paused state and set the map playing again. The baseline fixture observed map time advancing from 334 to 1,114 ms after re-pausing.

The native map now processes scheduled continuation only while unpaused. Application-pointer continuation rejects paused input, and entering Pause clears any stale confirmation. This corrects lifecycle ownership; gameplay timing integration, target selection, aim strength, slider logic, PP rules and score history are unchanged.

The correction passed 116 lead-in and 116 active-pattern assertions across four contexts: unassisted, Maple V1, Max Assist and Maple V3, including AC/Relax combinations, Raw Input On/Off and rates 1.00/1.95. Checks covered the real form Escape route, key repeat, pause-control activation, frozen map time/score/judgments, Continue, reopening Pause during confirmation, tapping/clicking, focus regain, retry and Back. These are production-window/runtime tests using synthetic inputs and owned fixture maps; they are not a physical mouse/tablet certification.

## Diagnostic overhead corrections

- Keep real wall time for FPS, but exclude intentional limiter waits when deciding whether a frame is a stall.
- Emit one compact record per native-client stall, including mode, lifecycle state, attempt/session, map time, frame/update/draw/scheduler/wait timings and managed GC counters. The native Draw figure includes its present wait; a zero legacy Swap figure does not imply that native present took zero time.
- Move per-difficulty start/completion chatter to Debug. Timeouts remain visible at Important level.
- Stop formatting and queuing writes once a logger has reached its cap. Serialize background file writes so overlapping scheduler callbacks cannot race on the same file. Protect first logger construction against concurrent creation/clearing.

21 deterministic checks passed for intentional pacing versus real stalls, the captured 10.28 ms/930.26 ms cases, filtering, release-debug suppression, concurrent creation/writing, hard file caps, inert post-cap logging, cap reset and disabled logging. Writes remain off the gameplay thread. The existing 1 MiB normal-log cap, newest-session retention and detailed telemetry Off default remain intact.

## Validation

The managed and native builds passed. Also passed: 9,736 contract assertions, 2,322 bridge checks, 5,890 shell checks, 4,997 offline/pause-lifetime/binding checks and 82 updater checks. An isolated clean-package first launch and saved-display restart passed 13 checks. The baseline pause failure and corrected results are preserved privately. Existing qualified model/calculator files retain their previous hashes.

A matched short controlled-autoplay fixture completed three maps, eight pause/resume pairs and one retry per build on this PC. Native QPC frame measurements include application update/messages, drawing and present/pacing; they are not end-to-end input latency. Warmup and duplicate shutdown snapshots were excluded; all recorded frames were foreground.

| Recorded metric | RC5 baseline | RC6 candidate |
|---|---:|---:|
| Frames | 125,750 | 130,104 |
| Weighted mean | 0.1662 ms | 0.1607 ms |
| Largest attempt p95 upper bound | 0.225 ms | 0.215 ms |
| Largest attempt p99 upper bound | 0.340 ms | 0.290 ms |
| Maximum | 16.8915 ms | 16.5821 ms |

The candidate showed no material regression in this comparison. Percentile figures are the largest per-segment histogram upper bounds (0.005 ms buckets), not pooled percentiles. Both runs still contained draw/present spikes above 5 ms; RC6 had three frames above 10 ms, versus two in RC5, and neither had a 20 ms frame. This short owned-map run does not establish an improvement on every map or resolve the uncaptured late-session hitch.

## Delivery and limits

RC6 is an optional complete portable update, with corresponding source and checksums. Both manual and in-client update paths preserve player data. The personal installation is not automatically replaced. Source and clean packages exclude player accounts, scores, maps, custom skins, telemetry and credentials.

The identified pause bug is fixed. Reduced logging overhead is verified, but every gameplay hitch is not certified fixed. Cold engine attachment and some map/Results preparation can still stall; the missing late-session evidence cannot be recreated. A subsequent normal play session with the compact logs is needed to attribute any remaining hitch. No zero-spike, end-to-end latency, all-hardware, physical-device, online or recorder-recognition certification is claimed. Existing component notices and supplied-asset/runtime provenance limits remain.
