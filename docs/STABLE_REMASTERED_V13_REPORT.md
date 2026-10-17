# Version 1.3 engineering report

17 October 2026 — Version 1.3 RC1 optimization candidate.

Version 1.3 remains an optimization release candidate. It removes recurring PP/UI allocations, avoids identical PP preparation work, moves optional diagnostic disk work off the owner, adds bounded diagnostic/resource evidence and repairs the corresponding-source build procedure. It preserves the working frontend and qualified gameplay/assist/reward model.

The matched comparison records similar mean work (0.308133 → 0.311031 ms) and similar per-attempt p99 bounds. It does not establish a general FPS improvement, zero spikes or a memory reduction. Rare long frames and higher short-run retained memory remain visible. Detailed measurements are in `STABLE_REMASTERED_V13_PERFORMANCE.md` and `STABLE_REMASTERED_V13_RESOURCES.md`.

PP and assist preservation are strong: 216 expanded golden states, 560 wider PP rows and 190,859 trajectory samples match. Broader testing also found existing PP edge cases and a captured dense-map replay mismatch. These reproduce on sealed RC6 and remain documented; this is not a blanket correctness certificate. See `STABLE_REMASTERED_V13_REGRESSION.md`.

Fresh Git-free source build passed on this Windows PC using the existing verified compiler and restored pinned runtime inputs.

Completed cycles: 45; resource-sweep assertions passed: True; elapsed 27.02 minutes. This scope excludes replay recording/playback, which is separately tested and has a retained failure. The earlier resource run completed 37 cycles in 30.84 minutes, then timed out waiting for native continuation after foreground was interrupted for the repeat comparison. It remains a failed/interrupted qualification, not a completed 65-cycle pass.

Source/package changes, retained dependencies, discarded unsafe optimization ideas and rights limits are recorded in `STABLE_REMASTERED_V13_STRUCTURE.md` and `STABLE_REMASTERED_V13_OPTIMIZATION.md`. The main-menu footer now reads the current build identity rather than a stale hardcoded version. Publication/checksums and final sealed-package qualification are recorded by the separate release manifest and delivery evidence. No personal data is included in clean staging, and no running personal client has been overwritten. The requested equal-or-better frame tails and complete replay correctness are not certified; this remains a candidate.
