# Version 1.4 RC1 — published package validation

Published [v1.4.0-rc.1](https://github.com/hsob2nd/Boshsu/releases/tag/v1.4.0-rc.1), continuing the sealed 1.3 baseline. This document records post-sealing checks; it does not alter the attached executable or source ZIP.

| Check | Observed result |
| --- | --- |
| Version identity | Executable file version 1.4.0.0, product version 1.4.0-rc.1, shell Boshsu 1.4, updater tag v1.4.0-rc.1 |
| Exact clean ZIP | 1,645 runtime files; SHA256 `460cdf3f762f613896e1cecd3859d50d2f940d57810c628096660b79a7f5116d` |
| Matching source ZIP | 7,418 curated files plus archive readme; 948 pinned McEngine inputs verified; SHA256 `ac2ea03ad370366340ce93f72855323c52a61797bfc76b93daf6937c99cbe0f7` |
| Portable startup | Two real launches of a fresh exact-ZIP extraction reached Main and shut down cleanly; the same fresh profile survived restart |
| Display persistence | Fullscreen and 1920×1080 saved; qualification used the left-monitor launch override |
| Update/rollback | 1,679 assertions; 15 protected synthetic files preserved on a disposable baseline installation; live player installation untouched |
| Published update discovery | Six assertions against the sealed 1.3 updater selected the real 1.4 release, clean ZIP, allowed origin and matching digest |
| PP/model preservation | All 2,876 protected PP source files and qualified model/data hashes match 1.3; no saved score recalculation |
| Local release suites | 28,542 portable-host assertions and 3,422 account/provider assertions passed |
| Public package integrity | GitHub's Linux release-integrity job passed against the uploaded ZIPs and digests |
| Hosted Windows CPU | Portable-host and source-compiled account/provider regression job passed |

Both jobs in the [GitHub run](https://github.com/hsob2nd/Boshsu/actions/runs/39321411056) completed successfully on release commit `4c1781523f005f39ffa51c84b95ead3c7e00b8f4`. This qualifies the published package integrity and the hosted CPU fixture scope; it is not a complete source build or Linux client launch. None of these checks certifies physical tablet feel, end-to-end latency, every mode/skin/device, live online submission or all-PC performance.

Issue #11 is delivered and closed: Songs-folder linking and skin-folder import, with 24 filesystem and 54 real-shell checks. Other feature issues retain their documented remaining acceptance work. PP changes (#3/#7) and launch troubleshooting (#9/#10) were excluded by the owner. Dense replay parity and accepted Kawata submission rules/proof remain open; uploaded scores are not enabled by these tests.

The clean package contains no personal profiles, maps, custom skins, scores, replays, credentials or telemetry. First-run generated test files stayed private and were never added to release assets. The initial preservation harness failed to resolve its renamed test-host assembly; correcting the harness filename made the full exercise pass. That fixture failure was not hidden or attributed to the shipped client.
