> RC3 supersedes the raw cursor ownership described in the RC2 report. See [the current correction report](INPUT_UI_FIX.md).

# In-client updates

Open **Options → Updates**, or search **updates** with Ctrl K.

1. Check for updates. Optional startup checking is available.
2. Enable beta releases for RC builds, or disable it for stable releases only.
3. Download the available update. Progress and Cancel remain available; gameplay is not used to perform download/verification work.
4. Click **Restart & install** when ready. Boshsu drains saves and closes before its installer replaces files, then restarts normally.

Updates come from `hsob2nd/Boshsu` GitHub Releases. The client verifies the GitHub SHA-256 digest, expected release identity, full file manifest and safe application-owned paths. Personal profiles, scores, progression, maps, skins, settings and custom PP provider files are preserved. A failed installation attempts to restore changed application files; one prior application version is retained under `Updates/rollback-*`. `Updates/last-update.txt` records the last installer outcome. The update downloader keeps one completed package and removes partial downloads when cancelled.

RC1 predates the updater: install the complete RC2 portable ZIP once to obtain this feature. RC2 and later can obtain subsequent compatible releases inside the client. No silent install, forced restart or credential upload is performed. GitHub connectivity is required only for checking/downloading; offline play remains independent.

If installation cannot proceed (permissions, insufficient space or locked files), keep the existing installation and check the outcome/rollback folder. An installer will not ask for administrator elevation. [Report a reproducible issue](https://github.com/hsob2nd/Boshsu/issues); do not upload private account/configuration files or unreviewed logs.
