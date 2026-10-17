# Map Lab

Map Lab authors a **new saved difficulty**, conceptually like generating a difficulty externally and importing it. It is not a global client modifier.

## Workflow

1. Select the source difficulty in Song Select.
2. Open Map Lab.
3. Choose generation speed, base CS and pitch behavior; use quick presets or saved presets as appropriate.
4. Generate, then open/play the resulting difficulty.

Generated variants appear under the original map set as extra difficulties. Their authoring files remain saved in the owned Map Lab library and are rediscovered when the map database is rebuilt. Variants have their own file/hash, baked audio, object timings, rated difficulty and score identity.

Source provenance is used for grouping, regeneration and comparison. If the original source is no longer indexed, a saved generated variant remains available separately; restoring the source can restore the grouping. Keep generated audio and metadata with the `.osu` file when backing up or moving a variant.

## Rated map versus runtime settings

Example:

| Layer | Value |
| --- | --- |
| Source map | CS4, 180 BPM |
| Generated difficulty | CS8, 1.30× baked timing/audio transformation, 234 BPM equivalent |
| Runtime CS Changer | CS4 if you choose it |
| Runtime Timewarp | 1.00× unless you change it |

The generated difficulty remains CS8-rated. Runtime CS Changer changes the physical gameplay size without rewriting its rated map CS. Map Lab speed is already baked into the new timings and does not set Timewarp to 1.30× or count a second time as a Timewarp reward.

You may apply runtime Timewarp on top of a generated difficulty. The calculation keeps that live setting separate from the generation provenance.

## Pitch and identity

Preserve-pitch and pitch-shift variants use distinct identities in the current generator. Existing older variants are not rewritten, so regenerating with the current version may create another difficulty beside an older revision. Check the selected difficulty label and its audio if comparing variants.

Map Lab does not assign an official/server beatmap ID to a local generated map. Local PP/progression and any future real server submission are separate policy boundaries.
