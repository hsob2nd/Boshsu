# Boshsu 1.2 RC3 — Song Select skin layers and shared Raw Input

Song Select now explicitly tracks every legacy chrome instance replaced by Bosh, including both normal and hover images for Mode, Mods, Random and Options. Previously each button field was reassigned to its hover image, leaving the normal image drawn behind the footer. Their coloured tops could become visible at 1080p and other layouts. Both header backings, the old footer, cookie layers, old mod text and small mode indicator are bypassed as instances, with input disabled. Later header tooltip updates cannot revive an old input owner. Shared skin textures are not globally hidden: song rows, beatmap backgrounds, gameplay skins and the optional skin icons inside the modern footer remain available. Footer geometry is unchanged.

## Raw Input ownership

One native cursor now processes raw movement for menus, pause and gameplay. The legacy shell raw handler stops integrating the same packets once the native subsystem is available. The shell reads the native position after the existing application update, with an allocation-free call. Raw Input, sensitivity and absolute-to-window settings have one cached synchronization path, independent of map loading. Enabling Raw Input can initialize the runtime before the first map; surface/window changes rebind the same application window. No new input thread, frame multiplier or gameplay assist tuning is introduced.

Menu clicks use the visible raw cursor, rather than replacing it with the Windows compatibility-message position. Raw button/wheel packets also drive input while absolute mapping differs from desktop placement. Compatibility mouse buttons/wheel are excluded from the raw path, preventing duplicate delivery. Native button transitions are deduplicated, and focus loss releases held buttons and clears old raw state. The duplicate legacy wheel subscription is removed. Raw Input Off retains the Windows pointer path. Hardware/virtual desktop flags and explicit absolute-to-window mapping remain authoritative; no speculative device remapping is applied.

The supplied Bosh reference's RawMouseHandler, WndProcInputHandler and ownership/seeding flow were compared with the migrated client. Qualified Max Assist, Maple, AC, Relax and PP reward rules are unchanged.

## Validation

- Managed and native Release builds passed; existing compiler warnings remain.
- RC2 negative control reproduced four visible normal legacy buttons under the modern footer.
- 598 real-client chrome checks across 25 combinations: 1280×720, 1024×768, 1280×800, 1600×900 and fullscreen 1920×1080; Automatic/75%/100%/125% scale; Default, Rafis, Snowy and deliberately oversized classic/modern test skins. The final sweep includes Raw Input On and Off. Native-size screenshot review covered the main resolutions and an oversized skin; modern Mods was opened by a desktop click.
- 105 real-client cursor/navigation checks: native raw cursor before the first map, shell/native position agreement, relative aggregation/reversal, sensitivity 1/1.5/2, primary/virtual/window tablet mapping, valid (0,0), button-only packets, 2×/centred viewport, pause, focus, retry, Raw Input Off, F9 and update navigation.
- Desktop-injected Windows WM_INPUT was observed through the registered form hook. Main → Song Select → native map → Retry → return was exercised with desktop absolute mapping. This is a Windows transport test, not a physical mouse/tablet feel certificate.
- 5,890 shell contract checks include logical 1440p/4K/ultrawide and DPI/scale layouts; these larger modes are not physically certified on this 1080p monitor.
- 9,736 gameplay/display contract checks, 2,322 bridge checks, current qualified Max/AC/AFK fixtures and 55 updater protection/rollback checks passed.

Personal data/settings are preserved and personal/clean binaries match. The clean portable ZIP includes matching source and checksums. Detailed telemetry remains Off by default, with bounded current-session retention. Existing online/platform and supplied asset/runtime licensing limits remain unchanged. Normal user play with physical mouse/tablet is still the appropriate device-feel confirmation.
