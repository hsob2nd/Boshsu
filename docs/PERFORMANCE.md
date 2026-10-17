# Performance policy

Frame pacing, recurring long frames and loading consistency take priority over headline FPS. QPC application-frame time measures update/messages, CPU rendering and present/pacing; it is not end-to-end input latency or GPU completion.

| Shell profile | Texture cache | Preparation workers |
|---|---:|---:|
| Low | 16 MiB | 1 |
| Balanced | 64 MiB | 1 |
| High | 128 MiB | Up to 2 |
| Extreme | 256 MiB | Up to 3 |
| Custom | 16–256 MiB | Hardware-bounded preference |

The hardware limit is `min(3, max(1, logical CPUs / 4))`. These budgets apply to shell resources; they are not total process RAM/VRAM limits. Active texture leases can temporarily exceed eviction targets. Cache keys include size/file versions, and active resources are not disposed by eviction.

Existing bounds include 128 shell texture entries, 256 retained GDI fonts, three immutable pattern analyses and eight menu PP estimate results. Long-session qualification must examine plateaus after warmup, not declare a leak from two isolated memory readings.

Normal users retain detailed diagnostics Off. Optional diagnostics use bounded counters/rings and a bounded background writer. Timing reports retain warmup/background exclusions, first-frame timings, p50/p95/p99/p99.9, maxima, phase measurements and a seven-band frame histogram. Nested phases overlap and must not be added together. Resource inventories count owned engine/skin objects, not all driver GL or BASS handles.

The explicit Version 1.3 foreground fixture pauses when its own window loses foreground ownership. Resource observation and allocation monitoring are qualification-only work. Keep baseline/candidate settings, maps, skins and external workload comparable; report observer overhead and interruptions. Do not turn background-only measurements into a foreground performance certificate.

Treat update/assist/UI/upload/PP budgets as diagnostic signals, not correctness deadlines. Reject an optimization if it changes PP, assist output, score, replay outcome or progression. Never disable GC globally, conceal long frames or discard unexplained spikes as driver faults.
