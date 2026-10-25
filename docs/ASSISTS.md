# Assists and input

These are the existing qualified assistance systems. This release does not introduce concealment, anti-cheat bypass or an assumption that assisted scores are allowed on an unrelated server. Online use must follow the actual server's policy; native ranked submission remains unqualified here.

| Setting | Role |
| --- | --- |
| Max Assist | Strong cursor assistance using the qualified personal movement model |
| Maple V1 | Reference-range cursor assistance; maximum V1 strength is 1 |
| Maple V3 | Existing V3 behavior; Power and Slider Power range from 0 to 2 |
| AC / Aim Correction | Its own correction/capture role, independently configurable |
| Relax | Generated tapping behavior; independent from choosing an aim owner |
| Automatic slider controls | Separate slider-body assistance/ownership options, including the dedicated AC authority option |
| CS Changer | Runtime gameplay circle size, separate from the map's rated CS |
| Reading/visibility controls | Existing Reading AR, HD Remover and associated reading settings |
| Survival controls | Existing slider/spinner/health protection settings where enabled |

## Combining systems

**Choose Max Assist or Maple, not both.** Enabling Maple excludes Max Assist. Maple exposes V1 and V3; the superseded variants are not offered as active modes.

AC can be used alone or with one cursor assist. Relax can be used alone or with a cursor assist. With Relax off, physical tapping remains necessary. With no aim owner, Relax does not become an aim system.

Slider assistance has separate controls so Max Assist, Maple and AC can operate in their supported independent/combined configurations. The dedicated AC slider-body authority option is separate from enabling AC itself. Read the active settings rather than assuming that every slider option is enabled by a cursor-mode selection.

## Strength and timing

The strong Max Assist behavior is retained; the earlier freer-assist experiment is not the selected model. Loading, lead-in and empty sections release unnecessary cursor ownership, while active-object assistance preserves the existing strength.

Max Assist and Maple V1 retain movement-only AFK protection with a 1.5-second real-time grace. Meaningful physical mouse/tablet movement counts; taps alone do not. Maple V3 does not use that same gate. This protects against passive play without retuning the underlying active assistance strength.

Gameplay decisions use the map/gameplay clock. AFK activity uses monotonic real time. Background worker limits and UI performance profiles do not choose another assist gain or model. A real-time application still depends on OS scheduling, input delivery and the observations it receives; “clock-based” does not mean that every machine has identical latency.

## Reporting inconsistent behavior

Include the map/difficulty, mods, runtime Timewarp, generated Map Lab metadata, cursor mode/strength, AC, Relax and slider settings. Describe whether movement, target switching, tapping or a slider body is wrong. A miss count alone does not establish which owner failed. Do not post private profiles or authentication files.

## Adaptive learning in 1.5.3 (current)

The profile retains 100,000 weighted learned movements TOTAL across eight directions. At capacity, the account worker proportionally ages old influence and keeps fresh observations; it does not freeze learning. The summary remains below 16 KiB plus a backup. Detailed raw logs retain their existing separate budgets. Strength remains capped at 100%; fixed per-map seeds, normal Max guidance and explicit local consent are unchanged. [Details](STABLE_REMASTERED_V15_3_LEARNING_BUDGET.md).

## Max Assist and Adaptive in 1.5.2

Strength ends at **100%**, the established Max maximum. Older higher selections load at 100%, preserving other options and history. The ten presets remain optional and preserve strength, slider mode, AC, Relax, rate and consent.

**Adaptive Assist is normal Max plus gradual optional learning.** With learning Off or an empty profile it gives the same guidance. Explicit opt-in records bounded local raw movement before assistance. The map uses the profile available at its start; new observations affect later maps. Learning shapes a small part of the route and does not raise aim strength or change deadlines, tapping or PP. A fresh release never enables learning automatically. Existing learning data and consent are retained.

Automatic sliders and Guided remain separate choices; AC is not required for Max automatic bodies. Meaningful physical mouse/tablet movement is required; taps alone do not bypass AFK protection. See the [correction report](STABLE_REMASTERED_V15_2_ASSIST_CORRECTION.md).

## Max Assist in 1.5.1 (historical behavior, superseded above)

The former Max V2 is now the normal **Max Assist**. Open **Options → Assists → Max Assist**. **100%** equals the previous maximum; 101–125% is an optional extended range. Existing lower intensity is preserved until you edit strength. Target refinement is separate from strength. **Aim style** offers ten optional route presets; Advanced edits show Custom. Presets preserve strength, slider mode, AC, Relax, rate and learning consent.

Choose **Automatic 100%** sliders for full native body ownership, or **Guided** to expose the guided-slider refinement slider. AC is not required. Advanced planner retains meaningful shape, directness, precision, timing and conditional input-matching preferences.

**Adaptive Assist** follows the model's full route while you actively move. Enable Max Assist and select Adaptive mode before learning can collect movement. **Local movement learning** is Off until you explicitly enable it on the explanation screen. It needs extra raw mouse/tablet cursor telemetry captured before assistance, and meaningful directed movement. It starts untrained and adapts gradually; improvement is not guaranteed. With learning off there is planned guidance only. It records bounded extra session diagnostics and stores a small per-profile raw-motion curvature summary locally. No corrected cursor output, replay or stationary jitter is training data; no data is uploaded. Learning does not change PP, target deadlines or hit windows. Reset deletes only that profile's learning summary and backup.

Detailed behavior, the endpoint correction and qualification limits are in the [1.5.1 report](STABLE_REMASTERED_V15_MAX_ASSIST.md). Real play testing is still needed to compare personal feel; the strongest setting is not guaranteed to suit every player.
