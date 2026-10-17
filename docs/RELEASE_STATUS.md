# Release status and validation

Client display version: **Boshsu 1.2**. Initial GitHub tag: **v1.2.0-rc.1**. This is a clean Windows x64 **prerelease**, not a declaration that every original product goal is finished.

## Qualified locally

- Fresh clean-client startup: new local profile, zero scores, Main Menu, primary/native fullscreen defaults and saved display edits.
- Restart: saved window size/settings and the same local profile survive. Tests used a left-monitor override; actual primary/native defaults were asserted without taking over another display.
- The existing native gameplay/ownership, assist/slider, PP/provider, Map Lab, menu and storage test suites described by the project reports.
- Detailed telemetry Off/On integration, current-session cleanup, hard file budgets and continued local score/replay saving.
- Logging-only model wrapper change: 45 identical production traces / 190,859 samples against the reproduced sealed binary, plus current acknowledgment/ownership fixtures.
- GitHub privacy correction: legacy hard-coded Discord crash-report transport removed, local-error behavior retained, and release/source credential scans performed.

Native integration used authored/synthetic application input. It is not proof that a human's tablet, every mouse driver or every monitor/DPI combination passed.

## Still outstanding

- Live Bosh authentication, native online score submission and real server contract/replay proof.
- Stable score equivalence and legacy `.osr` conversion.
- Native taiko/catch/mania qualification; the current adapter accepts Standard maps.
- Linux launch, physical Windows DPI matrix/true 4K, human tablet/device qualification and all-hardware certification.
- Portable dependency bootstrap and a fully repeatable clean-machine build.
- Public redistribution clearance for supplied Bosh/stable artwork, legacy resource assemblies, audio/helper runtime and other exact assets/dependencies.
- Implementation source/redistribution provenance for the historical prebuilt Bosh PP module.

The root GPL notice applies to its stated source scope; it is not a blanket license grant for every bundled component. Read [THIRD_PARTY_NOTICES.md](../THIRD_PARTY_NOTICES.md) and the notices in the downloads. An accessible download or prerelease label does not resolve those rights or certify live online compatibility.

## Performance claims

Short development fixtures reached high native loop rates with low mean/p95 frame times, but measurable maximum spikes remained. That evidence does not certify end-to-end latency, zero spikes, all computers or a universal speedup. The richer shell must continue to preserve gameplay timing priority.

## Clean package

The package excludes the owner's profiles, avatars, settings, scores, map/custom-skin collections, replays and diagnostic history. The assistance model/calculator modules are intentionally shared under the owner's authorization. Existing source/dependency attribution is retained.

Errors remain local and capped. Optional detailed diagnostic files retain only the current application session. Saved player content such as replays and generated audio is independent from those quotas.
