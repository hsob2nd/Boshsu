# Boshsu 1.2 RC4 — Windows file picker ownership

Avatar selection and PP ZIP/JSON import previously opened unowned Windows dialogs without releasing the application's hidden cursor, confinement or fullscreen priority. A Windows common dialog also runs a nested message pump on the application's own thread: the normal game idle handler could continue drawing and reclaiming the pointer while it was open.

All current shell avatar/PP pickers and the retained identity picker now share DesktopFilePicker. It explicitly supplies the main window as owner, yields native focus/Raw Input, releases mouse capture and confinement, shows the Windows cursor, suppresses the skin cursor and clears pending clicks/keys. The game idle handler yields to the modal pump; fullscreen cannot reclaim topmost priority. A finally block restores ownership according to actual focus after selection, cancellation or failure, including nested calls. It does not steal focus from another foreground application or change saved fullscreen, resolution, Raw Input or sensitivity preferences.

Ordinary window deactivation also releases confinement and shows the OS pointer immediately, covering Explorer/other desktop windows. Focus handoff continues to use the existing native cursor lifecycle. No gameplay input thread, frame multiplier, assist tuning, PP formula or native engine binary is changed.

## Qualification

- Release managed build passed with existing compiler warnings.
- 146 real-client handoff checks across fullscreen/windowed and Raw Input On/Off passed, covering owned selection, cancellation, nested calls, controlled failure, confinement, cursor visibility, preferences and restoration.
- Real Windows avatar selection and cancellation, plus PP ZIP and JSON imports and PP cancellation, were exercised through desktop input in fullscreen with Raw Input On. Windows cursor visibility/raw ownership/confinement/topmost state was observed while the modal picker was open. The avatar persisted across the test restart; imported PP appeared without switching the active calculator. An initial test-harness optional-parameter invocation was corrected; production code did not require a further change.
- 105 existing real-client menu/gameplay raw-input and navigation checks passed after activating the isolated fixture as required by its ready marker. An earlier attempt started with a stale marker before activation and was retained separately as an invalid setup run.
- 9,736 gameplay/display contract checks, 2,322 bridge checks, 5,890 shell checks and 55 updater protection/rollback checks passed.

This tests Windows desktop/modal integration on the available display, not every physical device, display configuration or platform. Qualified assistance and PP rewards remain unchanged. The portable clean package comes from the sealed fresh-data RC3 archive, not a mutable extracted installation; player data, custom maps/skins and telemetry are excluded. Existing license, online and platform qualification limitations remain as documented.
