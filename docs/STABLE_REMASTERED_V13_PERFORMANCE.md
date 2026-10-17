# Version 1.3 performance qualification

17 October 2026 — Version 1.3 RC1 optimization candidate.

| Measurement | Sealed RC6 baseline | Candidate |
|---|---:|---:|
| Recorded foreground frames | 1,065,802 | 1,056,072 |
| Mean recorded frame work (ms) | 0.308133 | 0.311031 |
| Mean update incl. messages (ms) | 0.046909 | 0.046972 |
| Mean draw incl. swap (ms) | 0.258149 | 0.261047 |
| Mean swap (ms) | 0.137767 | 0.135612 |
| Maximum recorded frame (ms) | 211.645700 | 14.237400 |

Both matched runs completed 12 scripted cycles, 32 pause/resume pairs and four retries, using the same owned maps, resolutions, quality rotation and default/fallback skin workload. The baseline host differs from sealed RC6 only by inert qualification instrumentation; its gameplay optimization files were restored verbatim and its native DLL is sealed RC6. One final shutdown snapshot was excluded as a duplicate; retry attempts remain included.

Per-attempt p95 upper-bound range: baseline [0.2, 1.135], candidate [0.225, 1.145] ms. Per-attempt p99 upper-bound range: baseline [0.27, 5.15], candidate [0.355, 5.11] ms. These ranges are not aggregate percentiles. Bucket width is 0.005 ms.

| Threshold | Baseline count | Candidate count | Baseline / million | Candidate / million |
|---|---:|---:|---:|---:|
| AtLeast1Ms | 19602 | 21217 | 18391.78 | 20090.49 |
| AtLeast2Ms | 9435 | 9704 | 8852.49 | 9188.77 |
| AtLeast5Ms | 3072 | 3084 | 2882.34 | 2920.26 |
| AtLeast10Ms | 146 | 184 | 136.99 | 174.23 |
| AtLeast20Ms | 1 | 0 | 0.94 | 0.00 |

Mean recorded frame work changed by 0.94%. This pair establishes similar mean/p99 work, not a demonstrated overall FPS win. The baseline's 211.646 ms maximum spent 211.242 ms in the qualification harness; it remains reported and is not credited as an optimization win. Swap/present long frames remain visible.

A repeat comparison completed six cycles, 16 pause/resume pairs and two retries per version with compilers stopped. Mean recorded work: 0.298659 → 0.302246 ms (1.20% higher). Per-attempt p95: [0.215, 1.22] → [0.23, 1.22]; p99: [0.315, 5.3] → [0.34500000000000003, 5.4750000000000005] ms. Maxima: 14.3765 → 14.4761 ms. ≥10 ms counts: 58 → 68 (105.91 → 125.66 per million). Neither had a ≥20 ms frame. Mean swap work accounts for about 71% of the repeat mean difference; this observation does not establish a driver cause. Rare-frame elimination, equal-or-better tails and reduced GC interference are not proven.

Measurements are QPC CPU frame work/present/pacing, not GPU completion or end-to-end latency. Resource-query observer work is reported separately; counters themselves also have overhead. The two-second managed observer's maximum was 4.5026 ms baseline / 3.3464 ms candidate and remains within recorded frames. No zero-spike/all-PC certificate is issued.

The 20 largest retained frames in each repeat cohort all spent at least 65% of their recorded work in swap/present; none recorded a coincident managed collection. The first comparison had 19/20 baseline frames dominated by swap and one by qualification, versus 20/20 candidate. This is a biased tail sample, not the whole distribution, and does not identify a driver/OS cause or prove that all spikes are external.

The first background-only attempt was invalid for foreground comparison and was stopped during a reported PC slowdown. The PC recovered after Discord was closed; the cause was not proven. That run is excluded from the comparison above.

Completed cycles: 45; resource-sweep assertions passed: True; elapsed 27.02 minutes. This scope excludes replay recording/playback, which is separately tested and has a retained failure. The earlier resource run completed 37 cycles in 30.84 minutes, then timed out waiting for native continuation after foreground was interrupted for the repeat comparison. It remains a failed/interrupted qualification, not a completed 65-cycle pass. Long resource testing overlaps one-worker source compilation and CPU fixtures, so it is not a matched performance comparison. Windows physical DPI variants, Linux, arbitrary hardware and physical mouse/tablet feel are not newly certified.
