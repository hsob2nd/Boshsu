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
