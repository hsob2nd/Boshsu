# Threading and ownership

| Work | Owner / bound |
|---|---|
| Window messages, input, gameplay update, assist decisions, GL draw/present | Main application owner |
| Native target/model preparation and decisions | Existing qualified native/personal pipeline; map clock drives decisions |
| Live PP | One in-flight calculation per game mode, at most five requests/second; stale generations discarded |
| Menu PP estimates | Shared semaphore of one calculation, eight cached selection results |
| Shell image/art preparation | At most 64 queued tasks; 1–3 active jobs limited by profile and hardware; new jobs wait during gameplay |
| Native resource loading | Existing McEngine loader pool; lifetime is the engine's |
| Account score/PP/progression persistence | Account worker; completed replies applied by owner-thread Pump |
| Map Lab | Existing generation/audio jobs and cancellation; authored output published on completion |
| Public API | Two in-flight requests, asynchronous cancellation and 15-second timeouts |
| Authenticated session | Serialized session transport; no guessed backend award rules |
| Normal logs | Existing background scheduler, fixed target files and hard caps |
| Optional native frame diagnostics | One lazy worker, 64 queued records / 4 MiB queued text; no worker exists until diagnostics is enabled |

Performance profiles change shell preparation/quality budgets. They must not change assist strengths, target selection, map time, tap decisions or physical playback rate. Changing a worker count is not a substitute for gameplay ownership.

Never put map parsing, image decoding, large JSON saves, network requests, provider import, Map Lab generation or directory scans in the critical frame path. GL resource creation/disposal must stay on the rendering owner. A background task must not mutate current map/profile state after its generation has expired.

Optional frame reports still construct/format bounded JSON at lifecycle/report boundaries. Their directory scans and file writes are queued; worker shutdown drains accepted records after play has ended. Slow/failing storage can still delay shutdown and is not certified against arbitrary disconnected devices. Writer failure/drop counters remain diagnostic failures, not successful evidence.
