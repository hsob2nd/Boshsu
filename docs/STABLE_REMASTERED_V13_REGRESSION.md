# Version 1.3 regression qualification

17 October 2026 — Version 1.3 RC1 optimization candidate.

| Area | Result |
|---|---|
| Contracts / fixed snapshot / shell / offline presentation / updater / allocation identity | 24,185 assertions passed against the candidate, repeated after Version 1.3 metadata |
| Shipped shell persistence / avatars / identity / fonts / notifications / worker budgets | 423 assertions passed in disposable data, without a game window or player records. An initial relative fixture path was rejected by the avatar validator as designed; the absolute-path invocation passed |
| Accounts, PP providers, mock server/session, submission ledger, Maple persistence, Akatsuki ZIP and CCV3 | 3,355 assertions passed; no real credentials or HTTP score submissions |
| Progression / shipped reward logic / achievement catalogue | 247 / 26 / 35 assertions passed; 18,576 catalogue entries. The old minimal-assist fixture now explicitly disables authoritative automatic sliders; no product policy changed |
| Native difficulty, arrows, health clipping, HUD, spinner and replay clock fixtures | 437 assertions passed |
| Qualified assist trajectories / native ownership | 45 exact traces, 190,859 exact samples; six current ownership/Maple/slider/activity suites passed |
| PP expanded golden capture | 216 states and 648 consistency assertions per version; exact values and IEEE-754 representation preserved |
| Wider actual PP matrix | 32,811 checks per version, 560 identical result rows; 380 identical pre-existing expectation failures retained |
| Actual short mixed gameplay | Both 12-cycle cohorts passed: 32 pause/resume pairs and four retries each |
| Extended baseline | Seven cycles, 20 pause/resume pairs and three retries passed |
| Fixed failing replay | Both versions fail identically: original 3,892,612 points / one 100; replay 3,892,530 / that judgment becomes 50. Original stored score remains unchanged |
| Prior RC3 update discovery | The actual RC3 selector accepts and selects the Version 1.3 clean ZIP in a mock release response; zero network requests |
| Final production-window pause regression | 116 assertions across four contexts passed, including re-pause during native continuation and focus regain |
| Production-window raw input / F9 | 105 assertions across five contexts passed; synthetic relative/absolute packets, menus and gameplay; observed focus/click before starting |
| Song Select chrome | 598 assertions / 25 skin-resolution combinations passed, including two deliberately oversized fixture skins; logical dimensions/scales, not physical DPI certification |
| Avatar and PP file-picker ownership | 146 assertions across four display/raw modes passed; both real Windows pickers opened/cancelled via desktop input, with 104 modal observations |

The wider PP failures are not hidden or reclassified as passes: 40 accuracy monotonicity failures and 340 positive-Hidden expectations fail identically on the sealed build. On the simple owned map, 97–98% yields 89.019pp while the nearest 99% state yields 87.416pp; NM SS 99.010pp versus HD SS 91.879pp. The positive-HD assumption may not describe the supplied custom calculator's intended rules. No formula/reward change or historical-score recalculation was performed in this frozen optimization pass.

The replay mismatch was isolated using the same frozen recording/context/resolution in both versions, rather than comparing two different original plays. Serialization/gameplay code is unchanged. A separate resource sweep explicitly excludes playback; its pass cannot certify replay correctness. Full replay parity remains unqualified.

Physical device feel, actual Windows DPI changes, other hardware/Linux, live authentication/submission and automatic third-party game detection remain unqualified or deferred. Logical layout/injected-input tests are not human-device certification. One desktop activation approval timed out; later restoration/click and both picker interactions succeeded. One accessibility-index Cancel click did not dismiss the picker; observed Escape cancellation succeeded. No client change was inferred from that tool action alone.
