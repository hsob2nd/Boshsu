# Version 1.3 optimization inventory

17 October 2026 — Version 1.3 RC1 optimization candidate.

| Change | Evidence / scope |
|---|---|
| Reuse the exact legacy menu PP selection key for unchanged inputs | 1,157 identity/invalidation checks; 20,000 unchanged calls allocated 40 measurement bytes instead of 12,320,040 bytes |
| Cache the live PP presentation delegate and move task closure construction behind the due-request gate | Production early-out: 20,000 calls allocated 40 measurement bytes versus the previous 2,240,040; one in-flight task, stale-generation rejection and disposal remain intact |
| Reuse an identical native rated difficulty request at NM/HD and runtime rate 1.0 | 216 expanded golden states match exactly, including IEEE-754 values; 560 wider matrix rows also match. Initial preparation samples improved about 2.5–7.3% for eligible cases; unchanged HR controls varied around 1% |
| Queue optional native diagnostic disk work | One lazy worker; bounded FIFO. Deliberately blocked/failing storage tests passed. JSON formatting remains at report/lifecycle boundaries on the owner, with disk scans/writes on the worker |
| Compact diagnostic JSON | A real report shrank from 180,536 to 120,318 bytes while parsing identically; this is not a gameplay FPS claim |
| Add histogram/resource inventories and writer status | Seven exact histogram bands, ownership counts and failed/dropped evidence counters; no driver-wide GL/BASS claim |
| Repair archive build/bootstrap paths | Full McEngine hash inventory, bundled source archives, tool discovery, complete pinned binary restoration and current documentation |

No assist model, target selection, input clock, replay serialization, gameplay scoring, PP formula, reward curve or progression rule was retuned. Forty-five qualified trajectory traces / 190,859 samples are identical. Current ownership, Maple, AC, slider and activity fixtures pass. Ninety-three frozen gameplay/personal/replay/reward source files match their checkpoint hashes.

Rejected/unqualified ideas: deleting live fallback resource/FFmpeg DLLs, raising gameplay worker counts, global GC/priority/affinity changes, arbitrary PP multipliers, visual redesign and weakening assists. Default-off native metric counters still have sampling overhead; this pass does not claim that all diagnostics become zero-cost when recording is off.

No dependency version was upgraded. Existing imported-code compiler warnings remain; a passing build is not a zero-warning refactor certificate.
