# PP systems

## Local calculations

**Bosh PP is the default local/private progression system.** It includes the existing custom difficulty/strain and configured Timewarp rewards. It is not official osu! PP and is not a promise of what a server will award.

The optional bundled Akatsuki and CCV3 systems use their own calculators and identities. They do not add Bosh custom reward multipliers. Provider selection belongs to the profile/world; switching does not automatically recalculate old plays or award the same score twice on a shared PP scale.

For comparable local totals, Boshsu selects the best eligible play per map/mode and applies its existing `0.95^i` weighting and local bonus curve. It does not simply sum every play. Historical values retain their captured calculator identity. Installing another calculator does not replace the server's aggregation rules.

## Map and mod state

- Generated Map Lab object timings and rated map CS feed the selected calculation.
- Runtime CS Changer does not overwrite rated CS. HD Remover remains PP-neutral.
- Normal DT/NC difficulty effects apply once at the actual playback rate. The separate configured Bosh Timewarp reward is retained.
- Accuracy, judgments, misses, combo and the captured mod/rate context matter.
- FC PP means the same map/context at current accuracy, zero misses and maximum combo. It is a hypothetical estimate, not another saved result.

Song Select estimates, live PP and Results use the selected provider boundary. A failed calculation preserves the raw score and exposes failure rather than inventing a fallback award. Background calculation rejects stale map requests instead of blocking the gameplay owner.

## Install another system

Use **Performance → PP systems → Import ZIP / JSON**, inspect the system, then select **Use for this profile / world**. You can also place supported files in `PPProviders` beside the executable and Rescan. Importing does not automatically equip a provider.

The release includes pinned source ZIPs for:

- **Akatsuki PP**: `akatsuki-pp-rs` revision `b96019f50aa7d09bcbde60016481a69df04315e4`.
- **CCV3 PP**: the validated 4.0.1 source tree identified by the shipped manifest.

These are recognized source families implemented by application-shipped calculators. Players do not need Rust/Python, a compiler or a custom adapter for those exact supported packages. Unknown/modified repository ZIPs are reported as unsupported; arbitrary repository scripts or DLLs are not executed.

## One JSON formula

A simple formula can use existing normalized attributes through one UTF-8 file:

```json
{
  "id": "myname.performance",
  "name": "My PP",
  "version": "1.0.0",
  "calculation": {
    "current": ["$bosh.current", 0.9, "*"],
    "fc": ["$bosh.fc", 0.9, "*"],
    "breakdown": {"My scale": [0.9]}
  }
}
```

This returns 90% of Bosh PP. No sidecar/manual checksum is required. Formula changes need a new version and identity. A formula over summary attributes is not a full replacement hit-object strain algorithm.

The client ZIP includes `PP_SYSTEMS_README.md`, `PP_AUTHORING_QUICKSTART.md` and `PP_PROVIDER_API.md` with the detailed format, operators and supported calculator identities. The source snapshot includes developer tests/helpers. Always validate a custom system across maps, mods, rates, accuracy and combo/miss cases before sharing it.
