# Performance and diagnostic storage

## Performance profiles

Low, Balanced, High, Extreme and Custom primarily control shell quality, effects, asset/cache budgets and background preparation. They do not select another Max Assist/Maple model or strength. Extreme is not a requirement for correct assistance.

Gameplay remains the priority. UI animation/effects scale with quality and motion settings; noncritical PP, authoring and account work belongs outside the critical input/render path. Reduced/Off motion preserves readable static identity and state feedback.

High loop FPS alone is not end-to-end input/audio/display latency. The existing short native fixture measurements do not establish a universal speedup, zero frame spikes or certification for every PC. See [testing scope](RELEASE_STATUS.md).

## Diagnostics default Off

Detailed recording is optional under **Performance → Advanced → Diagnostic file recording**. Its change applies to the next map. FPS and the bounded in-memory frame statistics remain available without writing session telemetry.

The legacy managed research recorder is disabled. Obsolete model/host/map-preparation traces have been removed. The old embedded Discord crash-upload transport and its credential are removed from the GitHub candidate; local errors use the capped session logger.

## One application session, hard limits

| Output | Maximum |
| --- | ---: |
| Optional assistance diagnostics | 64 MiB total |
| Optional native performance receipts | 16 MiB total |
| Optional Map Lab generation trace | 1 MiB |
| Each normal/error log file | 1 MiB |

Recognized old diagnostic files clear at application startup. Cleanup happens once per launch, not on each map change or retry. Optional recording stops at its quota instead of growing without bound or deleting parts of the active session. A folder lease prevents another concurrent launch from clearing active logs. If safe cleanup is unavailable, new recording stays disabled.

Cleanup is limited to recognized application diagnostic files. It does not recursively traverse player data or directory links and does not delete profiles, scores, replays, maps, skins or settings.

## Saved content is separate

Scores, native replay files and generated Map Lab audio are saved player content, not telemetry. Their storage can grow as you play or author variants. Back them up and manage that library deliberately. The log limits do not silently erase play history.

To report a bug, copy the relevant current-session logs **before the next launch** if you explicitly recorded them. Review/redact them before attaching anything to a public issue. Never upload passwords, tokens, account configuration or an entire personal installation.
