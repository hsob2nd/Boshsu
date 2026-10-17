# Version 1.3 resource and storage audit

17 October 2026 — Version 1.3 RC1 optimization candidate.

Completed cycles: 45; resource-sweep assertions passed: True; elapsed 27.02 minutes. This scope excludes replay recording/playback, which is separately tested and has a retained failure. The earlier resource run completed 37 cycles in 30.84 minutes, then timed out waiting for native continuation after foreground was interrupted for the repeat comparison. It remains a failed/interrupted qualification, not a completed 65-cycle pass.

The matched short run retained more memory in the candidate: private peak 1286.8 → 1528.7 MiB; final managed heap 193.0 → 440.8 MiB. Gen2 collections were 10 versus 8. This is not a memory-win claim or proof of a leak. Longer data is required to distinguish warm caches/collection timing from retained resources.

Existing bounds remain: shell texture count 128 and profile-dependent 16–256 MiB eviction target; active leases may exceed the target temporarily. Fonts retain at most 256 entries, pattern analysis three entries, menu PP estimates eight, shell queue 64 and active preparation workers 1–3. Live PP remains one request in flight. The new native writer has 64 queued records / 4 MiB queued text plus at most one in-flight record; its worker exists only after explicit diagnostic enablement.

Diagnostic tests passed: 88 newest-session/retention assertions, three logger cap assertions, 15 native storage assertions, 31 writer/histogram assertions and 21 repeated frame/logger checks. Player files survive cleanup and concurrent launch cannot delete another active session. Performance storage remains 16 MiB with reserved status space, assistance 64 MiB, normal logs 1 MiB per file. Writer failures/drops remain failures. Slow/disconnected storage can still delay a draining shutdown.

The matched candidate accepted and wrote all 223 diagnostic jobs, with zero failed/dropped records, an empty queue at shutdown and peak queued text 208,861 bytes. Native inventories count owned manager/skin objects and overlap; they are not all GPU/driver/audio handles.

A verified private portable checkpoint preserves user data. External libraries remain read-only references and require a separate backup; they were not silently included in that checkpoint. No personal installation was replaced. The actual updater passed 3,984 full-package assertions on a marked checkpoint copy: 1,172 protected files (including the copy-only marker), application rollback after an injected partial-install failure and successful installation hashes. Updater staging/recovery was intentionally excluded from protected-file comparisons. Final sealed-archive qualification is recorded separately.

The uninterrupted run wrote all 746 diagnostic jobs with zero failed/dropped writes and an empty shutdown queue; peak queued text was 423,149 bytes. Performance evidence used 6.38 MiB of the 16 MiB cap. Private memory peaked at 1762.6 MiB and ended at 1404.2 MiB. Handles peaked at 1220 and ended at 1196; threads peaked at 83 and ended at 61. Pattern/estimate caches stayed within three/eight entries; shell textures peaked at 15 and fonts at 37. This supports the tested resource bounds over 45 cycles, not indefinite leak freedom or a memory reduction.
