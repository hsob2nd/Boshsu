# Recording setup

Search `recording` or `obs`, or open Options → Advanced → Windows integration → Recording / OBS setup. Save the OBS scene collection and import it using OBS → Scene Collection → Import. It targets the current executable and game window; it adds no desktop, microphone or account data. Configure your encoder, audio, recording hotkey and optional replay buffer in OBS itself.

Prefer Game Capture → Capture specific window → Boshsu.exe. Menus and McOsu gameplay render through the same application window. Use matching GPU selection if your PC has multiple GPUs. Limit the capture frame rate independently of gameplay. Boshsu has no capture worker, encoder or recording allocation when the external recorder is closed.

Actual qualification used official portable OBS Studio 32.2.2, 640×360 at 30 FPS, one capped disposable client and the AMD RX 6600. OBS successfully hooked the OpenGL renderer using shared textures; a nonblank gameplay/pause image was visually inspected. The roughly 37-second recording produced 1,112 output frames, zero encoding-skipped frames and one OBS rendering stall (0.1%). The concurrent client cycle passed four pause/resume pairs and a retry. No desktop or microphone was captured. This is limited qualification, not a zero-overhead or all-hardware certificate. Earlier setup failures and black captures were retained privately rather than counted as passes.

OBS compatibility does not guarantee automatic catalogue recognition, overlays or highlight events in AMD Adrenalin, Insights, Medal or ShadowPlay. Those vendor capabilities remain unqualified. Do not rename Boshsu to impersonate osu! or bypass a recorder's game catalogue. Built-in video encoding is not included in this candidate.

Primary references: [OBS Game Capture](https://obsproject.com/kb/game-capture-source), [OBS GPU selection](https://obsproject.com/kb/gpu-selection-guide), [OBS launch parameters](https://obsproject.com/kb/launch-parameters).
