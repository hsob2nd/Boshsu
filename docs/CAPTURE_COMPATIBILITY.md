# Boshsu capture compatibility

Use **Boshsu.exe** as the process and **Boshsu** as the window title when selecting a game/window in recording software. The title can include an additional context suffix; match the Boshsu prefix or executable rather than an entire temporary title.

## Client-side audit — 17 October 2026

The current RC4 client has a visible, ordinary top-level Windows game window. Its OpenGL control is the main window itself. Menus and the native gameplay runtime present through that same window and process; there is no separate McOsu gameplay executable to select. The application runs at the invoking user's privilege level (`asInvoker`). Shipping executable product/title metadata identifies Boshsu.

The current application/window implementation does not request Windows display-affinity capture exclusion, create a hidden capture-only surface, or implement an anti-capture/hook block. Recording software can enumerate the normal game window. No renderer replacement, artificial frame delay or gameplay/assist change was required for this audit.

The existing real-client Windows qualification captured the menu and gameplay window. This establishes window visibility and capture through the available Windows desktop tool; it does **not** certify another recorder's renderer hook, audio recording or automatic game catalogue.

## Insights Capture

Insights' current website says unsupported games can be recorded/clipped manually, and its V3 announcement describes capture across PC games. Its game recognition and automatic match/highlight support remain features of Insights. A normal Windows game window cannot register itself in another product's supported-game catalogue.

If Insights offers game/window selection, select **Boshsu.exe / Boshsu**. Automatic match/highlight events are not integrated or advertised by Boshsu. Do not rename the executable to impersonate another game.

Insights was not installed on the qualification PC. Its automatic detection and recording of Boshsu have **not** been physically validated. Report the Insights version, capture mode and whether Boshsu appears in its selection list when filing a compatibility issue; do not include account credentials or private recordings.

Official references: [Insights Capture](https://insights.gg/), [Insights V3 announcement](https://insights.gg/blog/launching-our-next-chapter-v3), [Insights recording troubleshooting](https://insights.gg/help/en/articles/13765168-why-isn-t-insights-capture-working).

## Capture selection

Prefer the recorder's OpenGL-compatible game capture when available. Window capture of the visible Boshsu window is a separate capture path. If a recorder requires a visible desktop-composited window, use Boshsu's existing windowed/borderless display options; saved display settings remain the player's choice. Do not run the game as administrator merely to make it recordable.

Only Boshsu's client-side visibility was audited here. Recording quality, hotkeys, audio routing and automatic detection depend on the selected recorder and should be tested in that product before being declared supported.


## 1.4 actual OBS qualification

See [Recording setup](CAPTURE_SETUP.md) for the in-client scene exporter and the limited real OBS OpenGL shared-texture recording test. Vendor automatic recognition remains unqualified.
